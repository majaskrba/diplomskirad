using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Rezervacija
    {
        [Required]
        [Key]
        public int RezervacijaID { get; set; }
        [Required(ErrorMessage = "Polje Ime i prezime je obavezno")]
        public string Autor { get; set; }
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "Polje Broj telefona je obavezno")]
        [DisplayName("Broj telefona")]
        public string BrojTelefona { get; set; }
        [Required(ErrorMessage = "Polje Datum je obavezno")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Polje Vrijeme je obavezno")]
        [DataType(DataType.Time)]
        public DateTime Vrijeme { get; set; }
       
       public TipAnalize TipAnalize { get; set; }
        public int TipAnalizeID { get; set; }
        public Laboratorija Laboratorija { get; set; }
        public int LaboratorijaID { get; set; }
    }
}
