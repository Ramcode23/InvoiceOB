using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.DTOs.Invoice
{
    public class InvoiceDto
    {
        public int Id{ get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public string Decription { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public double Total { get; set; }
    }
}