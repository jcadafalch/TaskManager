
namespace GestorTareas.Shared;

public record TareaDTO(Guid Id, string Title, string? Content, DateTime CreatedAt, bool IsCompleted, List<TareaEtiquetaDTO> TareaEtiquetas)
{
    private List<GestorTareas.Dominio.TareaEtiqueta> tareaEtiquetas;

    public TareaDTO(Guid id, string title, string content, DateTime createdAt, bool isCompleted, List<GestorTareas.Dominio.TareaEtiqueta> tareaEtiquetas)
    {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        IsCompleted = isCompleted;
        this.tareaEtiquetas = tareaEtiquetas;
    }
}

public record CreateTareaRequestDTO(string Title, string Content);

public record UpdateTareaRequest(Guid Id, string? NewContent, bool IsCompleted);

public record DeleteTareaRequestDTO(Guid Id);

public record CompleteTareaRequestDTO(Guid Id);

public record SetPendingTareaRequestDTO(Guid Id);
