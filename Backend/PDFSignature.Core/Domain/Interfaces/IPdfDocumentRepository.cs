using PDFSignatureAPI.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSignatureAPI.Core.Domain.Interfaces
{
    public interface IPdfDocumentRepository
    {
        Task<IEnumerable<PdfDocument>> GetAllAsync();
        Task AddAsync(PdfDocument pdfDocument);
        Task<PdfDocument> GetByIdAsync(int id);
        Task UpdateAsync(PdfDocument pdfDocument);
        
    }
}
