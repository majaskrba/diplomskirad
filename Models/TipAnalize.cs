using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class TipAnalize
    {
        [Required]
        [Key]
        public int TipAnalizeID { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        public string Slika { get; set; }

        public List<Analize> Analiza { get; set; } = new List<Analize>();
        public List<Rezervacija> Rezervacija { get; set; } = new List<Rezervacija>();
    }
}
