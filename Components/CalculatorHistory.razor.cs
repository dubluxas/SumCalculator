using Microsoft.AspNetCore.Components;
using SumCalculator.Models;

namespace SumCalculator.Components;

public class CalculatorHistoryBase: ComponentBase
{
    protected IList<CalculatorRecord> CalculatorRecords { get; set; } = [];
    
    public async Task UpdateRecordsAsync(IList<CalculatorRecord> calculatorRecords)
    {
        CalculatorRecords = calculatorRecords;
        await InvokeAsync(StateHasChanged);
    }


}
