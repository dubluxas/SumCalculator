using Microsoft.EntityFrameworkCore;
using SumCalculator.Data;

namespace SumCalculator.Repositories;

/// <summary>
/// Provides CRUD (Create, Read, Update, Delete) operations for entities of <typeparamref name="T"/>
/// This class uses <see cref="ApplicatonDbContext"/> database context.
/// </summary>
/// <typeparam name="T">The type of Entity CRUD operation implemented</typeparam>
/// <param name="dbContextFactory">Uses <see cref="IDbContextFactory{TContext}"/> to create dbcontext.</param>
public class BaseRepository<T>(IDbContextFactory<ApplicatonDbContext> dbContextFactory) : IBaseRepository<T> where T : class
{
    private readonly IDbContextFactory<ApplicatonDbContext> _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory), "Database context is null.");

    public async Task<int> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            dbContext.Set<T>().Add(entity);
            var results = await dbContext.SaveChangesAsync(cancellationToken);
            return results;
        }catch
        {
            throw;
        }
       
    }

    public Task<int> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAllQueryable()
    {
        try
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            return dbContext.Set<T>().AsQueryable();
        }catch
        {
            throw;
        }
        
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Set<T>().FindAsync([id], cancellationToken);
        }catch
        {
            throw;
        }
      
    }

    public Task<bool> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}