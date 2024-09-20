using Microsoft.EntityFrameworkCore;
using PDFSignatureAPI.Core.Domain.Interfaces;
using PDFSignatureAPI.Core.Domain.Models;
using PDFSignatureAPI.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSignatureAPI.Infrastructure.Repositories
{
    public class PdfDocumentRepository : IPdfDocumentRepository
    {
        private readonly PDFSignatureDbContext _context;

        public PdfDocumentRepository(PDFSignatureDbContext context)
        {
                _context = context;
        }

        public async Task<IEnumerable<PdfDocument>> GetAllAsync()
        {
            return await _context.PdfDocuments.ToListAsync();
        }

        public async Task AddAsync(PdfDocument pdfDocument)
        {
            _context.PdfDocuments.Add(pdfDocument);
            await _context.SaveChangesAsync();
        }

        public async Task<PdfDocument> GetByIdAsync(int id)
        {
            return await _context.PdfDocuments.FindAsync(id);
        }

        public async Task UpdateAsync(PdfDocument pdfDocument)
        {
            _context.PdfDocuments.Update(pdfDocument);
            await _context.SaveChangesAsync();
        }
    }
}
