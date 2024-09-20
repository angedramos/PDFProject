
using iText.Commons.Actions.Contexts;
using iText.Signatures;
using PDFSignatureAPI.Core.Domain.Interfaces;
using PDFSignatureAPI.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PDFSignatureAPI.Core.Application.Services
{
    public class PdfDocumentService : IPdfDocumentService
    {
        private readonly IPdfDocumentRepository _pdfDocumentRepository;

        public PdfDocumentService(IPdfDocumentRepository pdfDocumentRepository)
        {
                _pdfDocumentRepository = pdfDocumentRepository;
        }

        public async Task<IEnumerable<PdfDocument>> GetAllPdfDocumentsAsync()
        {
            return await _pdfDocumentRepository.GetAllAsync();
        }

        public async Task AddPdfDocumentAsync(PdfDocument pdfDocument)
        {
            await _pdfDocumentRepository.AddAsync(pdfDocument);
        }
        public async Task<PdfDocument> GetPdfDocumentByIdAsync(int id)
        {
            return await _pdfDocumentRepository.GetByIdAsync(id);
        }
        public async Task UpdatePdfDocumentAsync(PdfDocument pdfDocument)
        {
            await _pdfDocumentRepository.UpdateAsync(pdfDocument);
        }
    }
}
