
namespace GestorTareas.Shared;

public record EtiquetaDTO(
    Guid Id,
    string Name,

    /* Mejor pedir el tipo IEnumerable (interfaz/abstracto) que List (implementación) */
    IEnumerable<TareaEtiquetaDTO> TareaEtiquetas
);

public record CreateEtiquetaRequestDTO(string Name);

public record UpdateEtiquetaRequestDTO(Guid Id, string NewName);

public record DeleteEtiquetaRequestDTO(Guid Id);
