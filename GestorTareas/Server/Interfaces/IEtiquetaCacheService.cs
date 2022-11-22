using GestorTareas.Dominio;

namespace GestorTareas.Server.Interfaces;

public interface IEtiquetaCacheService
{
    /// <summary>
    /// Obtiene el listado de etiquetas de cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a obtener</param>
    /// <returns>Listado de etiquetas en cache</returns>
    List<Etiqueta>? Get(string key);

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a introducir o actualizar</param>
    /// <param name="value">Valor que se va a almacenar</param>
    void Upsert(string key, List<Etiqueta> value);

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a introducir  o actualizar</param>
    /// <param name="value">Valor que se va a almacenar</param>
    /// <param name="expiration">Tiempo que transcurre antes de que los valores se eliminen</param>
    void Upsert(string key, List<Etiqueta> value, TimeSpan expiration);

    /// <summary>
    /// Elimina de caché el contenido que tenga como nombre clave el valor pasado como parametro
    /// </summary>
    /// <param name="key">Nombre clave del valor a eliminar</param>
    void Delete(string key);
}