using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace PeliculasAPI.Migrations
{
    public partial class SalaCineUbicacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Ubicacion",
                table: "SalasCine",
                type: "geography",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "SalasCine");
        }
    }
}
