using GestorTareas.Dominio;
using GestorTareas.Server.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GestorTareas.Server.Services;

public class EtiquetaCacheService : IEtiquetaCacheService
{
    private readonly IMemoryCache _memoryCache;

    public EtiquetaCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// Obtiene el listado de etiquetas de cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a obtener</param>
    /// <returns>Listado de etiquetas en cache</returns>
    public List<Etiqueta>? Get(string key)
    {
        var found = _memoryCache.TryGetValue(key, out var value);
        return found ? value as List<Etiqueta> : null;
    }

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a introducir o actualizar</param>
    /// <param name="value">Valor que se va a almacenar</param>
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

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="key">Nombre clave del valor a introducir  o actualizar</param>
    /// <param name="value">Valor que se va a almacenar</param>
    /// <param name="expiration">Tiempo que transcurre antes de que los valores se eliminen</param>
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

    /// <summary>
    /// Elimina de caché el contenido que tenga como nombre clave el valor pasado como parametro
    /// </summary>
    /// <param name="key">Nombre clave del valor a eliminar</param>
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


