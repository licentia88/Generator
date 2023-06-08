using Generator.Shared.Models.ComponentModels;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Database;

public class MemoryContext : DbContext
{
    public MemoryContext(DbContextOptions<MemoryContext> options):base(options)
    {

    }

    public DbSet<PERMISSIONS> PERMISSIONS { get; set; }

}

