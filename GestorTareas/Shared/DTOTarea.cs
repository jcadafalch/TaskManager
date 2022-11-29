
namespace GestorTareas.Shared;

public record TareaDTO(
    Guid Id,
    string Title,
    string? Content,
    DateTime CreatedAt,
    bool IsCompleted,

    /* Mejor pedir el tipo IEnumerable (interfaz/abstracto) que List (implementación) */
    IEnumerable<EtiquetaDTO> Etiquetas);

public record CreateTareaRequestDTO(string Title, string Content);

public record UpdateTareaRequestDTO(Guid Id, string? NewContent);

public record IdRequestDTO(Guid Id);

public record ManageEtiquetaTareaRequestDTO(Guid IdTarea, Guid IdEtiqueta);

public record AddArchivoTareaRequestDTO(Guid IdTarea, byte[] File, string Extension);

public record RemoveArchivoTareaRequestDTO(Guid IdTarea, Guid IdArchivo);