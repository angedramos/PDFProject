using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSignatureAPI.Core.Domain.Models
{
    public class PdfDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public bool IsSigned { get; set; }
        public string? SignerName { get; set; }
        public DateTime? SignedDate { get; set; }
        public byte[]? SignatureImage { get; set; }
    }
}
