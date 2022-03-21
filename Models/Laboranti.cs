using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Laboranti
    {
        [Required]
        [Key]
        public int LaborantID { get; set; }
        [Required]
        [Display(Name="Ime laboranta:")]
        public string ImeLaboranta { get; set; }
        [Required]
        [Display(Name="Prezime laboranta:")]
        public string PrezimeLaboranta { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name="Broj telefona:")]
        public int BrojTelefona { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Zaposlen od:")]
        public DateTime OD { get; set; }
       
        [Display(Name="Zaposlen do:")]
        [DataType(DataType.Date)]
        public DateTime DO { get; set; }
    }
}
