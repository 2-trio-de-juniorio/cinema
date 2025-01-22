using DataAccessLayer.Interfaces;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Repositories;

internal sealed class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly AppDbContext _ctx;
    private readonly DbSet<TEntity> _entities;
    public Repository(AppDbContext ctx)
    {
        _ctx = ctx;
        _entities = ctx.Set<TEntity>();
    }


    public Task<TEntity?> GetByIdAsync(int id) => _entities.FindAsync(id).AsTask();
    public Task<List<TEntity>> GetAllAsync() => _entities.ToListAsync();
    public void Add(TEntity entity)
    {
        _entities.Add(entity);
    }
    public void Remove(TEntity entity)
    {
        if (_ctx.Entry(entity).State == EntityState.Detached) 
            _entities.Attach(entity);

        _entities.Remove(entity);
    }
    public async Task RemoveAsync(int id) 
    {
        TEntity? entity = await GetByIdAsync(id).ConfigureAwait(false);

        if (entity != null) 
            Remove(entity);
    }
    public void Update(TEntity entity)
    {
        _entities.Attach(entity);
        _ctx.Entry(entity).State = EntityState.Modified;
    }
    public Task SaveAsync() 
    {
        return _ctx.SaveChangesAsync();
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
                _ctx.Dispose();
            }

            _disposed = true;
        }
    }
}