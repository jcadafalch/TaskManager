
namespace GestorTareas.Shared;

public record TareaDTO(Guid Id, string Title, string? Content, DateTime CreatedAt, bool IsCompleted, List<TareaEtiquetaDTO> TareaEtiquetas);

public record CreateTareaRequestDTO(string Title, string Content);

public record UpdateTareaRequest(Guid Id, string? NewContent, bool IsCompleted);

public record DeleteTareaRequestDTO(Guid Id);

public record CompleteTareaRequestDTO(Guid Id);

public record SetPendingTareaRequestDTO(Guid Id);
