using System;
using System.Collections.ObjectModel;
using Generator.Examples.Shared;
using Generator.Shared.Extensions;
using GenFu;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.InjectionTest
{
	public class InjectionClass
	{
		[Inject]
		public Lazy<List<USER>> UserList { get; set; }

		public InjectionClass(IServiceProvider services)
		{
			UserList = services.GetService<Lazy<List<USER>>>();

        }

		public void FillTable()
		{
            UserList.Value.AddRange(A.ListOf<USER>());

        }
    }
}

