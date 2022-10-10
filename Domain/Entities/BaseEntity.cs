using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Indentity;

namespace Domain.Entities
{
      public class BaseEntity
    {
         //TODO:Modify model
        [Required]
        [Key]
        public int Id { get; set; }
       
        public ApplicationUser CreatedBy { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ApplicationUser? UpdatedBy { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public ApplicationUser? DeletedBy { get; set; } 
        public DateTime? DeleteteAt { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}