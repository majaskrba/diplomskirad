using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models.ViewModels
{
    public class TipAnalizeViewModels
    {
        [Required]
        [Key]
        public int TipAnalizeID { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Slika { get; set; }

        public List<Analize> Analiza { get; set; } = new List<Analize>();
        public List<Rezervacija> Rezervacija { get; set; } = new List<Rezervacija>();
    }
}
