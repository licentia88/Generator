using System;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using BCrypt.Net;
using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages
{
	public partial class Databases
	{
		[Inject]
		public List<DATABASES> DATABASE { get; set; }

		[Inject]
		public IDatabaseService IDatabaseService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //$2a$11$VP/ocvMI0dpk9yhw7yex9O
            //$2a$11$VP/ocvMI0dpk9yhw7yex9OdHiKxSapdhwvSxJha93MDm4FrexT5NW
            var saltKey = BCrypt.Net.BCrypt.GenerateSalt();

            var test =   BCrypt.Net.BCrypt.HashPassword("TEST", "$2a$11$VP/ocvMI0dpk9yhw7yex9O");

            var enhancedHashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword("TEST", hashType: HashType.SHA384);
            var validatePassword = BCrypt.Net.BCrypt.EnhancedVerify("TEST", enhancedHashPassword, hashType: HashType.SHA384);

            var builder = new DbConnectionStringBuilder();
			builder.ConnectionString = "Server=Localhost;Database=TestContext;User Id=sa;Password=LucidNala88!;TrustServerCertificate=true";
			Console.WriteLine();
            await base.OnInitializedAsync();
        }
    }
}

