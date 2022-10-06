
namespace GestorTareas.Shared;

public record EtiquetaDTO(Guid Id, string Title, DateTime CreatedAt, HashSet<TareaDTO> Tareas);

public record CrearEtiquetaRequestDTO(string Name);
