using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorTareas.Server.Migrations
{
    /// <inheritdoc />
    public partial class ArchivosErikUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Tareas_TareaId",
                table: "Archivos");

            migrationBuilder.AlterColumn<Guid>(
                name: "TareaId",
                table: "Archivos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Archivos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Tareas_TareaId",
                table: "Archivos",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Tareas_TareaId",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Archivos");

            migrationBuilder.AlterColumn<Guid>(
                name: "TareaId",
                table: "Archivos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Tareas_TareaId",
                table: "Archivos",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id");
        }
    }
}
