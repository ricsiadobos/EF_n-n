using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF6.Domain;

namespace EF6.Data
{
    public class FootballLeagueDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlServer(@"data source=DESKTOP-9IINTC5\SQLEXPRESS;initial catalog=Football_EF6_Core;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging();

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()       // kiválsztott típusú model konfigurálása (pl. db. kapcsolathoz)
                 .HasMany(m => m.HomeMatches) // hazai meccsek   (olyam entitás amely más típusú kollekciókat tartalmaz)
                 .WithOne(m => m.HomeTeam)     //hazai csapat
                 .HasForeignKey(m => m.HomeTeamId) // hazai csapat id
                 .IsRequired()                     // megszabott szabályok vizsgálata
                 .OnDelete(DeleteBehavior.Restrict);// betartás esete
                                                   // Principal itt a hazai csapat. Ha törlődik a hazai csapat
                                                   // akkor törlődnek vele együtt az Ő hazai meccsei is!

            modelBuilder.Entity<Team>()       // kiválsztott típusú model konfigurálása (pl. db. kapcsolathoz)
                 .HasMany(m => m.AwayMatches) // idegen meccsek   
                 .WithOne(m => m.AwayTeam)     //idegen csapat
                 .HasForeignKey(m => m.AwayTeamId) // idegen csapat id
                 .IsRequired()                     // megszabott szabályok vizsgálata
                 .OnDelete(DeleteBehavior.Restrict);// betartás esete
                                                   // Principal itt a hazai csapat. Ha törlődik a idegen csapat
                                                   // akkor törlődnek vele együtt az Ő idegen meccsei is!
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }



    }
}
