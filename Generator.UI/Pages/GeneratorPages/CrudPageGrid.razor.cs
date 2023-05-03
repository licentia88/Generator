using System.Data;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.UI.Pages.GeneratorPages
{
    public partial class CrudPageGrid :PagesBase<PAGES_M, IPagesMService>
    {
		public List<DATABASE_INFORMATION> DatabaseList { get; set; }

		public List<TABLE_INFORMATION> TableList { get; set; }

        public List<STORED_PROCEDURES> StoredProcedureList { get; set; }

        private GenComboBox DatabaseComboBox;

        private GenTextField QueryComboBox;


        protected override async Task OnInitializedAsync()
        {
            DatabaseList = (await DatabaseService.GetDatabaseList()).Data;
        }
        public Task Search(SearchArgs args)
		{
			DataSource = new();

			return Task.CompletedTask;
        }

		public Task CreateAsync(PAGES_M model)
		{


			return Task.CompletedTask;
		}

        public Task Load(IGenView<PAGES_M> View)
        {
            View.SelectedItem.CB_COMMAND_TYPE = (int)CommandType.Text;
            return Task.CompletedTask;
        }
		private  Task DatabaseChanged(object model)
		{
            if (model is not DATABASE_INFORMATION diModel) return Task.CompletedTask;
			DatabaseComboBox.SetValue(diModel);

            return Task.CompletedTask;
         }

        

    }
}

