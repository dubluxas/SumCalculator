using SumCalculator.Models;

namespace SumCalculator.Repositories;

/// <summary>
/// Defines generic CRUD methods for entities of <typeparamref name="T"/>
/// </summary>
/// <typeparam name="T">The type of entity interface operates on.</typeparam>
public interface IBaseRepository <T> where T : class
{
    public IQueryable<T> GetAllQueryable();
    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<int> CreateAsync(T entity, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(T entity);
    public Task<int> DeleteAsync(T entity);

}