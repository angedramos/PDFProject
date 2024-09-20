using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PDFSignatureAPI.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSignatureAPI.Infrastructure.Persistence
{
    public class PDFSignatureDbContext : DbContext
    {
        public PDFSignatureDbContext(DbContextOptions<PDFSignatureDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PdfDocument>().ToTable("PdfDocuments");
            
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<PdfDocument> PdfDocuments { get; set; }
    }
}
