using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace GestorTareas.Dominio
{
    public class Tarea
    {
        public Tarea() => this.Etiquetas = new HashSet<Etiqueta>();
        

        public Guid Id { get; set; } = new Guid();
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? CompletedAt { get; set; }

        public bool IsCompleted => CompletedAt != null;

        public ICollection<Etiqueta>? Etiquetas { get; set; }
    }
}