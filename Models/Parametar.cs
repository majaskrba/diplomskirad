using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class Parametar
    {
        [Required]
        [Key]
        public int ParametarID { get; set; }
        [Required]
        public float Vrijednost { get; set; }
    


        public int NalazID { get; set; }
     
        public Nalaz Nalaz { get; set; }

        public int SifarnikID { get; set; }
        public Sifarnik Sifarnik { get; set; }
    }
}
