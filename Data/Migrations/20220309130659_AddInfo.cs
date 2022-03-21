using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace diplomskirad.Data.Migrations
{
    public partial class AddInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "informacija",
                columns: table => new
                {
                    InformacijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumDogadjaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Od = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Do = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIVrijemeObjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_informacija", x => x.InformacijaID);
                });

            migrationBuilder.CreateTable(
                name: "komentar",
                columns: table => new
                {
                    KomentarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumIVrijemeObjave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformacijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_komentar", x => x.KomentarID);
                    table.ForeignKey(
                        name: "FK_komentar_informacija_InformacijaID",
                        column: x => x.InformacijaID,
                        principalTable: "informacija",
                        principalColumn: "InformacijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_komentar_InformacijaID",
                table: "komentar",
                column: "InformacijaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "komentar");

            migrationBuilder.DropTable(
                name: "informacija");
        }
    }
}
