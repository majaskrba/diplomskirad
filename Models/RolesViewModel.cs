using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class RolesViewModel
    {
        [Key]
        public string UlogeID { get; set; }
        public string UlogeIme { get; set; }
        public bool Selektovano { get; set; }
    }
}
