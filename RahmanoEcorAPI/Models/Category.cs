using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RahmanoEcorAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string CategoryName { get; set; }

        [DisplayName("Description")]
        public string CategoryDescription { get; set; }
        
        [DisplayName("Display Order")]
        [Range(1, int.MaxValue, ErrorMessage = "Display Order must be graeter than 0")]
        public int DisplayOrder { get; set; }

        [DefaultValue("1")]
        public string Is_Active { get; set; }
    }
}
