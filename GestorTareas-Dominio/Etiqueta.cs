using System.ComponentModel.DataAnnotations;

#nullable disable
namespace GestorTareas.Dominio
{
    public class Etiqueta
    {   
        /// <summary>
        /// Identificador de la etiqueta
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        
        /// <summary>
        /// Nombre de la etiqueta
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Tareas que tienen esta etiqueta
        /// </summary>
        public ICollection<Tarea> Tareas { get; } = new List<Tarea>();
    }
}
