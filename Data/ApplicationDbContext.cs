using diplomskirad.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using diplomskirad.Models.ViewModels;

namespace diplomskirad.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Informacija> informacija { get; set; }  //model informacije prebacujemo u tabelu u bazu
        public DbSet<Komentar> komentar { get; set; }
        public DbSet<Laboratorija> laboratorija { get; set; }    
        public DbSet<Analize> analize { get; set; }
        public DbSet<TipAnalize> tipanalize { get; set; }
        public DbSet<Rezervacija> rezervacija { get; set; }
        public DbSet<Nalaz> nalaz { get; set; }
        public DbSet<Parametar> parametar { get; set; }
        public DbSet<Sifarnik> sifarnik { get; set; }
        public DbSet<diplomskirad.Models.UserRoles> User { get; set; }
        public DbSet<diplomskirad.Models.RolesViewModel> RolesViewModel { get; set; }
    }
}
