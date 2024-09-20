using Moq;
using PDFSignatureAPI.Core.Application.Services;
using PDFSignatureAPI.Core.Domain.Interfaces;
using PDFSignatureAPI.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PdfSignatureAPI.Test.Services
{
    public class PdfDocumentServiceTest
    {
        private readonly Mock<IPdfDocumentRepository> _pdfDocumentRepositoryMock;
        private readonly PdfDocumentService _pdfDocumentService;

        public PdfDocumentServiceTest()
        {
            _pdfDocumentRepositoryMock = new Mock<IPdfDocumentRepository>();
            _pdfDocumentService = new PdfDocumentService(_pdfDocumentRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllPdfDocumentsAsync_ShouldReturnAllDocuments()
        {
            // Arrange
            var documents = new List<PdfDocument>
        {
            new PdfDocument { Id = 1, FileName = "document1.pdf" },
            new PdfDocument { Id = 2, FileName = "document2.pdf" }
        };

            _pdfDocumentRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(documents);

            // Act
            var result = await _pdfDocumentService.GetAllPdfDocumentsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddPdfDocumentAsync_ShouldCallAddOnRepository()
        {
            // Arrange
            var document = new PdfDocument { Id = 1, FileName = "document.pdf" };

            // Act
            await _pdfDocumentService.AddPdfDocumentAsync(document);

            // Assert
            _pdfDocumentRepositoryMock.Verify(repo => repo.AddAsync(document), Times.Once);
        }

        [Fact]
        public async Task GetPdfDocumentByIdAsync_ShouldReturnDocument()
        {
            // Arrange
            var document = new PdfDocument { Id = 1, FileName = "document.pdf" };
            _pdfDocumentRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(document);

            // Act
            var result = await _pdfDocumentService.GetPdfDocumentByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(document.FileName, result.FileName);
        }

        [Fact]
        public async Task UpdatePdfDocumentAsync_ShouldCallUpdateOnRepository()
        {
            // Arrange
            var document = new PdfDocument { Id = 1, FileName = "document.pdf" };

            // Act
            await _pdfDocumentService.UpdatePdfDocumentAsync(document);

            // Assert
            _pdfDocumentRepositoryMock.Verify(repo => repo.UpdateAsync(document), Times.Once);
        }
    }
}
