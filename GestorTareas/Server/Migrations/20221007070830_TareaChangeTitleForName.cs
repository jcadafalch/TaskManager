using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorTareas.Server.Migrations
{
    public partial class TareaChangeTitleForName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Etiquetas",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Etiquetas",
                newName: "Title");
        }
    }
}
