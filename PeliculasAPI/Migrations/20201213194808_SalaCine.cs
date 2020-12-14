using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasAPI.Migrations
{
    public partial class SalaCine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalasCine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalasCine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeliculasSalasCine",
                columns: table => new
                {
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    SalaCineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasSalasCine", x => new { x.SalaCineId, x.PeliculaId });
                    table.ForeignKey(
                        name: "FK_PeliculasSalasCine_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculasSalasCine_SalasCine_SalaCineId",
                        column: x => x.SalaCineId,
                        principalTable: "SalasCine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasCine_PeliculaId",
                table: "PeliculasSalasCine",
                column: "PeliculaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculasSalasCine");

            migrationBuilder.DropTable(
                name: "SalasCine");
        }
    }
}
