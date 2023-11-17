using Microsoft.EntityFrameworkCore;
using WorldLeague.DataAccess.Context;
using WorldLeague.Entities;

namespace WorldLeague.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    private readonly AdessoContext _adessoContext;

    private readonly DbSet<T> _dbSet;

    public GenericRepository(AdessoContext adessoContext)
    {
        _adessoContext = adessoContext;
        _dbSet = _adessoContext.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);

        entity.Id = Guid.NewGuid();
    }

    public async Task AddRangeAsync(List<T> entityList)
    {
        await _dbSet.AddRangeAsync(entityList);
        foreach (var baseEntities in entityList)
        {
            baseEntities.Id = Guid.NewGuid();
        }
    }
}