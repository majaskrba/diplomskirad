using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Laboratorija
    {
        [Required]
        [Key]
        public int LaboratorijaID { get; set; }
        [Required]
        public string Lokacija { get; set; }
        public string Email { get; set; }
    }
}
