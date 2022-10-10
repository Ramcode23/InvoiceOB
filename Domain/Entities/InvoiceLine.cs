using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InvoiceLine
    {
        [Key]
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
        public Invoice Invoice { get; set; } = new Invoice();

    }
}