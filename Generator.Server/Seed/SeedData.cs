using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.Server.Seed;

public class SeedData
{
    
    public GeneratorContext Db { get; set; }

    public SeedData(IServiceProvider provider)
    {
        Db = provider.GetService<GeneratorContext>();
    }

    public Task FillComponentsAsync()
    {
        return Task.CompletedTask;

    }
}

