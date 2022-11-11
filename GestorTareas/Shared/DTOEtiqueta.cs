
namespace GestorTareas.Shared;

public record EtiquetaDTO(
    Guid Id,
    string Name
);

public record CreateEtiquetaRequestDTO(string Name);

public record UpdateEtiquetaRequestDTO(Guid Id, string NewName);

public record DeleteEtiquetaRequestDTO(Guid Id);
