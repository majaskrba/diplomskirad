using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Komentar
    {
        [Required]
        [Key]
        public int KomentarID { get; set; }
        [Required]
        public string Sadrzaj { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string DatumIVrijemeObjave { get; set; }

        public Informacija Informacija { get; set; }
        public int InformacijaID { get; set; }
    }
}
