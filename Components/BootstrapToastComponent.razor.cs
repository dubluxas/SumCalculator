using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SumCalculator.Components;

public class BootstrapToastComponentBase: ComponentBase
{
    [Parameter] 
    public string Title { get; set; } = default!;
    [Parameter] 
    public string Message { get; set; } = default!;
    [Parameter] 
    public string Type { get; set; } = default!;

    [Inject] 
    public required IJSRuntime JSRuntime { get; set; } 

    public async Task ShowAsync()
    {
        await JSRuntime.InvokeVoidAsync("bootstrapToast.show", "successToast");
    }
}