using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Nalaz
    {
        [Required]
        [Key]
        public int NalazID { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DisplayName("Ime i prezime")]
        public string Imeiprezime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime Vrijeme { get; set; }
        [Required]
        [DisplayName("Broj telefona")]
        public string Brojtelefona { get; set; }
        

        public List<Parametar> Parametar { get; set; } = new List<Parametar>();

    }
}
