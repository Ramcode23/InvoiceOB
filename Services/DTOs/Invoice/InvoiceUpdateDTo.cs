using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.DTOs.Invoice
{
    public class InvoiceUpdateDTo
    {
        public int Id { get; set; }

        public string Decription { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public double Total { get; set; }
        public List<InvoiceLineDTo> InvoiceLines { get; set; } = new List<InvoiceLineDTo>();
    }
}