using Generator.Components.Args;
using Generator.Components.Interfaces;
using Generator.Components.Enums;
using Mapster;
using MudBlazor;
using Generator.Shared.Extensions;
using Force.DeepCloner;

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


  

    private async Task OnCommit(object model)
    {
        Components.ForEach(x => x.ValidateObject());

        if (HasErrors()) return;

 
        await InvokeCallBackByViewState(model);
    }


    private bool SearchFunction(object model)
    {
        if (string.IsNullOrEmpty(_searchString)) return true;

        var searchableFields = GetComponentsOf<IGenTextField>()
            .Where((x) => x.BindingField is not null && x.VisibleOnGrid);

        //foreach (var field in searchableFields)
        //{
        //    var columnValue = model.GetPropertyValue(field.BindingField);

        //    if (columnValue is null) continue;

        //    if (columnValue.ToString()!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
        //        return true;
        //}

        return searchableFields.Select(field => model.GetPropertyValue(field.BindingField)).Where(columnValue => columnValue is not null).Any(columnValue => columnValue.ToString()!.Contains(_searchString, StringComparison.OrdinalIgnoreCase));
    }


     /// <summary>
    /// When a new item is added in inlinemode, for better user experience it has to be on the first row of the table,
    /// therefore the newly added item is inserted to 0 index of datasource however the component is not rendered at that moment
    /// and this method must be called on after render method
    /// </summary>
    /// <returns></returns>
    private async Task OnNewItemAddEditInvoker()
    {
        if (ViewState == ViewState.Create && EditMode == EditMode.Inline)
        {
            if ((!EditButtonActionList.Any() && EditButtonRef is not null) || !IgnoreErrors)
            {
                await EditButtonRef.OnClick.InvokeAsync();

                IgnoreErrors = true;

                return;
            }

            var firstItem = EditButtonActionList.FirstOrDefault(x => x.Target?.CastTo<MudTr>().Item == SelectedItem);

            if (firstItem is null)
                return;

            firstItem?.Invoke();

            EditButtonActionList.Clear();

            return;
        }

        if (ViewState == ViewState.Update && EditMode == EditMode.Inline )
        {
            if ((!EditButtonActionList.Any() && EditButtonRef is not null) || !IgnoreErrors)
            {
                await EditButtonRef.OnClick.InvokeAsync();

                IgnoreErrors = true;

                return;
            }

            var firstItem = EditButtonActionList.FirstOrDefault(x => x.Target?.CastTo<MudTr>().Item == SelectedItem);

            firstItem?.Invoke();

            EditButtonActionList.Clear();

            return;

        }
    }

    private void OnBackUp(object element)
    {
        if (HasErrors()) return;

        NewDisabled = true;
        ExpandDisabled = true;
        SearchDisabled = true;
        SelectedItem = element;
        OriginalEditItem = element.DeepClone();
    }

    public async Task OnDeleteClicked(Action buttonAction)
    {
        ViewState = ViewState.Delete;

        var dataToRemove = buttonAction.Target.CastTo<MudTr>().Item;

        await  InvokeCallBackByViewState(dataToRemove);
 
    }
}
