
namespace GestorTareas.Shared;

public record TareaDTO(
    Guid Id,
    string Title,
    string? Content,
    DateTime CreatedAt,
    bool IsCompleted,

    /* Mejor pedir el tipo IEnumerable (interfaz/abstracto) que List (implementación) */
    IEnumerable<TareaEtiquetaDTO> TareaEtiquetas);

public record CreateTareaRequestDTO(string Title, string Content);

public record UpdateTareaRequest(Guid Id, string? NewContent);

public record DeleteTareaRequestDTO(Guid Id);

public record CompleteTareaRequestDTO(Guid Id);

public record SetPendingTareaRequestDTO(Guid Id);

public record AddEtiquetaRequestDTO(Guid IdTarea, Guid IdEtiqueta);

public record RemoveEtiquetaRequestDTO(Guid IdTarea, Guid IdEtiqueta);