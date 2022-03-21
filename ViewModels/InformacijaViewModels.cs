using diplomskirad.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.ViewModels
{
    public class InformacijaViewModels
    {
        [Required]
        [Key]
        public int InformacijaID { get; set; }
        [Required]
        public string Naslov { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatumDogadjaja { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime Od { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime Do { get; set; }
        [Required]
        public DateTime DatumIVrijemeObjave { get; set; }
        public string Autor { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Slika { get; set; }

        public List<Komentar> Komentar { get; set; } = new List<Komentar>();
    }
}
