
namespace GestorTareas.Shared;

public record ArchivoDTO(
    Guid Id,
    byte[] File,
    string Extension,
    DateTime UploadedAt,
    TareaDTO Tarea
);