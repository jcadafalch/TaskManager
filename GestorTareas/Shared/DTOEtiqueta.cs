
namespace GestorTareas.Shared;

public record EtiquetaDTO(Guid Id, string Title, DateTime CreatedAt, List<TareaEtiquetaDTO> TareaEtiquetas);

public record CrearEtiquetaRequestDTO(string Name);
