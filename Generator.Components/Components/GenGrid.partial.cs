using Generator.Components.Args;
using Generator.Components.Interfaces;
using Generator.Components.Enums;
using Mapster;
using MudBlazor;
using Generator.Shared.Extensions;

namespace Generator.Components.Components;

public partial class GenGrid : MudTable<object>, IGenGrid
{

    public async Task InvokeLoad()
    {
        if (Load.HasDelegate)
            await Load.InvokeAsync(this);
    }

    public async Task OnCreateClick()
    {
        EditButtonActionList.Clear();

        ViewState = ViewState.Create;

        var datasourceModelType = DataSource.GetType().GenericTypeArguments[0];

        var newData = Components.Where(x=> x is not GenSpacer).ToDictionary(comp => comp.BindingField, comp => comp.GetDefaultValue);

        var adaptedData = newData.Adapt(typeof(Dictionary<string, object>), datasourceModelType);

        SelectedItem = adaptedData;

        if (EditMode == EditMode.Inline)
        {
            DataSource.Insert(0, SelectedItem);

            await InvokeLoad();

            return;
        }

        var paramList = new List<(string, object)>();
        paramList.Add((nameof(GenPage.ViewModel), SelectedItem));
        paramList.Add((nameof(GenPage.GenGrid), this));

        await ShowDialogAsync<GenPage>(paramList.ToArray());
 
        //if (Create.HasDelegate)
        //    await Create.InvokeAsync(new GenGridArgs(null, SelectedItem));

    }
    //public void Create()
    //{

    //}

    //public void Update()
    //{

    //}

    //public void Delete()
    //{

    //}

    //public void Load()
    //{

    //}

    //public void Cancel()
    //{

    //}

    //public void BackUp(object element)
    //{

    //}


    //public void OnCommit()
    //{

    //}

}
