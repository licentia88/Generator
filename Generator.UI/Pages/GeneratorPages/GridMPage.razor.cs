using System;
using System.Data;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.InkML;
using Generator.Client;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.NonDB;
using Grpc.Core;

namespace Generator.UI.Pages.GeneratorPages;

public partial class GridMPage  
{
	public List<DATABASE_INFORMATION> DatabaseList { get; set; }

	public List<TABLE_INFORMATION> TableList { get; set; }

	public List<STORED_PROCEDURES> StoredProcedureList { get; set; }

	private GenComboBox DatabaseComboBox;

	private GenTextField QueryComboBox;

 
	protected override async Task OnInitializedAsync()
	{
        DateTime startTime = DateTime.UtcNow;

		DatabaseList = (await DatabaseService.GetDatabaseList()).Data;
 
        TimeSpan executionTime = DateTime.UtcNow - startTime;
        double seconds = executionTime.TotalSeconds;
        double milliseconds = executionTime.TotalMilliseconds;

		await base.OnInitializedAsync();
    }
	 
 
	private  Task DatabaseChanged(object model)
	{
		if (model is not DATABASE_INFORMATION diModel) return Task.CompletedTask;
		DatabaseComboBox.SetValue(diModel);

		return Task.CompletedTask;
	}

    

    public override Task Load(IGenView<GRID_M> View)
    {
        View.SelectedItem.CB_COMMAND_TYPE = (int)CommandType.Text;
        return Task.CompletedTask;
    }
}