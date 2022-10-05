using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diplomskirad.Models
{
    public class UserRoles
    {
        [Key]
        public string KorisnickiID { get; set; }
        public string KorisnickoIme { get; set; }

        public string Email { get; set; }
        [DisplayName("Broj telefona")]
        public string BrojTelefona { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }
    }
}
