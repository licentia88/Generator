using Microsoft.AspNetCore.Components;

namespace Generator.Components.Components;

public partial class GridLabel:ComponentBase,IDisposable
{
    [Parameter]
    public object Value { get; set; }


    private void ReleaseUnmanagedResources()
    {
        // TODO release unmanaged resources here
        
        Value = null;
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~GridLabel()
    {
        ReleaseUnmanagedResources();
    }
}

