using Generator.Server.Database;
using Generator.Shared.Models.ComponentModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Seed;

public class SeedData
{
    public IServiceProvider Provider { get; }

    [Inject]
    public GeneratorContext GenDb { get; set; }

 
    [Inject]
    public List<USER_AUTHORIZATIONS> UserAuthorizationsList { get; set; }

    public SeedData(IServiceProvider provider)
	{
        Provider = provider;

        GenDb = provider.GetService<GeneratorContext>();

    }

    public void Seed()
    {
        var userAuthorizationsList = GenDb.USER_AUTHORIZATIONS.Include(x => x.AUTH_BASE).ToList();

        UserAuthorizationsList = userAuthorizationsList;
    }
}

