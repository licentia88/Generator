using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Examples.Shared;
using Generator.Shared.Extensions;
using Generator.Shared.Models;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using MudBlazor;

namespace Generator.Example.Pages
{
	public partial class Users
	{
		[Inject]
		public IUserService UserService { get; set; }

        [Inject]
        public Lazy<List<USER>> userList { get; set; }

        public List<USER> DataSource { get; set; } = new();

        private IGenView<USER> View { get; set; }

        [Inject]
        public ITestService tser { get; set; }


        public async IAsyncEnumerable<RESPONSE_REQUEST> TestData()
        {
            foreach (var item in DataSource)
            {
                await Task.Delay(10000);
                Console.WriteLine("************SENDING************");

                yield return new RESPONSE_REQUEST { TableName = item.U_LASTNAME };
            }
        }
        protected override async Task OnInitializedAsync()
        {

            DataSource =  await UserService.ReadAsync();

            //DataSource = res;

            //DataSource = userList.Value;

            var test = tser.Subscribe(TestData());



            await foreach (RESPONSE_RESULT item in test)
            {
                Console.WriteLine("************RECEIVING************");
            }
           
        }


        public void OnBeforeLoad(IGenGrid<USER> grid)
        {
            //if (grid.SelectedItem.U_AGE == 0)
            //{
            //    grid.ShouldShowDialog = false;
            //}

            //grid.ShowDialog = false;
        }

        private GenTextField U_LASTNAME;
        private GenDatePicker U_REGISTER_DATE;

        public void OnDateChanged(DateTime? date)
        {
            U_REGISTER_DATE.SetValue(date);
            //U_LASTNAME.SetValue("VALUE SET!!");
        }

        public void OntextChanged(object date)
        {
            U_LASTNAME.SetValue("VALUE SET!!");
        }
        
        public  void Load(IGenView<USER> view)
        {
            //Console.WriteLine(view.ViewState.ToString());
            View = view;

            if (view.SelectedItem.U_AGE == 0)
                view.ShoulShowDialog = false;

            U_LASTNAME = view.GetComponent<GenTextField>(nameof(USER.U_LASTNAME));

            U_REGISTER_DATE = view.GetComponent<GenDatePicker>(nameof(USER.U_REGISTER_DATE));
        }

        public async ValueTask Search(SearchArgs components)
        {
            var wherestatements = components.WhereStatements;

            Console.WriteLine();
        }

        public async ValueTask CreateAsync(USER data)
        {
          
            //throw new Exception();
             var result = await UserService.CreateAsync(new Examples.Shared.RESPONSE_REQUEST<USER>(data));



            ////REQUIRED 
            data.U_ROWID = result.U_ROWID;
            data = result;

 
            DataSource.Add(result);
        }
        private bool IsDisabled = false;

        public void TEST()
        {
            IsDisabled = !IsDisabled;
        }

        public async ValueTask UpdateAsync(USER data)
        {
            var result = await UserService.UpdateAsync(new Examples.Shared.RESPONSE_REQUEST<USER>(data));

            var existing = DataSource.FirstOrDefault(x => x.U_ROWID == data.U_ROWID);

            DataSource.Replace(existing, data);
        }

        public async ValueTask DeleteAsync(USER data)
        {
            var result = await UserService.DeleteAsync(new Examples.Shared.RESPONSE_REQUEST<USER>(data));

            DataSource.Remove(data);
        }
    }
}

