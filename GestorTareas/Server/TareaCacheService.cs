using GestorTareas.Dominio;
using Microsoft.Extensions.Caching.Memory;

namespace GestorTareas.Server;


public class TareaCacheService : ITareaCacheService
{
    private readonly IMemoryCache _memoryCache;

    public TareaCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public List<Tarea>? Get(String key)
    {
        var found = _memoryCache.TryGetValue(key, out var value);
        return found ? value as List<Tarea> : null;
    }

    public void Upsert(string key, List<Tarea> value)
    {
        try
        {
            var s = _memoryCache.Set(key, value);
        }catch(Exception ex)
        {
            // deal with solutiom, or log error
        }
    }


    public void Upsert(string key, List<Tarea> value, TimeSpan expiration)
    {
        try
        {
            var s = _memoryCache.Set(key, value, expiration);
        }catch(Exception ex)
        {
            // deal  with the exception, or log error
        }
    }

    public void Delete(string key)
    {
        try
        {
            _memoryCache.Remove(key);
        }catch(Exception ex)
        {
            // Deal with the exception, or log error
            throw;
        }
    }
}

public interface ITareaCacheService
{
    List<Tarea>? Get(String key);

    // Insert or update
    void Upsert(string key, List<Tarea> value);

    // Insert or update
    void Upsert(string key, List<Tarea> value, TimeSpan expiration);

    void Delete(string key);
}
