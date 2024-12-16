using SumCalculator.Models;
using SumCalculator.Utilities;

namespace SumCalculator.Repositories;
/// <summary>
/// Defines CRUD methods for entity of type <see cref="CalculatorRecord"/>
/// </summary>
public interface ICalculatorRepository
{
        public Task<Repositoryresponse<IList<CalculatorRecord>>> GetAllRecordsAsync(CancellationToken cancellationToken = default);
        public Task<Repositoryresponse<CalculatorRecord>> GetRecordByIdAsync(int id, CancellationToken cancellationToken = default);
        public Task<Repositoryresponse<CalculatorRecord>> CreateRecordAsync(CalculatorRecord entity, CancellationToken cancellationToken = default);

}