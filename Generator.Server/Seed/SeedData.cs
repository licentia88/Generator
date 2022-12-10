using System.Collections.ObjectModel;
using Generator.Shared.Models.ComponentModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.Server.Seed;

public class SeedData
{
    [Inject]
    public Lazy<ObservableCollection<COMPONENT>> Components { get; set; }

    public GeneratorContext Db { get; set; }

    public SeedData(IServiceProvider provider)
    {
        Db = provider.GetService<GeneratorContext>();
        Components = provider.GetService<Lazy<ObservableCollection<COMPONENT>>>();
    }

    public async ValueTask FillComponentsAsync()
    {
        //var newGrid = new GRIDS_M
        //{
        //    COMP_TITLE = "TEST",

        //};

        //var newFooter = new FOOTER_BUTTON
        //{
        //    //COMP_TITLE = "TEST2"
        //};

        //var newGridD = new GRIDS_D
        //{
        //    COMP_TITLE = "TEST3"
        //};

        //newGrid.FOOTER_BUTTON.Add(newFooter);
        //newGrid.GRIDS_D.Add(newGridD);

        //Db.GRIDS_M.Add(newGrid);
        //await Db.SaveChangesAsync();


        var comps = await Db.COMPONENT
        .Include(x => x.DATABASES)
        .ToListAsync();
        var COMPTODELETE = comps.FirstOrDefault(X => X.COMP_TYPE == nameof(GRIDS_M));

        //Db.Remove(COMPTODELETE);
        //await Db.SaveChangesAsync();

        await Task.Delay(0);

    }
}

