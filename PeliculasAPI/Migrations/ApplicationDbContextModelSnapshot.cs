﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeliculasAPI.Data;

namespace PeliculasAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PeliculasAPI.Entidades.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Actores");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            FechaNacimiento = new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Jim Carrey"
                        },
                        new
                        {
                            Id = 6,
                            FechaNacimiento = new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Robert Downey Jr."
                        },
                        new
                        {
                            Id = 7,
                            FechaNacimiento = new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Chris Evans"
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Generos");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Nombre = "Aventura"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Animación"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Suspenso"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "Romance"
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("EnCines")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEstreno")
                        .HasColumnType("datetime2");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Peliculas");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            EnCines = true,
                            FechaEstreno = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Avengers: Endgame"
                        },
                        new
                        {
                            Id = 3,
                            EnCines = false,
                            FechaEstreno = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Avengers: Infinity Wars"
                        },
                        new
                        {
                            Id = 4,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Sonic the Hedgehog"
                        },
                        new
                        {
                            Id = 5,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Emma"
                        },
                        new
                        {
                            Id = 6,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Wonder Woman 1984"
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.PeliculaActores", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int?>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int>("Ordern")
                        .HasColumnType("int");

                    b.Property<string>("Personaje")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorId", "PeliculaId");

                    b.HasIndex("GeneroId");

                    b.HasIndex("PeliculaId");

                    b.ToTable("PeliculaActores");

                    b.HasData(
                        new
                        {
                            ActorId = 6,
                            PeliculaId = 2,
                            Ordern = 1,
                            Personaje = "Tony Stark"
                        },
                        new
                        {
                            ActorId = 7,
                            PeliculaId = 2,
                            Ordern = 2,
                            Personaje = "Steve Rogers"
                        },
                        new
                        {
                            ActorId = 6,
                            PeliculaId = 3,
                            Ordern = 1,
                            Personaje = "Tony Stark"
                        },
                        new
                        {
                            ActorId = 7,
                            PeliculaId = 3,
                            Ordern = 2,
                            Personaje = "Steve Rogers"
                        },
                        new
                        {
                            ActorId = 5,
                            PeliculaId = 4,
                            Ordern = 1,
                            Personaje = "Dr. Ivo Robotnik"
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.PeliculasGeneros", b =>
                {
                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.HasKey("GeneroId", "PeliculaId");

                    b.HasIndex("PeliculaId");

                    b.ToTable("PeliculasGeneros");

                    b.HasData(
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 2
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 2
                        },
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 3
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 3
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 4
                        },
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 5
                        },
                        new
                        {
                            GeneroId = 7,
                            PeliculaId = 5
                        },
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 6
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 6
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.PeliculaActores", b =>
                {
                    b.HasOne("PeliculasAPI.Entidades.Actor", "Actor")
                        .WithMany("PeliculaActores")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculasAPI.Entidades.Genero", null)
                        .WithMany("PeliculaActores")
                        .HasForeignKey("GeneroId");

                    b.HasOne("PeliculasAPI.Entidades.Pelicula", "Pelicula")
                        .WithMany("PeliculaActores")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Pelicula");
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.PeliculasGeneros", b =>
                {
                    b.HasOne("PeliculasAPI.Entidades.Genero", "Genero")
                        .WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculasAPI.Entidades.Pelicula", "Pelicula")
                        .WithMany("PeliculasGeneros")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");

                    b.Navigation("Pelicula");
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.Actor", b =>
                {
                    b.Navigation("PeliculaActores");
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.Genero", b =>
                {
                    b.Navigation("PeliculaActores");
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.Pelicula", b =>
                {
                    b.Navigation("PeliculaActores");

                    b.Navigation("PeliculasGeneros");
                });
#pragma warning restore 612, 618
        }
    }
}
