using GestorTareas.Dominio;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Controllers
{
    public class GestorTareasDbContext: DbContext
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
            modelBuilder.Entity<TareaEtiqueta>()
                .HasKey(t => new {t.TareaId, t.EtiquetaId});

            modelBuilder.Entity<TareaEtiqueta>()
                .HasOne(t => t.Tarea)
                .WithMany(te => te.TareaEtiquetas)
                .HasForeignKey(t => t.TareaId);

            modelBuilder.Entity<TareaEtiqueta>()
                .HasOne(e => e.Etiqueta)
                .WithMany(te => te.TareaEtiquetas)
                .HasForeignKey(e => e.EtiquetaId);
        }

       
    }
}
