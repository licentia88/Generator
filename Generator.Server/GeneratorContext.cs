//using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server;

public class GeneratorContext: DbContext
{

    public GeneratorContext(DbContextOptions<GeneratorContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }


    public DbSet<COMPONENTS_BASE> COMPONENTS_BASE { get; set; }

    public DbSet<PAGES_M> PAGES_M { get; set; }

    public DbSet<PAGES_D> PAGES_D { get; set; }

    public DbSet<VIEW_BASE> VIEW_BASE { get; set; }

    public DbSet<GRID_CRUD_VIEW> CRUD_VIEW { get; set; }

    public DbSet<HEADER_BUTTON_VIEW> HEADER_BUTTON_VIEW { get; set; }

    public DbSet<SIDE_BUTTON_VIEW> SIDE_BUTTON_VIEW { get; set; }

    //public DbSet<BUTTONS_BASE> BUTTONS_BASE { get; set; }

    //public DbSet<PAGES_M> PAGES_M { get; set; }

    //public DbSet<PAGES_D> PAGES_D { get; set; }

    //public DbSet<DISPLAY_FIELDS> DISPLAY_FIELDS { get; set; }

    //public DbSet<HEADER_BUTTONS> HEADER_BUTTONS { get; set; }

    //public DbSet<GRID_BUTTONS> GRID_BUTTONS { get; set; }

    public DbSet<PERMISSIONS> PERMISSIONS { get; set; }

    


}

