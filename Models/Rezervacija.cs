using System;
using System.Collections.Generic;
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
        [Required]
        public DateTime DatumIVrijeme { get; set; }
    }
}
