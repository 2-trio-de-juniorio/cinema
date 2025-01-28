using DataAccessLayer.Interfaces;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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


    public Task<TEntity?> GetByIdAsync(int id, params string[] includes) 
    {
        if (typeof(TEntity).GetProperty("Id") == null) 
        {
            throw new InvalidOperationException($"Entity type '{typeof(TEntity).Name}' does not have an Id property.");
        }

        IQueryable<TEntity> query = _entities;

        foreach(string include in includes) 
        {
            query = query.Include(include);
        }

        ParameterExpression parameter = Expression.Parameter(typeof(TEntity));

        return query.FirstOrDefaultAsync(
            (Expression<Func<TEntity, bool>>)
            Expression.Lambda(
                Expression.Equal(
                    Expression.MakeMemberAccess(parameter, typeof(TEntity).GetProperty("Id")!), 
                    Expression.Constant(id)
                ), 
                parameter
            )
        );
    }

    public Task<List<TEntity>> GetAllAsync(params string[] includes) 
    {
        IQueryable<TEntity> query = _entities;

        foreach(string include in includes) 
        {
            query = query.Include(include);
        }
        return query.ToListAsync();
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
