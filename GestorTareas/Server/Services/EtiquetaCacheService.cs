﻿using GestorTareas.Dominio;
using GestorTareas.Server.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GestorTareas.Server.Services;

internal sealed class EtiquetaCacheService : IEtiquetaCacheService
{
    private const string Key = "etiquetas";
    private readonly IMemoryCache _memoryCache;

    public EtiquetaCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// Obtiene el listado de etiquetas de cache
    /// </summary>
    /// <returns>Listado de etiquetas en cache</returns>
    public IEnumerable<Etiqueta>? Get()
    {
        var found = _memoryCache.TryGetValue(Key, out var value);
        return found ? value as IEnumerable<Etiqueta> : null;
    }

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="value">Valor que se va a almacenar</param>
    public void Upsert(IEnumerable<Etiqueta> value)
    {
        try
        {
            var s = _memoryCache.Set(Key, value);
        }
        catch (Exception ex)
        {
            // deal with solutiom, or log error
        }
    }

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="value">Valor que se va a almacenar</param>
    /// <param name="expiration">Tiempo que transcurre antes de que los valores se eliminen</param>
    public void Upsert(IEnumerable<Etiqueta> value, TimeSpan expiration)
    {
        try
        {
            var s = _memoryCache.Set(Key, value, expiration);
        }
        catch (Exception ex)
        {
            // deal  with the exception, or log error
        }
    }

    /// <summary>
    /// Elimina de caché el contenido
    /// </summary>
    public void Clear()
    {
        try
        {
            _memoryCache.Remove(Key);
        }
        catch (Exception ex)
        {
            // Deal with the exception, or log error
            throw;
        }
    }
}


