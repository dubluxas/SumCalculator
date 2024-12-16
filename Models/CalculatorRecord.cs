namespace SumCalculator.Models;

public class CalculatorRecord
{
    public int Id { get; set; }
    public decimal? Value1 { get; set; }
    public decimal? Value2 { get; set;}
    public decimal Sum { get; private set; }
    public DateTime DateTime{ get; private set; }
}