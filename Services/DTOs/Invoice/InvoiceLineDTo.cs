using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.DTOs.Invoice
{
    public class InvoiceLineDTo
    {
    
        public int Id { get; set; }
        public int Quality { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }
        public double TotalPrice
        {
            get
            {
                return (double)Quality * Price;
            }
          
        }
    }
}