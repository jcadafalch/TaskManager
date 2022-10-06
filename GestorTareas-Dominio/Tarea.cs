using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

#nullable disable
namespace GestorTareas.Dominio
{
    public class Tarea
    {
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string Title { get; set; } = default(string);
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted => CompletedAt != null;

        public ICollection<Etiqueta> Etiquetas { get; set; }
    }
}