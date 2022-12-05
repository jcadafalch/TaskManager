
namespace GestorTareas.Shared;

public record ArchivoDTO(
    Guid Id,
    string Name,
    byte[] File,
    string Extension,
    DateTime UploadedAt,
    TareaDTO Tarea
);