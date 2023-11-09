using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RahmanoEcorAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        [Range(1, int.MaxValue)]
        public double Price { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        //public string imageName { get; set; }
        public string imageUrl { get; set; }
        public int CategoryId { get; set; }
        [Display(Name = "Category Type")]
        public virtual Category Category { get; set; }

        public int ApplicationTypeId { get; set; }
        [Display(Name = "Application Type")]
        public virtual ApplicationType ApplicationType { get; set; }

        public string Is_Active { get; set; }

    }
}
