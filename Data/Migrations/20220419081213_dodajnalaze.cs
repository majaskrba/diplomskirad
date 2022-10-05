using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace diplomskirad.Data.Migrations
{
    public partial class dodajnalaze : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nalaz",
                columns: table => new
                {
                    NalazID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imeiprezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Brojtelefona = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nalaz", x => x.NalazID);
                });

            migrationBuilder.CreateTable(
                name: "sifarnik",
                columns: table => new
                {
                    SifarnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Min = table.Column<float>(type: "real", nullable: false),
                    Max = table.Column<float>(type: "real", nullable: false),
                    Jedinica = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sifarnik", x => x.SifarnikID);
                });

            migrationBuilder.CreateTable(
                name: "parametar",
                columns: table => new
                {
                    ParametarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrijednost = table.Column<float>(type: "real", nullable: false),
                    NalazID = table.Column<int>(type: "int", nullable: false),
                    SifarnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parametar", x => x.ParametarID);
                    table.ForeignKey(
                        name: "FK_parametar_nalaz_NalazID",
                        column: x => x.NalazID,
                        principalTable: "nalaz",
                        principalColumn: "NalazID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_parametar_sifarnik_SifarnikID",
                        column: x => x.SifarnikID,
                        principalTable: "sifarnik",
                        principalColumn: "SifarnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_parametar_NalazID",
                table: "parametar",
                column: "NalazID");

            migrationBuilder.CreateIndex(
                name: "IX_parametar_SifarnikID",
                table: "parametar",
                column: "SifarnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "parametar");

            migrationBuilder.DropTable(
                name: "nalaz");

            migrationBuilder.DropTable(
                name: "sifarnik");
        }
    }
}
