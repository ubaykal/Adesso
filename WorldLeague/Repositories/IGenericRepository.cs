using WorldLeague.Entities;

namespace WorldLeague.Repositories;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    /// <summary> 
    /// Seçilen repositorye belirlenen datayı async kayıt oluşturur. 
    /// </summary>
    Task AddAsync(T entity);

    /// <summary> 
    /// Seçilen repositorye belirlenen datayı toplu olarak async kayıt oluşturur. 
    /// </summary>
    Task AddRangeAsync(List<T> entityList);
}