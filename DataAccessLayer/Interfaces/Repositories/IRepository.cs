namespace DataAccessLayer.Interfaces;

public interface IRepository<TEntity> : IDisposable
    where TEntity : class 
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<List<TEntity>> GetAllAsync();

    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task RemoveAsync(int id);

    Task SaveAsync();
}