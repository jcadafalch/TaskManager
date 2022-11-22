using GestorTareas.Dominio;

namespace GestorTareas.Server.Interfaces;

public interface ITareaCacheService
{
    /// <summary>
    /// Obtiene el listado de tareas de cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a obtener</param>
    /// <returns>Listado de tareas en cache</returns>
    List<Tarea>? Get(String key);

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a introducir o actualizar</param>
    /// <param name="value">Valor que se va a almacenar</param>
    void Upsert(string key, List<Tarea> value);

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a introducir  o actualizar</param>
    /// <param name="value">Valor que se va a almacenar</param>
    void Upsert(string key, List<Tarea> value, TimeSpan expiration);

    /// <summary>
    /// Elimina de caché el contenido que tenga como nombre clave el valor pasado como parametro
    /// </summary>
    /// <param name="key">Nombre clave del valor a eliminar</param>
    void Delete(string key);
}
