using System;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using Microsoft.EntityFrameworkCore;

namespace Generator.Services
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<TEST_TABLE> TEST_TABLE { get; set; }

        public DbSet<COMPUTED_TABLE> COMPUTED_TABLE { get; set; }

        public DbSet<STRING_TABLE> STRING_TABLE { get; set; }


    }
}

