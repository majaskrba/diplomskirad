using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models.ViewModels
{
    public class AnalizeViewModels
    {
        [Required]
        [Key]
        public int AnalizeID { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Slika { get; set; }
        [Required]
        [DataType(DataType.Currency)]//tip floata,neka novcana vrijednost ,uzima na kojoj mi je sistem ,
        public float Cijena { get; set; }

        public TipAnalize TipAnalize { get; set; }
        public int TipAnalizeID { get; set; }
    }
}
