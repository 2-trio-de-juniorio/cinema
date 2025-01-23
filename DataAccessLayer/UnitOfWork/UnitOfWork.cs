using DataAccessLayer.Data;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.UnitOfWork;
internal sealed class UnitOfWork : IUnitOfWork
{
    private Dictionary<string, object> _repositories;
    private readonly AppDbContext _dbContext;
    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<string, object>();
    }
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (!_repositories.ContainsKey(typeof(TEntity).Name))
        {
            _repositories[typeof(TEntity).Name] = new Repository<TEntity>(_dbContext);
        }
        return (IRepository<TEntity>)_repositories[typeof(TEntity).Name];
    }

    public Task<int> SaveAsync()
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