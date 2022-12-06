using System;
using Generator.Components.Components;
using Generator.Shared.Extensions;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Example.Pages
{
	public partial class GenericComponentTest:ComponentBase
	{
        [Inject]
        public ITestService ITestService { get; set; }

        public IDictionary<string,object> firstData { get; set; }

        public GenTextField GenTextField { get; set; }


        protected  override async Task OnInitializedAsync()
        {
            await QueryAsync();

        }
        private async Task QueryAsync()
        {
            var result = await ITestService.QueryAsync();
            var data = result.Data.Deserialize<List<IDictionary<string, object>>>();

            firstData = data.First();

            GenTextField = new GenTextField();
            GenTextField.Variant = MudBlazor.Variant.Outlined;
            //GenTextField.Context = firstData;
            GenTextField.BindingField = "TT_DESC";
            GenTextField.Converter = StringConverter;
            //GenTextField.ValueChanged = EventCallback.Factory.Create<object>(this, (x) =>
            //{
            //    Console.WriteLine(GenTextField.GetType().Name);
            //    //GenTextField.ComponentRef.Context.SetPropertyValue(GenTextField.BindingField,x);
            //});

            Console.WriteLine();
        }
         
        Converter<object> StringConverter = new Converter<object>
        {
            SetFunc = value => value?.ToString(),
            GetFunc = text => text?.ToString(),
        };
    }
}

