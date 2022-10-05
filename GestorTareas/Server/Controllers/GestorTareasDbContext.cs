using GestorTareas.Dominio;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Controllers
{
    public class GestorTareasDbContext: DbContext
    {
        public GestorTareasDbContext(DbContextOptions<GestorTareasDbContext> options) : base(options)
        {
        }
        public DbSet<Tarea> Tareas { get; set; } = default;
        public DbSet<Etiqueta> Etiquetas { get; set; } = default;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\SQLEXPRESS;Database=gestorTareas;Trusted_Connection=True");
        }

       
    }
}
