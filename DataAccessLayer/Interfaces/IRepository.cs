namespace DataAccessLayer.Interfaces;

// Currently implemented as an interface. In future, class-based repositories might be used
public interface IRepository<TEntity>
    where TEntity : class 
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<List<TEntity>> GetAllAsync();
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task RemoveByIdAsync(int id);
}