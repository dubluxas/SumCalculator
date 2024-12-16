using SumCalculator.Models;
using SumCalculator.Utilities;

namespace SumCalculator.Repositories;

public interface ICalculatorRepository
{
        public Task<Repositoryresponse<IList<CalculatorRecord>>> GetAllRecordsAsync(CancellationToken cancellationToken = default);
        public Task<Repositoryresponse<CalculatorRecord>> GetRecordByIdAsync(int id, CancellationToken cancellationToken = default);
        public Task<Repositoryresponse<CalculatorRecord>> CreateRecordAsync(CalculatorRecord entity, CancellationToken cancellationToken = default);

}