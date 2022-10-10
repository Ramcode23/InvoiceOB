using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public string Decription { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

        public double Total
        {
            get
            {
                return InvoiceLines.Sum(line => line.TotalPrice);

            }
            set
            {
                InvoiceLines.Sum(line => line.TotalPrice);

            }
        }
    }
}