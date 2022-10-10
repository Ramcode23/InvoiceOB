using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.DTOs.Invoice
{
    public class InvoiceCreateDTo
    {
   
        public string Decription { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLineDTo> InvoiceLines { get; set; } = new List<InvoiceLineDTo>();
    }
}