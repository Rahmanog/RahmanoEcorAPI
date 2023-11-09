using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RahmanoEcorAPI.Models
{
    public class ApplicationType
    {
        [Key]
        public int TypeId { get; set; }
        [Required]
        public string TypeName { get; set; }
        public string TypeDescripton { get; set; }

        public string Is_Active { get; set; }
    }
}
