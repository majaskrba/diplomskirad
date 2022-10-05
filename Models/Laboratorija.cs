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
        public string Naziv { get; set; }
        [Required]
        public string Lokacija { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public List<Rezervacija> Rezervacija { get; set; } = new List<Rezervacija>();
    }
}
