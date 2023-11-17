namespace WorldLeague.Repositories;

public interface IUnitofWork
{
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true);
}