﻿using System.Data;
using Generator.Client.Hubs;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.NonDB;
using MessagePipe;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.GeneratorPages;

public partial class GridMPage  
{
	public List<DATABASE_INFORMATION> DatabaseList { get; set; }

	public List<TABLE_INFORMATION> TableList { get; set; }

	public List<STORED_PROCEDURES> StoredProcedureList { get; set; }

	private GenComboBox DatabaseComboBox;

	private GenTextField QueryComboBox;

	[Inject]
	public List<GRID_M> GridList { get; set; }

	[Inject]
	public ISubscriber<Guid,PERMISSIONS> Subs { get; set; }

    [Parameter]
    public Guid ID { get; set; }

    protected override async Task OnInitializedAsync()
	{

		Subs.Subscribe(ID, x =>
		{
			Console.WriteLine(x.AUTH_NAME);
		});
		DataSource = GridList;
 
        DateTime startTime = DateTime.UtcNow;

		var token = await AuthService.Request(8, "licentia");

		//var test = await Service.SetToken(token).ReadAll();

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
 
	protected override async Task Delete(GenArgs<GRID_M> args)
    {
        await base.Delete(args);
    }

	protected override Task Load(IGenView<GRID_M> View)
    {
        View.SelectedItem.CB_COMMAND_TYPE = (int)CommandType.Text;
        return Task.CompletedTask;
    }
}