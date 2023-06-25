using Generator.Client.Hubs;

namespace Generator.UI.Startup;

public class HubInitializer
{
    public IServiceProvider Provider { get; }

    private GridMHub GridMHub { get;  }

    private PermissionHub PermissionHub { get;  }

    public HubInitializer(IServiceProvider provider)
	{
        Provider = provider;

        GridMHub = provider.GetRequiredService<GridMHub>();
        PermissionHub = provider.GetRequiredService<PermissionHub>();
       
    }

    public async Task ReadAsync()
    {
        await GridMHub.ReadAsync();
        await PermissionHub.ReadAsync();
    }

    public async Task ConnectAsync()
    {
        await GridMHub.ConnectAsync();
        await PermissionHub.ConnectAsync();
    }
}

