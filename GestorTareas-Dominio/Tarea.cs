using System.ComponentModel.DataAnnotations;

#nullable disable
namespace GestorTareas.Dominio
{
    public class Tarea
    {
        /// <summary>
        /// Identificador de loa tarea
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nombre de la tarea
        /// </summary>
        [Required]
        public string Title { get; set; } = default(string);

        /// <summary>
        /// Contenido de la tarea
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Fecha de creación de la tarea
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Fecha de completación de la tarea
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Estado de la tarea 
        /// </summary>
        public bool IsCompleted => CompletedAt != null;

        /// <summary>
        /// Etiquetas asignadas a esta tarea
        /// </summary>
        public ICollection<Etiqueta> Etiquetas { get; } = new List<Etiqueta>();

        /// <summary>
        /// Archivos que perteneces a esta tarea
        /// </summary>
        public ICollection<Archivo> Archivos { get; } = new List<Archivo>();

        #region Métodos

        public void AddEtiqueta(Etiqueta etiqueta) =>  Etiquetas.Add(etiqueta);
        
        public void RemoveEtiqueta(Etiqueta etiqueta) => Etiquetas.Remove(etiqueta);

        public void AddArchivo(Archivo archivo) => Archivos.Add(archivo);

        public void RemoveArchivo(Guid archivoId) => Archivos.Remove(Archivos.FirstOrDefault(a => a.Id == archivoId));

        #endregion
    }
}