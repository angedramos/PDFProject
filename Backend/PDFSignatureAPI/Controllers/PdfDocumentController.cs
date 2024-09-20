using AutoMapper;
using iText.Kernel.Pdf;
using iText.Signatures;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Pkcs;
using PDFSignatureAPI.Core.Domain.Interfaces;
using PDFSignatureAPI.Core.Domain.Models;
using PdfDocument = PDFSignatureAPI.Core.Domain.Models.PdfDocument;

namespace PDFSignatureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfDocumentController : ControllerBase
    {
        private readonly IPdfDocumentService _pdfDocumentService;

        public PdfDocumentController(IPdfDocumentService pdfDocumentService)
        {
            _pdfDocumentService = pdfDocumentService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PdfDocument>>> GetAllPdfDocuments()
        {
            var pdfDocuments = await _pdfDocumentService.GetAllPdfDocumentsAsync();
            return Ok(pdfDocuments);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdfDocument(IFormFile pdf)
        {
            if (pdf == null || pdf.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var fileExtension = Path.GetExtension(pdf.FileName).ToLower();
            if (fileExtension != ".pdf")
            {
                return BadRequest("File is not a PDF.");
            }

            using (var ms = new MemoryStream())
            {
                await pdf.CopyToAsync(ms);
                var fileContent = ms.ToArray();

                var newDocument = new PdfDocument
                {
                    FileName = pdf.FileName,
                    FileContent = fileContent,
                    SignerName = null,
                    IsSigned = false,
                    SignatureImage=null,
                    SignedDate = null
                };

        await _pdfDocumentService.AddPdfDocumentAsync(newDocument);
            }

            return Ok("PDF uploaded successfully.");
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadPdfDocument(int id)
        {
            // Obtener el documento PDF por ID desde el servicio
            var pdfDocument = await _pdfDocumentService.GetPdfDocumentByIdAsync(id);

            if (pdfDocument == null)
                return NotFound("PDF document not found.");

            // Retornar el archivo PDF como FileResult
            return File(pdfDocument.FileContent, "application/pdf", pdfDocument.FileName);
        }

    }
}
