using GestorTareas.Dominio;
using Microsoft.Extensions.Caching.Memory;

namespace GestorTareas.Server;

public class EtiquetaCacheService : IEtiquetaCache
{
    private readonly IMemoryCache _memoryCache;

    public EtiquetaCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public List<Etiqueta>? Get(String key)
    {
        var found = _memoryCache.TryGetValue(key, out var value);
        return found ? value as List<Etiqueta> : null;
    }

    public void Upsert(string key, List<Etiqueta> value)
    {
        try
        {
            var s = _memoryCache.Set(key, value);
        }
        catch (Exception ex)
        {
            // deal with solutiom, or log error
        }
    }


    public void Upsert(string key, List<Etiqueta> value, TimeSpan expiration)
    {
        try
        {
            var s = _memoryCache.Set(key, value, expiration);
        }
        catch (Exception ex)
        {
            // deal  with the exception, or log error
        }
    }

    public void Delete(string key)
    {
        try
        {
            _memoryCache.Remove(key);
        }
        catch (Exception ex)
        {
            // Deal with the exception, or log error
            throw;
        }
    }
}

public interface IEtiquetaCache
{
    /// <summary>
    /// Obtiene el listado de etiquetas de cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a obtener</param>
    /// <returns></returns>
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
