using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class NalazKrvi
    {
        [Required]
        [Key]
        public int NalazKrviID { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        [DisplayName("Referentni interval")]
        public string ReferentniInterval { get; set; }
        [Required]
        public string Jedinica { get; set; }
        [Required]
        public float Rezultat { get; set; }

    }
}
