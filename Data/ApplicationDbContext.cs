using diplomskirad.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using diplomskirad.ViewModels;

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
        public DbSet<diplomskirad.ViewModels.InformacijaViewModels> InformacijaViewModels { get; set; }
               
            
        }
}
