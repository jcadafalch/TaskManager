
namespace GestorTareas.Shared;

public record EtiquetaDTO(Guid Id, string Title, DateTime CreatedAt, ICollection<TareaDTO>? Tareas);

public record CrearEtiquetaRequestDTO(string Name);
