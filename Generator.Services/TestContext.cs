using Generator.Examples.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Generator.Services
{
   
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }


        public DbSet<TEST_TABLE> TEST_TABLE { get; set; }

        public DbSet<COMPUTED_TABLE> COMPUTED_TABLE { get; set; }

        public DbSet<STRING_TABLE> STRING_TABLE { get; set; }

        public DbSet<PARENT_CLASS> PARENT_CLASS { get; set; }

        public DbSet<CHILD_CLASS> CHILD_CLASS { get; set; }


        public DbSet<USER> USER { get; set; }

        public DbSet<ORDERS_M> ORDERS_M { get; set; }

        public DbSet<ORDERS_D> ORDERS_D { get; set; }
    }
}

