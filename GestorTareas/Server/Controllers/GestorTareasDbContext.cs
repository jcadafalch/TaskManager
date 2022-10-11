using GestorTareas.Dominio;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Controllers
{
    public class GestorTareasDbContext : DbContext
    {
        public GestorTareasDbContext(DbContextOptions<GestorTareasDbContext> options) : base(options)
        {
        }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\SQLEXPRESS;Database=gestorTareas;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Tarea>()
                .HasMany(t => t.Etiquetas)
                .WithMany(t => t.Tareas)
                .UsingEntity<Dictionary<string, object>>(
                "EtiquetaTarea",
                j => j.HasOne<Etiqueta>().WithMany().HasForeignKey("EtiquetasId").HasConstraintName("FK_EtiquetaTarea_Etiquetas_EtiquetasId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Tarea>().WithMany().HasForeignKey("TareasId").HasConstraintName("FK_EtiquetaTarea_Tareas_TareasId").OnDelete(DeleteBehavior.Cascade)
                );*/
        }


    }
}
