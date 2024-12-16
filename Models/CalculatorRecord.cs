namespace SumCalculator.Models;

/// <summary>
/// Represents a database entity for a summarization operation.
/// Stores input values (<see cref="Value1"/> and <see cref="Value2"/>). The <see cref="Sum"/> and <see cref="DateTime"/> 
/// is calculated in database.
/// </summary>
public class CalculatorRecord
{
    public int Id { get; set; }
    public decimal? Value1 { get; set; }
    public decimal? Value2 { get; set;}
    public decimal Sum { get; private set; }
    public DateTime DateTime{ get; private set; }
}