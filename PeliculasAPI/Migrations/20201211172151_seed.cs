using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasAPI.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fechaEstreno",
                table: "Peliculas",
                newName: "FechaEstreno");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "PeliculaActores",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Actores",
                columns: new[] { "Id", "FechaNacimiento", "Foto", "Nombre" },
                values: new object[,]
                {
                    { 5, new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jim Carrey" },
                    { 6, new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Robert Downey Jr." },
                    { 7, new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chris Evans" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 4, "Aventura" },
                    { 5, "Animación" },
                    { 6, "Suspenso" },
                    { 7, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "Id", "EnCines", "FechaEstreno", "Poster", "Titulo" },
                values: new object[,]
                {
                    { 2, true, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Avengers: Endgame" },
                    { 3, false, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Avengers: Infinity Wars" },
                    { 4, false, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonic the Hedgehog" },
                    { 5, false, new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emma" },
                    { 6, false, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wonder Woman 1984" }
                });

            migrationBuilder.InsertData(
                table: "PeliculaActores",
                columns: new[] { "ActorId", "PeliculaId", "GeneroId", "Ordern", "Personaje" },
                values: new object[,]
                {
                    { 6, 2, null, 1, "Tony Stark" },
                    { 7, 2, null, 2, "Steve Rogers" },
                    { 6, 3, null, 1, "Tony Stark" },
                    { 7, 3, null, 2, "Steve Rogers" },
                    { 5, 4, null, 1, "Dr. Ivo Robotnik" }
                });

            migrationBuilder.InsertData(
                table: "PeliculasGeneros",
                columns: new[] { "GeneroId", "PeliculaId" },
                values: new object[,]
                {
                    { 6, 2 },
                    { 4, 2 },
                    { 6, 3 },
                    { 4, 3 },
                    { 4, 4 },
                    { 6, 5 },
                    { 7, 5 },
                    { 6, 6 },
                    { 4, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaActores_GeneroId",
                table: "PeliculaActores",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaActores_Generos_GeneroId",
                table: "PeliculaActores",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaActores_Generos_GeneroId",
                table: "PeliculaActores");

            migrationBuilder.DropIndex(
                name: "IX_PeliculaActores_GeneroId",
                table: "PeliculaActores");

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PeliculaActores",
                keyColumns: new[] { "ActorId", "PeliculaId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "PeliculaActores",
                keyColumns: new[] { "ActorId", "PeliculaId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "PeliculaActores",
                keyColumns: new[] { "ActorId", "PeliculaId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "PeliculaActores",
                keyColumns: new[] { "ActorId", "PeliculaId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "PeliculaActores",
                keyColumns: new[] { "ActorId", "PeliculaId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "GeneroId", "PeliculaId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "GeneroId", "PeliculaId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "GeneroId", "PeliculaId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "GeneroId", "PeliculaId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "GeneroId", "PeliculaId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "GeneroId", "PeliculaId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "GeneroId", "PeliculaId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "GeneroId", "PeliculaId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "GeneroId", "PeliculaId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "Actores",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actores",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Actores",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "PeliculaActores");

            migrationBuilder.RenameColumn(
                name: "FechaEstreno",
                table: "Peliculas",
                newName: "fechaEstreno");
        }
    }
}
