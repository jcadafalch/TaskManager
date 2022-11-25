using GestorTareas.Dominio;

namespace GestorTareas.Server.Interfaces;

public interface ITareaCacheService
{
    /// <summary>
    /// Obtiene el listado de tareas de cache
    /// </summary>
    /// <returns>Listado de tareas en cache</returns>
    List<Tarea>? Get();

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="value">Valor que se va a almacenar</param>
    void Upsert(List<Tarea> value);

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="value">Valor que se va a almacenar</param>
    void Upsert(List<Tarea> value, TimeSpan expiration);

    /// <summary>
    /// Elimina de caché el contenido
    /// </summary>
    void Clear();
}
