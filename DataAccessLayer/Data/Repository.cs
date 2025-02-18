using DataAccessLayer.Interfaces;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<TEntity> _entities;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<TEntity>();
        }

        public Task<TEntity?> GetByIdAsync<TKey>(TKey id, params string[] includes)
        {
            if (typeof(TEntity).GetProperty("Id") == null)
            {
                throw new InvalidOperationException(
                    $"Entity type '{typeof(TEntity).Name}' does not have an Id property.");
            }

            IQueryable<TEntity> query = _entities;

            foreach (string include in includes)
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

            foreach (string include in includes)
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

        public async Task<bool> RemoveByIdAsync(int id)
        {
            TEntity? entity = await GetByIdAsync(id).ConfigureAwait(false);

            if (entity == null) return false;

            Remove(entity);
            return true;
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.CountAsync(predicate);
        }
    }
}
