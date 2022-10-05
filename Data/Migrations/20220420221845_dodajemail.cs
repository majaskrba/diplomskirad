using Microsoft.EntityFrameworkCore.Migrations;

namespace diplomskirad.Data.Migrations
{
    public partial class dodajemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "rezervacija",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "rezervacija");
        }
    }
}
