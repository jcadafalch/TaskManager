using GestorTareas.Dominio;

namespace GestorTareas.Server.Interfaces;

public interface IEtiquetaCacheService
{
    /// <summary>
    /// Obtiene el listado de etiquetas de cache
    /// </summary>
    /// <returns>Listado de etiquetas en cache</returns>
    IEnumerable<Etiqueta>? Get();

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="value">Valor que se va a almacenar</param>
    void Upsert(IEnumerable<Etiqueta> value);

    /// <summary>
    /// Inserta o actualiza valores en cache
    /// </summary>
    /// <param name="value">Valor que se va a almacenar</param>
    /// <param name="expiration">Tiempo que transcurre antes de que los valores se eliminen</param>
    void Upsert(IEnumerable<Etiqueta> value, TimeSpan expiration);

    /// <summary>
    /// Elimina de caché el contenido
    /// </summary>
    void Clear();
}