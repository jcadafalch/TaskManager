using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorTareas.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArchivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivo_Tareas_TareaId",
                table: "Archivo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Archivo",
                table: "Archivo");

            migrationBuilder.RenameTable(
                name: "Archivo",
                newName: "Archivos");

            migrationBuilder.RenameIndex(
                name: "IX_Archivo_TareaId",
                table: "Archivos",
                newName: "IX_Archivos_TareaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Archivos",
                table: "Archivos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Tareas_TareaId",
                table: "Archivos",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Tareas_TareaId",
                table: "Archivos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Archivos",
                table: "Archivos");

            migrationBuilder.RenameTable(
                name: "Archivos",
                newName: "Archivo");

            migrationBuilder.RenameIndex(
                name: "IX_Archivos_TareaId",
                table: "Archivo",
                newName: "IX_Archivo_TareaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Archivo",
                table: "Archivo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivo_Tareas_TareaId",
                table: "Archivo",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id");
        }
    }
}
