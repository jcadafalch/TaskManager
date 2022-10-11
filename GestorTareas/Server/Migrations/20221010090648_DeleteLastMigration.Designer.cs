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
    [Migration("20221010090648_DeleteLastMigration")]
    partial class DeleteLastMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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
                        .IsRequired()
                        .HasConstraintName("FK_EtiquetaTarea_Etiquetas_EtiquetasId");

                    b.HasOne("GestorTareas.Dominio.Tarea", null)
                        .WithMany()
                        .HasForeignKey("TareasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_EtiquetaTarea_Tareas_TareasId");
                });
#pragma warning restore 612, 618
        }
    }
}
