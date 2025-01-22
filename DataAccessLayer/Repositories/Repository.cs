using DataAccessLayer.Interfaces;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Repositories;

internal sealed class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _entities;
    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = dbContext.Set<TEntity>();
    }


    public Task<TEntity?> GetByIdAsync(int id) 
    {
        return _entities.FindAsync(id).AsTask();
    }
    public Task<List<TEntity>> GetAllAsync() 
    {
        return _entities.ToListAsync();
    }
    public void Add(TEntity entity)
    {
        _entities.Add(entity);
    }
    public void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }
    public async Task RemoveByIdAsync(int id) 
    {
        TEntity? entity = await GetByIdAsync(id).ConfigureAwait(false);

        if (entity != null) 
        {
            Remove(entity);
        }
    }
    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
    public Task SaveAsync() 
    {
        return _dbContext.SaveChangesAsync();
    }

    public void Dispose() 
    {
        Dispose(disposing: true);
    }
    private bool _disposed = false;
    private void Dispose(bool disposing) 
    {
        if (!_disposed) 
        {
            if (disposing) 
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }
    }
}