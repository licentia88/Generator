using System;
using Generator.Client;
using Generator.Client.Hubs.Base;

namespace Generator.UI.Startup;

public class HubInitializer
{
    public IServiceProvider Provider { get; }

    public GridMHub GridMHub { get;  }

    public ComponentsHub ComponentsHub { get;  }

    public PermissionHub PermissionHub { get;  }

    public HubInitializer(IServiceProvider provider)
	{
        Provider = provider;

        GridMHub = provider.GetRequiredService<GridMHub>();
        ComponentsHub = provider.GetRequiredService<ComponentsHub>();
        PermissionHub = provider.GetRequiredService<PermissionHub>();
       
    }

    public async Task ReadFromHubs()
    {
        await GridMHub.ConnectAsync();
        await ComponentsHub.ConnectAsync();
        await PermissionHub.ConnectAsync();

        await GridMHub.ReadAsync();
        await ComponentsHub.ReadAsync();
        await PermissionHub.ReadAsync();
    }
}

