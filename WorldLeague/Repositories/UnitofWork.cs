using WorldLeague.DataAccess.Context;
using WorldLeague.Entities;

namespace WorldLeague.Repositories;

public class UnitofWork : IUnitofWork, IDisposable
{
    private readonly AdessoContext _adessoContext;

    public UnitofWork(AdessoContext adessoContext)
    {
        _adessoContext = adessoContext ?? throw new ArgumentException("context is null");
    }

    public IGenericRepository<T> GetRepository<T>() where T : BaseEntity, new()
    {
        return new GenericRepository<T>(_adessoContext);
    }

    public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true)
    {
        var transaction = _adessoContext.Database.BeginTransaction();
        try
        {
            var affectedRow = await _adessoContext.SaveChangesAsync(acceptAllChangesOnSuccess);
            transaction.Commit();
            return affectedRow;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    public void Dispose()
    {
        _adessoContext.Dispose();
    }
}