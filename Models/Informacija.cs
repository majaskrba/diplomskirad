using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Informacija
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
        [DisplayName("Datum dogadjaja")]
        public DateTime DatumDogadjaja { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime Od { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime Do { get; set; }
        [Required]
        [Display(Name ="Datum i vrijeme objave")]
        public DateTime DatumIVrijemeObjave { get; set; }
        public string Autor { get; set; }
        [Required]
        public string Slika { get; set; }

        public List<Komentar> Komentar { get; set; } = new List<Komentar>();

    }
}
