using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Pacijenti
    {
        [Required]
        [Key]
        public int PacijentID { get; set; }
        [Required]
        public int ImePacijenta { get; set; }
        [Required]
        public string PrezimePacijenta { get; set; }
        [Required]
        public int JMBG { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string BrojTelefona { get; set; }
    }
}
