using PDFSignatureAPI.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSignatureAPI.Core.Domain.Interfaces
{
    public interface IPdfDocumentService
    {
        Task<IEnumerable<PdfDocument>> GetAllPdfDocumentsAsync();
        Task AddPdfDocumentAsync(PdfDocument pdfDocument);
        Task<PdfDocument> GetPdfDocumentByIdAsync(int id);
        Task UpdatePdfDocumentAsync(PdfDocument pdfDocument);
    }
}
