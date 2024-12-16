using SumCalculator.Models;

namespace SumCalculator.Repositories;

public interface IBaseRepository <T> where T : class
{
    public IQueryable<T> GetAllQueryable();
    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<int> CreateAsync(T entity, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(T entity);
    public Task<int> DeleteAsync(T entity);

}