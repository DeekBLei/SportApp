using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataBaseService;

namespace DataBaseService.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20210212133124_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3");

            modelBuilder.Entity("DataTypes.Doelpunt", b =>
                {
                    b.Property<Guid>("DoelpuntId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Datum");

                    b.Property<Guid?>("SpelerPersoonId");

                    b.Property<Guid?>("TeamId");

                    b.Property<Guid?>("WedstrijdId");

                    b.HasKey("DoelpuntId");

                    b.HasIndex("SpelerPersoonId");

                    b.HasIndex("TeamId");

                    b.HasIndex("WedstrijdId");

                    b.ToTable("AlleDoelpunten");
                });

            modelBuilder.Entity("DataTypes.Persoon", b =>
                {
                    b.Property<Guid>("PersoonId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AchterNaam");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("GeboorteDatum");

                    b.Property<string>("Geslacht");

                    b.Property<int>("Leeftijd");

                    b.Property<string>("VoorNaam");

                    b.HasKey("PersoonId");

                    b.ToTable("AllePersons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Persoon");
                });

            modelBuilder.Entity("DataTypes.VoetbalTeam", b =>
                {
                    b.Property<Guid>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<string>("Geslacht");

                    b.Property<string>("Naam");

                    b.Property<Guid>("VoetbalTeamId");

                    b.HasKey("TeamId");

                    b.ToTable("AlleTeams");
                });

            modelBuilder.Entity("DataTypes.VoetbalTeamWedstrijd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("VoetbalTeamTeamId");

                    b.Property<Guid?>("WedstrijdId");

                    b.HasKey("Id");

                    b.HasIndex("VoetbalTeamTeamId");

                    b.HasIndex("WedstrijdId");

                    b.ToTable("VoetbalTeamWedstrijd");
                });

            modelBuilder.Entity("DataTypes.Wedstrijd", b =>
                {
                    b.Property<Guid>("WedstrijdId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Datum");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Geslacht");

                    b.Property<bool>("IsBezig");

                    b.Property<bool>("IsGespeeld");

                    b.Property<Guid?>("ThuisTeamTeamId");

                    b.Property<TimeSpan>("Tijd");

                    b.Property<Guid?>("UitTeamTeamId");

                    b.Property<string>("Uitslag");

                    b.HasKey("WedstrijdId");

                    b.HasIndex("ThuisTeamTeamId");

                    b.HasIndex("UitTeamTeamId");

                    b.ToTable("AlleWedstrijden");
                });

            modelBuilder.Entity("DataTypes.Coach", b =>
                {
                    b.HasBaseType("DataTypes.Persoon");

                    b.Property<int>("Ervaring");

                    b.Property<Guid?>("TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("Coach");

                    b.HasDiscriminator().HasValue("Coach");
                });

            modelBuilder.Entity("DataTypes.Speler", b =>
                {
                    b.HasBaseType("DataTypes.Persoon");

                    b.Property<int>("Ervaring");

                    b.Property<Guid?>("TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("Speler");

                    b.HasDiscriminator().HasValue("Speler");
                });

            modelBuilder.Entity("DataTypes.Doelpunt", b =>
                {
                    b.HasOne("DataTypes.Speler", "Speler")
                        .WithMany("Doelpunten")
                        .HasForeignKey("SpelerPersoonId");

                    b.HasOne("DataTypes.VoetbalTeam", "Team")
                        .WithMany("Doelpunten")
                        .HasForeignKey("TeamId");

                    b.HasOne("DataTypes.Wedstrijd", "Wedstrijd")
                        .WithMany("Doelpunten")
                        .HasForeignKey("WedstrijdId");
                });

            modelBuilder.Entity("DataTypes.VoetbalTeamWedstrijd", b =>
                {
                    b.HasOne("DataTypes.VoetbalTeam", "VoetbalTeam")
                        .WithMany("VoetbalTeamWedstrijds")
                        .HasForeignKey("VoetbalTeamTeamId");

                    b.HasOne("DataTypes.Wedstrijd", "Wedstrijd")
                        .WithMany()
                        .HasForeignKey("WedstrijdId");
                });

            modelBuilder.Entity("DataTypes.Wedstrijd", b =>
                {
                    b.HasOne("DataTypes.VoetbalTeam", "ThuisTeam")
                        .WithMany()
                        .HasForeignKey("ThuisTeamTeamId");

                    b.HasOne("DataTypes.VoetbalTeam", "UitTeam")
                        .WithMany()
                        .HasForeignKey("UitTeamTeamId");
                });

            modelBuilder.Entity("DataTypes.Coach", b =>
                {
                    b.HasOne("DataTypes.VoetbalTeam", "Team")
                        .WithMany("Coaches")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("DataTypes.Speler", b =>
                {
                    b.HasOne("DataTypes.VoetbalTeam", "Team")
                        .WithMany("TeamLeden")
                        .HasForeignKey("TeamId");
                });
        }
    }
}
