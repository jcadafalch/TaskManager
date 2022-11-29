using GestorTareas.Dominio;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Conexión a la base de datos
/// </summary>
namespace GestorTareas.Server.Controllers
{
    public class GestorTareasDbContext : DbContext
    {
        public DbSet<Tarea> Tareas { get; set; } = default!;
        public DbSet<Etiqueta> Etiquetas { get; set; } = default!;
        public DbSet<Archivo> Archivos { get; set; } = default!;

        public GestorTareasDbContext(DbContextOptions<GestorTareasDbContext> options) : base(options) { }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\SQLEXPRESS;Database=gestorTareas;Trusted_Connection=True");
        }*/

    }
}
