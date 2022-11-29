using System.ComponentModel.DataAnnotations;


#nullable disable
namespace GestorTareas.Dominio;

public class Archivo
{
    /// <summary>
    /// Identificador del archivo
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

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
}
