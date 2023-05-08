using Generator.Examples.Shared;
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
            UserList.Value.AddRange(GenFu.GenFu.ListOf<USER>(300));

        }
    }
}

