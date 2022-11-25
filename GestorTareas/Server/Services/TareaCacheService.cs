using GestorTareas.Dominio;
using GestorTareas.Server.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GestorTareas.Server.Services;


public class TareaCacheService : ITareaCacheService
{
    private const string Key = "tareas";
    private readonly IMemoryCache _memoryCache;

    public TareaCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public List<Tarea>? Get()
    {
        var found = _memoryCache.TryGetValue(Key, out var value);
        return found ? value as List<Tarea> : null;
    }

    public void Upsert(List<Tarea> value)
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


    public void Upsert(List<Tarea> value, TimeSpan expiration)
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

    public void Delete()
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