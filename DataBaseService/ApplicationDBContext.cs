using DataTypes;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataBaseService
{
    
    internal class ApplicationDBContext : DbContext
    {

        public DbSet<Persoon> AllePersons { get; set; }
        public DbSet<VoetbalTeam> AlleTeams { get;            set; }
        public DbSet<Speler> AlleSpelers { get; set; }
        public DbSet<Coach> AlleCoaches { get; set; }
        public DbSet<Wedstrijd> AlleWedstrijden { get; set; }
        public DbSet<Doelpunt> AlleDoelpunten { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Test33.db");
            base.OnConfiguring(optionsBuilder);
            
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Team>().HasMany<Wedstrijd>(t=>t.Wedstrijden).WithMany(c=>c.)

        }


    }
}
