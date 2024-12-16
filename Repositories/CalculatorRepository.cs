using Microsoft.EntityFrameworkCore;
using SumCalculator.Models;
using SumCalculator.Utilities;

namespace SumCalculator.Repositories;

/// <summary>
/// Provides CRUD (Create, Read, Update, Delete) operations for entities of type <see cref="CalculatorRecord"/>.
/// Uses <see cref="IBaseRepository{T}"/> to complete implementation.
/// </summary>
public class CalculatorRepository : ICalculatorRepository
{
    private readonly IBaseRepository<CalculatorRecord>? _baseRepository;

    public CalculatorRepository(IBaseRepository<CalculatorRecord>? baseRepository)
    {

        if (baseRepository == null)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(baseRepository));
        }

        _baseRepository = baseRepository;
    }
    public async Task<Repositoryresponse<IList<CalculatorRecord>>> GetAllRecordsAsync(CancellationToken cancellationToken = default)
    {

        if (_baseRepository == null)
            return new Repositoryresponse<IList<CalculatorRecord>>().CreateFailure("Database connection is not initialized.");

        try
        {
            var result = await _baseRepository.GetAllQueryable().AsNoTracking().ToListAsync(cancellationToken);

            if (result.Count == 0)
            {
                return new Repositoryresponse<IList<CalculatorRecord>>()
                    .CreateFailure("No records were found in the database.");
            }

            return new Repositoryresponse<IList<CalculatorRecord>>().CreateSuccess(result);
        }
        catch (Exception ex)
        {
            return new Repositoryresponse<IList<CalculatorRecord>>()
                .CreateFailure($"An unexpected error occurred while fetching all records: {ex.Message}");
        }
    }

    public async Task<Repositoryresponse<CalculatorRecord>> GetRecordByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (_baseRepository == null)
            return new Repositoryresponse<CalculatorRecord>().CreateFailure("Database connection is not initialized.");

        if (id <= 0)
        {
            return new Repositoryresponse<CalculatorRecord>()
                .CreateFailure("Invalid ID provided. ID must be greater than zero.");
        }

        try
        {
            var result = await _baseRepository!.GetByIdAsync(id, cancellationToken);

            if (result == null)
            {
                return new Repositoryresponse<CalculatorRecord>()
                    .CreateFailure($"No record found with the ID: {id}.");
            }

            return new Repositoryresponse<CalculatorRecord>().CreateSuccess(result);
        }
        catch (Exception ex)
        {
            return new Repositoryresponse<CalculatorRecord>()
                .CreateFailure($"An error occurred while retrieving the record with ID {id}: {ex.Message}");
        }
    }

    public async Task<Repositoryresponse<CalculatorRecord>> CreateRecordAsync(CalculatorRecord entity, CancellationToken cancellationToken = default)
    {

        if (_baseRepository == null)
            return new Repositoryresponse<CalculatorRecord>().CreateFailure("Database connection is not initialized.");

        if (entity is null)
        {
            return new Repositoryresponse<CalculatorRecord>()
                .CreateFailure("The provided entity is null. Please ensure all required properties are set.");
        }

        if (entity.Value1 is null || entity.Value2 is null)
        {
            return new Repositoryresponse<CalculatorRecord>()
                .CreateFailure("Value1 and Value2 are required and cannot be null.");
        }

        try
        {
            var result = await _baseRepository!.CreateAsync(entity, cancellationToken);

            if (result == 0)
            {
                return new Repositoryresponse<CalculatorRecord>()
                    .CreateFailure("Failed to create the record due to an unexpected internal error.");
            }

            return new Repositoryresponse<CalculatorRecord>()
                .CreateSuccess(entity);
        }
        catch (Exception ex)
        {
            return new Repositoryresponse<CalculatorRecord>()
                .CreateFailure($"An error occurred while creating the record: {ex.Message}");
        }
    }
}