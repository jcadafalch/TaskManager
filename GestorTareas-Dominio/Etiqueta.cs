using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Dominio;

#nullable disable

public sealed class Etiqueta
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
