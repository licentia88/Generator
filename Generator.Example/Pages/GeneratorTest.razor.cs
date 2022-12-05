using System;
using Generator.Shared.Models;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.Pages
{
	public partial class GeneratorTest
	{
        [Inject]
        public IDatabaseService IDatabaseService { get; set; }

        [Inject]
        public IHeaderButtonService IHeaderButtonService { get; set; }

        [Inject]
        public IFooterButtonService IFooterButtonService { get; set; }

        [Inject]
        public IGridsMService IGridsMService { get; set; }

        [Inject]
        public IGridsDService IGridsDService { get; set; }

        public async Task ReadDb()
        {
            var newDatabase = new DATABASE()
            {
                ConnectionString = "Server=Localhost;Database=GeneratorContext;User Id=sa;Password=LucidNala88!;TrustServerCertificate=true"
            };

            var result = await IDatabaseService.AddAsync(new(newDatabase));

            Console.WriteLine();
        }

        public async Task ReadHeader()
        {
            var result = await IHeaderButtonService.QueryAsync(new());
        }

        public async Task ReadFooter()
        {
            var result = await IFooterButtonService.QueryAsync(new());
        }

        public async Task ReadGridM()
        {
            var result = await IGridsMService.QueryAsync(new());
        }

        public async Task ReadGridD()
        {
            var result = await IGridsDService.QueryAsync(new());
        }
    }
}

