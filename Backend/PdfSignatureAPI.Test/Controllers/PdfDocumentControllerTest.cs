using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PDFSignatureAPI.Controllers;
using PDFSignatureAPI.Core.Domain.Interfaces;
using PDFSignatureAPI.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PdfSignatureAPI.Test.Controllers
{
    public class PdfDocumentControllerTest
    {
        private readonly Mock<IPdfDocumentService> _pdfDocumentServiceMock;
        private readonly PdfDocumentController _controller;

        public PdfDocumentControllerTest()
        {
            _pdfDocumentServiceMock = new Mock<IPdfDocumentService>();
            _controller = new PdfDocumentController(_pdfDocumentServiceMock.Object);
        }

        [Fact]
        public async Task GetAllPdfDocuments_ReturnsOkResult_WithListOfDocuments()
        {
            // Arrange
            var documents = new List<PdfDocument>
        {
            new PdfDocument { Id = 1, FileName = "document1.pdf" },
            new PdfDocument { Id = 2, FileName = "document2.pdf" }
        };

            _pdfDocumentServiceMock.Setup(service => service.GetAllPdfDocumentsAsync()).ReturnsAsync(documents);

            // Act
            var result = await _controller.GetAllPdfDocuments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnDocuments = Assert.IsAssignableFrom<IEnumerable<PdfDocument>>(okResult.Value);
            Assert.Equal(2, returnDocuments.Count());
        }

        [Fact]
        public async Task UploadPdfDocument_ReturnsOkResult_WhenPdfIsUploadedSuccessfully()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("document.pdf");
            fileMock.Setup(f => f.Length).Returns(1);
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UploadPdfDocument(fileMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("PDF uploaded successfully.", okResult.Value);
            _pdfDocumentServiceMock.Verify(service => service.AddPdfDocumentAsync(It.IsAny<PdfDocument>()), Times.Once);
        }

        [Fact]
        public async Task UploadPdfDocument_ReturnsBadRequest_WhenFileIsNull()
        {
            // Act
            var result = await _controller.UploadPdfDocument(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No file uploaded.", badRequestResult.Value);
        }

        [Fact]
        public async Task DownloadPdfDocument_ReturnsFileResult_WhenDocumentExists()
        {
            // Arrange
            var document = new PdfDocument
            {
                Id = 1,
                FileName = "document.pdf",
                FileContent = new byte[] { 1, 2, 3, 4 }
            };

            _pdfDocumentServiceMock.Setup(service => service.GetPdfDocumentByIdAsync(1)).ReturnsAsync(document);

            // Act
            var result = await _controller.DownloadPdfDocument(1);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/pdf", fileResult.ContentType);
            Assert.Equal("document.pdf", fileResult.FileDownloadName);
        }

        [Fact]
        public async Task DownloadPdfDocument_ReturnsNotFound_WhenDocumentDoesNotExist()
        {
            // Arrange
            _pdfDocumentServiceMock.Setup(service => service.GetPdfDocumentByIdAsync(1)).ReturnsAsync((PdfDocument)null);

            // Act
            var result = await _controller.DownloadPdfDocument(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("PDF document not found.", notFoundResult.Value);
        }
    }
}
