
namespace GestorTareas.Shared;

public record ArchivoDTO(
    Guid Id, 
    byte[] File, 
    string Extension, 
    DateTime UploadedAt, 
    TareaDTO Tarea);

public record AddArchivoDTO(byte[] File, string Extension, Guid IdTarea);

public record RemoveArchivoDTO(Guid IdArchivo, Guid IdTarea);
