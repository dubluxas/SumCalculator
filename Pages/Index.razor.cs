using Microsoft.AspNetCore.Components;
using SumCalculator.Components;
using SumCalculator.Models;
using SumCalculator.Repositories;

namespace SumCalculator.Pages;

public class IndexBase : ComponentBase
{
    // Creates a cancellation token source.
    private CancellationTokenSource? Cts { get; set; }

    protected BootstrapToastComponent? ToastComponent { get; set; }
    protected CalculatorHistory? CalculatorHistory { get; set; }
    protected string ServerErrormessage { get; set; } = default!;
    protected bool HasError { get; set; }
    protected bool HasUnHandled { get; set; }

    [Inject]
    protected ICalculatorRepository? CalculatorRepository { get; set; }

    protected async Task RecordSubmitted(CalculatorRecord entry)
    {
        if (ToastComponent == null)
        {
            ServerErrormessage = "An unexpected internal error occurred.";
            await ShowMessage(ServerErrormessage);
            return;
        }

        if (entry == null)
        {
            ServerErrormessage = "The provided record is invalid. Please check the input.";
            await ShowMessage(ServerErrormessage);
            return;
        }

        if (CalculatorRepository == null)
        {
            ServerErrormessage = "The database is not available. Please try again later.";
            await ShowMessage(ServerErrormessage);
            return;
        }

        try
        {
            // Tries to create a record in the database.
            var result = await CalculatorRepository.CreateRecordAsync(entry, Cts!.Token);

            // Displays an error if the creation fails.
            if (!result.IsSuccessful)
            {
                ServerErrormessage = string.Join('\n', [.. result.Errors]);
                await ShowMessage(ServerErrormessage);
                return;
            }

            // Updates the history component on successful creation.
            await UpdateHistoryComponent("create");
        }
        catch (Exception ex)
        {
            ServerErrormessage = $"An error occurred: {ex.Message}";
            await ShowMessage(ServerErrormessage);
        }
    }

    private async Task ShowMessage(string message, bool hasError = true)
    {
        if (ToastComponent == null) return;

        HasError = hasError;
        ServerErrormessage = message;
        await ToastComponent.ShowAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override void OnInitialized()
    {
        Cts = new();
    }

    private bool _isFirstRender = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && _isFirstRender)
        {
            _isFirstRender = false;

            if (ToastComponent != null)
            {
                await UpdateHistoryComponent("update");
            }
        }
    }

    private async Task UpdateHistoryComponent(string action)
    {
        if (ToastComponent == null || CalculatorHistory == null)
        {
            ServerErrormessage = "Required components are unavailable.";
            await ShowMessage(ServerErrormessage);
            return;
        }

        // Fetches all records from the database.
        var response = await CalculatorRepository!.GetAllRecordsAsync(Cts!.Token);

        if (!response.IsSuccessful)
        {
            ServerErrormessage = string.Join('\n', [.. response.Errors]);
            await ShowMessage(ServerErrormessage);
            return;
        }

        // Updates the history component.
        var records = response.Data;
        await CalculatorHistory.UpdateRecordsAsync(records);

        // Sets success messages based on the action.
        var successMessage = action switch
        {
            "update" => "Successfully retrieved records from the database.",
            "create" => "Record created successfully.",
            _ => "Operation completed successfully."
        };

        await ShowMessage(successMessage, false);
    }
}