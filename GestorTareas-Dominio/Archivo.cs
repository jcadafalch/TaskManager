using System.ComponentModel.DataAnnotations;


#nullable disable
namespace GestorTareas.Dominio;

public sealed class Archivo
{
    /// <summary>
    /// Identificador del archivo
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Nombre del archivo
    /// </summary>
    [Required]
    public string Name { get; set; } = default(string);

    /// <summary>
    /// Contenido del archivo
    /// </summary>
    [Required]
    public byte[] File { get; set; } = default(byte[]);

    /// <summary>
    /// Extensión del archivo
    /// </summary>
    [Required]
    public string Extension { get; set; } = default(string);

    /// <summary>
    /// Fecha en que se subio el archivo
    /// </summary>
    public DateTime UploadedAt { get; set; } = DateTime.Now;

    public Guid TareaId { get; set; }
    public Tarea Tarea { get; set; }

}
