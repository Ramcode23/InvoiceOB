using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services.DTOs.Company
{
    public class CompanyCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}