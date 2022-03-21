using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Analize
    {
        [Required]
        [Key]
        public int AnalizeID { get; set; }
        [Required]
        public String Naziv { get; set; }
        [Required]
        public String Opis { get; set; }
        [Required]
        public String Slika { get; set; }
    }
}
