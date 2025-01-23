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
    public Task AddAsync(TEntity entity) 
    {
        return _entities.AddAsync(entity).AsTask();
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
}
