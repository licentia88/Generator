using System;
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
    public List<PERMISSIONS> Permissions { get; set; }

    public SeedData(IServiceProvider provider)
	{
        Provider = provider;

        GenDb = provider.GetService<GeneratorContext>();

     }

    public async void Seed()
    {
        var permissionList = await GenDb.PERMISSIONS.AsNoTracking().ToListAsync();

        Permissions = permissionList;
        Console.WriteLine();
    }
}

