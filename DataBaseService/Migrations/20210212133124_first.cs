using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseService.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlleTeams",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Geslacht = table.Column<string>(nullable: true),
                    Naam = table.Column<string>(nullable: true),
                    VoetbalTeamId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlleTeams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "AllePersons",
                columns: table => new
                {
                    PersoonId = table.Column<Guid>(nullable: false),
                    AchterNaam = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    GeboorteDatum = table.Column<DateTime>(nullable: false),
                    Geslacht = table.Column<string>(nullable: true),
                    Leeftijd = table.Column<int>(nullable: false),
                    VoorNaam = table.Column<string>(nullable: true),
                    Ervaring = table.Column<int>(nullable: true),
                    TeamId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllePersons", x => x.PersoonId);
                    table.ForeignKey(
                        name: "FK_AllePersons_AlleTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "AlleTeams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllePersons_AlleTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "AlleTeams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlleWedstrijden",
                columns: table => new
                {
                    WedstrijdId = table.Column<Guid>(nullable: false),
                    Datum = table.Column<DateTimeOffset>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Geslacht = table.Column<string>(nullable: true),
                    IsBezig = table.Column<bool>(nullable: false),
                    IsGespeeld = table.Column<bool>(nullable: false),
                    ThuisTeamTeamId = table.Column<Guid>(nullable: true),
                    Tijd = table.Column<TimeSpan>(nullable: false),
                    UitTeamTeamId = table.Column<Guid>(nullable: true),
                    Uitslag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlleWedstrijden", x => x.WedstrijdId);
                    table.ForeignKey(
                        name: "FK_AlleWedstrijden_AlleTeams_ThuisTeamTeamId",
                        column: x => x.ThuisTeamTeamId,
                        principalTable: "AlleTeams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlleWedstrijden_AlleTeams_UitTeamTeamId",
                        column: x => x.UitTeamTeamId,
                        principalTable: "AlleTeams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlleDoelpunten",
                columns: table => new
                {
                    DoelpuntId = table.Column<Guid>(nullable: false),
                    Datum = table.Column<DateTimeOffset>(nullable: false),
                    SpelerPersoonId = table.Column<Guid>(nullable: true),
                    TeamId = table.Column<Guid>(nullable: true),
                    WedstrijdId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlleDoelpunten", x => x.DoelpuntId);
                    table.ForeignKey(
                        name: "FK_AlleDoelpunten_AllePersons_SpelerPersoonId",
                        column: x => x.SpelerPersoonId,
                        principalTable: "AllePersons",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlleDoelpunten_AlleTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "AlleTeams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlleDoelpunten_AlleWedstrijden_WedstrijdId",
                        column: x => x.WedstrijdId,
                        principalTable: "AlleWedstrijden",
                        principalColumn: "WedstrijdId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoetbalTeamWedstrijd",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    VoetbalTeamTeamId = table.Column<Guid>(nullable: true),
                    WedstrijdId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoetbalTeamWedstrijd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoetbalTeamWedstrijd_AlleTeams_VoetbalTeamTeamId",
                        column: x => x.VoetbalTeamTeamId,
                        principalTable: "AlleTeams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoetbalTeamWedstrijd_AlleWedstrijden_WedstrijdId",
                        column: x => x.WedstrijdId,
                        principalTable: "AlleWedstrijden",
                        principalColumn: "WedstrijdId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlleDoelpunten_SpelerPersoonId",
                table: "AlleDoelpunten",
                column: "SpelerPersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_AlleDoelpunten_TeamId",
                table: "AlleDoelpunten",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AlleDoelpunten_WedstrijdId",
                table: "AlleDoelpunten",
                column: "WedstrijdId");

            migrationBuilder.CreateIndex(
                name: "IX_AllePersons_TeamId",
                table: "AllePersons",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_VoetbalTeamWedstrijd_VoetbalTeamTeamId",
                table: "VoetbalTeamWedstrijd",
                column: "VoetbalTeamTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_VoetbalTeamWedstrijd_WedstrijdId",
                table: "VoetbalTeamWedstrijd",
                column: "WedstrijdId");

            migrationBuilder.CreateIndex(
                name: "IX_AlleWedstrijden_ThuisTeamTeamId",
                table: "AlleWedstrijden",
                column: "ThuisTeamTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AlleWedstrijden_UitTeamTeamId",
                table: "AlleWedstrijden",
                column: "UitTeamTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlleDoelpunten");

            migrationBuilder.DropTable(
                name: "VoetbalTeamWedstrijd");

            migrationBuilder.DropTable(
                name: "AllePersons");

            migrationBuilder.DropTable(
                name: "AlleWedstrijden");

            migrationBuilder.DropTable(
                name: "AlleTeams");
        }
    }
}
