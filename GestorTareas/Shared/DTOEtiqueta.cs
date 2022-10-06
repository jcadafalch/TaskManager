
namespace GestorTareas.Shared;

public record EtiquetaDTO(
    Guid Id,
    string Title,
    DateTime CreatedAt,

    /* Mejor pedir el tipo IEnumerable (interfaz/abstracto) que List (implementación) */
    List<TareaEtiquetaDTO> TareaEtiquetas
);

public record CrearEtiquetaRequestDTO(string Name);
