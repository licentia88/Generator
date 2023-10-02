using Microsoft.JSInterop;

namespace Generator.Components;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class GeneratorJs : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public GeneratorJs(IJSRuntime jsRuntime)
    {
        moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Generator.Components/GeneratorJs.js").AsTask());
    }

    public async ValueTask<string> Prompt(string message)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("showPrompt", message);
    }

    public async ValueTask<string> ChangeRowStyle(string pointerEventsValue)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("changeRowStyle", pointerEventsValue);
    }


    public async ValueTask DownloadExcelFile(string fileName, MemoryStream xlsStream)
    {
        var module = await moduleTask.Value;

        await module.InvokeVoidAsync("BlazorDownloadFile", $"{fileName}.xlsx", xlsStream.ToArray());

    }
    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    public async ValueTask BlankClick()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("BlankClick");
    }
}

