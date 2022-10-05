using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Sifarnik
    {
        [Required]
        [Key]
        public int SifarnikID { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public float Min { get; set; }
        [Required]
        public float Max { get; set; }
        [Required]
        public string Jedinica { get; set; }

    }
}
