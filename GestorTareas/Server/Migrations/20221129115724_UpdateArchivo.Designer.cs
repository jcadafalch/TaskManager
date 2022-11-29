﻿// <auto-generated />
using System;
using GestorTareas.Server.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestorTareas.Server.Migrations
{
    [DbContext(typeof(GestorTareasDbContext))]
    [Migration("20221129115724_UpdateArchivo")]
    partial class UpdateArchivo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EtiquetaTarea", b =>
                {
                    b.Property<Guid>("EtiquetasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TareasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EtiquetasId", "TareasId");

                    b.HasIndex("TareasId");

                    b.ToTable("EtiquetaTarea");
                });

            modelBuilder.Entity("GestorTareas.Dominio.Archivo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("TareaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TareaId");

                    b.ToTable("Archivos");
                });

            modelBuilder.Entity("GestorTareas.Dominio.Etiqueta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Etiquetas");
                });

            modelBuilder.Entity("GestorTareas.Dominio.Tarea", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("EtiquetaTarea", b =>
                {
                    b.HasOne("GestorTareas.Dominio.Etiqueta", null)
                        .WithMany()
                        .HasForeignKey("EtiquetasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestorTareas.Dominio.Tarea", null)
                        .WithMany()
                        .HasForeignKey("TareasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GestorTareas.Dominio.Archivo", b =>
                {
                    b.HasOne("GestorTareas.Dominio.Tarea", null)
                        .WithMany("Archivos")
                        .HasForeignKey("TareaId");
                });

            modelBuilder.Entity("GestorTareas.Dominio.Tarea", b =>
                {
                    b.Navigation("Archivos");
                });
#pragma warning restore 612, 618
        }
    }
}
