using Microsoft.AspNetCore.Components;
using SumCalculator.Models;

namespace SumCalculator.Components;

public class SumRecordFormBase : ComponentBase 
{
    protected CalculatorRecord? CalculatorRecord { get; set; }

    [Parameter]
    public EventCallback<CalculatorRecord> OnRecordSubmitted { get; set; }

    public async Task HandleValidSubmitAsync()
    {
        await OnRecordSubmitted.InvokeAsync(CalculatorRecord);
        CalculatorRecord = new();
        await InvokeAsync(StateHasChanged);
    }

    protected override void OnInitialized()
    {
        CalculatorRecord = new CalculatorRecord();
    }

}