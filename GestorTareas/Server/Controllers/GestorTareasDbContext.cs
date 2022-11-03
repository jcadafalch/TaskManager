using GestorTareas.Dominio;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Conexión a la base de datos
/// </summary>
namespace GestorTareas.Server.Controllers
{
    public class GestorTareasDbContext : DbContext
    {
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\SQLEXPRESS;Database=gestorTareas;Trusted_Connection=True");
        }

    }
}
