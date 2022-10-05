
namespace GestorTareas.Shared;

public record TareaDTO(Guid Id, string Title, string? Content, DateTime CreatedAt, bool IsCompleted, ICollection<EtiquetaDTO>? Etiquetas);

public record CrearTareaRequestDTO(string Title, string Content);
