using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Database;

public class GeneratorContext : DbContext
{

    public GeneratorContext(DbContextOptions<GeneratorContext> options) : base(options)
    {
    }

    
    

    public DbSet<GRID_FIELDS> GRID_FIELDS { get; set; }

    public DbSet<COMPONENTS_BASE> COMPONENTS_BASE { get; set; }

    public DbSet<GRID_M> GRID_M { get; set; }

    public DbSet<GRID_D> GRID_D { get; set; }

    public DbSet<USERS> USERS { get; set; }

    public DbSet<USER_AUTHORIZATIONS> USER_AUTHORIZATIONS { get; set; }

    public DbSet<AUTH_BASE> AUTH_BASE { get; set; }

    public DbSet<ROLES> ROLES { get; set; }

    public DbSet<ROLES_DETAILS> ROLES_DETAILS { get; set; }

    public DbSet<PERMISSIONS> PERMISSIONS { get; set; }

}

