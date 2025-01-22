namespace DataAccessLayer.Interfaces;

// Currently implemented as an interface. In future, class-based repositories might be used
public interface IRepository<TEntity> : IDisposable
    where TEntity : class 
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<List<TEntity>> GetAllAsync();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task RemoveByIdAsync(int id);
    Task SaveAsync();
}