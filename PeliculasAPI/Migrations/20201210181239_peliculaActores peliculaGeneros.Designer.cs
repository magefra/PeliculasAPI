﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeliculasAPI.Data;

namespace PeliculasAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201210181239_peliculaActores peliculaGeneros")]
    partial class peliculaActorespeliculaGeneros
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("EnCines")
                        .HasColumnType("bit");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("fechaEstreno")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Peliculas");
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.PeliculaActores", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("Ordern")
                        .HasColumnType("int");

                    b.Property<string>("Personaje")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorId", "PeliculaId");

                    b.HasIndex("PeliculaId");

                    b.ToTable("PeliculaActores");
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
                });

            modelBuilder.Entity("PeliculasAPI.Entidades.PeliculaActores", b =>
                {
                    b.HasOne("PeliculasAPI.Entidades.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculasAPI.Entidades.Pelicula", "Pelicula")
                        .WithMany()
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
                        .WithMany()
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");

                    b.Navigation("Pelicula");
                });
#pragma warning restore 612, 618
        }
    }
}
