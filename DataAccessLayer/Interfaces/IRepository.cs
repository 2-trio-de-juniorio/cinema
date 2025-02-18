using System.Linq.Expressions;

namespace DataAccessLayer.Interfaces
{
    // Currently implemented as an interface. In future, class-based repositories might be used
    public interface IRepository<TEntity>
        where TEntity : class 
    {
        Task<TEntity?> GetByIdAsync<TKey>(TKey id, params string[] includes);
        Task<List<TEntity>> GetAllAsync(params string[] includes);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<bool> RemoveByIdAsync(int id);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}