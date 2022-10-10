using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Indentity
{
     public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }


        
        [InverseProperty("CreatedBy")]
        public ICollection<Company> CompanysCreatedBy { get; set; } = new List<Company>();

        [InverseProperty("UpdatedBy")]
        public ICollection<Company> CompanysUpdatedBy { get; set; } = new List<Company>();

        [InverseProperty("DeletedBy")]
        public ICollection<Company> CompanysDeletedBy { get; set; } = new List<Company>();



        [InverseProperty("CreatedBy")]
        public ICollection<Invoice> InvoicesCreatedBy { get; set; } = new List<Invoice>();

        [InverseProperty("UpdatedBy")]
        public ICollection<Invoice> InvoicesUpdatedBy { get; set; } = new List<Invoice>();

        [InverseProperty("DeletedBy")]
        public ICollection<Invoice> InvoicesDeletedBy { get; set; } = new List<Invoice>();

    }
}