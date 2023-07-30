using System.Data;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.NonDB;
using Generator.Shared.Services;
using Generator.UI.Extensions;
using Generator.UI.Helpers;
using Generator.UI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Generator.UI.Pages.GeneratorPages;

public partial class GridMPage  
{
    [Inject]
    public List<GRID_M> GridList { get; set; }

    public List<CODE_ENUM> CommandTypes { get; set; }

    public List<DATABASE_INFORMATION> DatabaseList { get; set; }

	public List<TABLE_INFORMATION> TableList { get; set; }

	public List<STORED_PROCEDURES> StoredProcedureList { get; set; }

	private GenComboBox DatabaseComboBox;

    private GenComboBox StoredProcedureCombo;

    private GenTextField QuryTextField;

    private GenComboBox CrudSourceComboBox;

    private GenComboBox CommandTypeComboBox;

    private GenCheckBox CreateCheckBox;

    private GenCheckBox UpdateCheckBox;

    private GenCheckBox DeleteCheckBox;

    [Inject]
    public IRolesService roleService { get; set; }

    protected override async Task OnInitializedAsync()
	{
		DataSource = GridList;

		CommandTypes = DataHelpers.GetEnumValues<CommandType>().Where(x => x.C_CODE != 0x200).ToList();

        DateTime startTime = DateTime.UtcNow;

		var token = await AuthService.Request(8, "licentia");

		//var test = await Service.SetToken(token).ReadAll();

		DatabaseList = (await DatabaseService.GetDatabaseList()).Data;
 
        TimeSpan executionTime = DateTime.UtcNow - startTime;

        double seconds = executionTime.TotalSeconds;
        double milliseconds = executionTime.TotalMilliseconds;

		await base.OnInitializedAsync();
		//Console.WriteLine();
    }
	 
	protected override async Task Delete(GenArgs<GRID_M> args)
    {
        await base.Delete(args);
    }

    protected override async Task Load(IGenView<GRID_M> View)
    {
        await base.Load(View);

        if(View.ViewState == Components.Enums.ViewState.Update)
        {
            await FillCrudSourceCombobox();

            if (View.SelectedItem.CB_COMMAND_TYPE == 4 && !string.IsNullOrEmpty(View.SelectedItem.CB_DATABASE))//storedProcedure ise
                await FillStoredProceduresCombobox();
        }


        //await CheckRules();

    }

    private async Task DatabaseChanged(object model)
    {
        if (model is not DATABASE_INFORMATION data) return;

        DatabaseComboBox.SetValue(data);

        await FillCrudSourceCombobox();
    }


    private void CrudSourceComboBoxChangedAsync(object model)
    {
        if (model is not TABLE_INFORMATION data) return;

        CrudSourceComboBox.SetValue(model);

        //await CheckRules();
     }

    private async Task CommandTypeComboBoxChangedAsync(object model)
    {
        if (model is not CODE_ENUM data) return;

        View.SelectedItem.CB_COMMAND_TYPE = data.C_CODE;
        //CommandTypeComboBox.SetValue(model);

        if(View.SelectedItem.CB_COMMAND_TYPE == 4)
        {
            if (string.IsNullOrEmpty(View.SelectedItem.CB_DATABASE))
            {
                NotificationsView.Notifications.Add(new NotificationVM("Database not selected!", MudBlazor.Severity.Error));
                NotificationsView.Fire();
            }
            else
                await FillStoredProceduresCombobox();
        }
        View.SelectedItem.CB_QUERY_OR_METHOD = string.Empty;
        View.StateHasChanged();
    }

  
    //private void OnCrudSourceComboBoxClear(MouseEventArgs args)
    //{
    //    CrudSourceComboBox.OnClearClicked(args);
    //}

 
    private async Task FillCrudSourceCombobox()
    {
        TableList = await CrudSourceComboBox.FillAsync(() => DatabaseService.GetTableListAsync(View.SelectedItem.CB_DATABASE));
    }
    private async Task FillStoredProceduresCombobox()
    {
        StoredProcedureList = await StoredProcedureCombo.FillAsync(() => DatabaseService.GetStoredProcedures(View.SelectedItem.CB_DATABASE));
    }
}