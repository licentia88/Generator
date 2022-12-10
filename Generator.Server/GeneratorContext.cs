using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server;

public class GeneratorContext: DbContext
{
    public GeneratorContext(DbContextOptions<GeneratorContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<COMPONENT> COMPONENT { get; set; }

    public DbSet<DATABASES> DATABASES { get; set; }

    public DbSet<GRIDS_D> GRIDS_D { get; set; }

    public DbSet<GRIDS_M> GRIDS_M { get; set; }

    public DbSet<HEADER_BUTTON> HEADER_BUTTON { get; set; }

    public DbSet<FOOTER_BUTTON> FOOTER_BUTTON { get; set; }


}

 