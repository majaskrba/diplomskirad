using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace diplomskirad.Data.Migrations
{
    public partial class dodajRezervacijeLaboratorijeAnalizeTipAnalize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tipanalize",
                columns: table => new
                {
                    TipAnalizeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipanalize", x => x.TipAnalizeID);
                });

            migrationBuilder.CreateTable(
                name: "analize",
                columns: table => new
                {
                    AnalizeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    TipAnalizeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analize", x => x.AnalizeID);
                    table.ForeignKey(
                        name: "FK_analize_tipanalize_TipAnalizeID",
                        column: x => x.TipAnalizeID,
                        principalTable: "tipanalize",
                        principalColumn: "TipAnalizeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rezervacija",
                columns: table => new
                {
                    RezervacijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipAnalizeID = table.Column<int>(type: "int", nullable: false),
                    LaboratorijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rezervacija", x => x.RezervacijaID);
                    table.ForeignKey(
                        name: "FK_rezervacija_laboratorija_LaboratorijaID",
                        column: x => x.LaboratorijaID,
                        principalTable: "laboratorija",
                        principalColumn: "LaboratorijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rezervacija_tipanalize_TipAnalizeID",
                        column: x => x.TipAnalizeID,
                        principalTable: "tipanalize",
                        principalColumn: "TipAnalizeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_analize_TipAnalizeID",
                table: "analize",
                column: "TipAnalizeID");

            migrationBuilder.CreateIndex(
                name: "IX_rezervacija_LaboratorijaID",
                table: "rezervacija",
                column: "LaboratorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_rezervacija_TipAnalizeID",
                table: "rezervacija",
                column: "TipAnalizeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analize");

            migrationBuilder.DropTable(
                name: "rezervacija");

            migrationBuilder.DropTable(
                name: "tipanalize");
        }
    }
}
