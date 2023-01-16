using Generator.Components.Args;
using Generator.Components.Interfaces;
using Generator.Components.Enums;
using Mapster;
using MudBlazor;
using Generator.Shared.Extensions;
using Force.DeepCloner;
using static MudBlazor.CategoryTypes;
using Microsoft.AspNetCore.Components.Forms;

namespace Generator.Components.Components;

public partial class GenGrid<TModel> 
{
 
    public async Task OnCreateClick()
    {
       await GridManager.Create();
     }

    private async Task OnCommit(object model)
    {
        Components.ForEach(x => x.ValidateObject());

        if (HasErrors()) return;

        await InvokeCallBackByViewState(SelectedItem);
    }

    private bool SearchFunction(TModel model)
    {
        if (string.IsNullOrEmpty(_searchString)) return true;

        var searchableFields = GetComponentsOf<IGenTextField>()
            .Where((x) => x.BindingField is not null && x.VisibleOnGrid);

        return searchableFields.Select(field => model.GetPropertyValue(field.BindingField)).Where(columnValue => columnValue is not null).Any(columnValue => columnValue.ToString()!.Contains(_searchString, StringComparison.OrdinalIgnoreCase));
    }

    private MudTr GetCurrentRow()
    {
        var selectedItem = EditButtonActionList.FirstOrDefault(x => (x.Target?.CastTo<MudTr>()).Item.CastTo<TModel>().Equals(SelectedItem));

        if (selectedItem is not null)
        {
            var row = selectedItem.Target.CastTo<MudTr>();
            return row;
        }

        return null;
        
    }

    internal void RefreshButtonState()
    {
        var row = GetCurrentRow();

        if (row is not null)
        {
            row.SetFieldValue("hasBeenCanceled", false);
            row.SetFieldValue("hasBeenCommitted", true);
            row.SetFieldValue("hasBeenClickedFirstTime", false);
        }

        SearchDisabled = false;
        NewDisabled = false;
        ExpandDisabled = false;

        StateHasChanged();
    }

    private void RollBack()
    {
        GetCurrentRow().ManagePreviousEdition();
    }
    /// <summary>
    /// When a new item is added in inlinemode, for better user experience it has to be on the first row of the table,
    /// therefore the newly added item is inserted to 0 index of datasource however the component is not rendered at that moment
    /// and this method must be called on after render method
    /// </summary>
    /// <returns></returns>
    //private async Task OnNewItemAddEditInvoker()
    //{
    //    if (ViewState == ViewState.Create && EditMode == EditMode.Inline)
    //    {
    //        if ((!EditButtonActionList.Any() && EditButtonRef is not null) || !IgnoreErrors)
    //        {
    //            await EditButtonRef.OnClick.InvokeAsync();

    //            IgnoreErrors = true;

    //            return;
    //        }

    //        var firstItem = EditButtonActionList.FirstOrDefault(x => (x.Target?.CastTo<MudTr>()).Item.CastTo<TModel>().Equals(SelectedItem));

    //        if (firstItem is null)
    //            return;

    //        var row = firstItem.Target.CastTo<MudTr>();

    //        if (row.IsEditing)
    //        {
    //            row.SetFieldValue("hasBeenCanceled", false);
    //            row.SetFieldValue("hasBeenCommitted", false);
    //            row.SetFieldValue("hasBeenClickedFirstTime", false);
    //        }

    //        firstItem?.Invoke();

    //        EditButtonActionList.Clear();

    //        return;
    //    }

    //    if (ViewState == ViewState.Update && EditMode == EditMode.Inline )
    //    {
    //        if ((!EditButtonActionList.Any() && EditButtonRef is not null) || !IgnoreErrors)
    //        {
    //            await EditButtonRef.OnClick.InvokeAsync();

    //            IgnoreErrors = true;

    //            return;
    //        }

    //        var firstItem = EditButtonActionList.FirstOrDefault(x => (x.Target?.CastTo<MudTr>()).Item.CastTo<TModel>().Equals(SelectedItem));

    //        if (firstItem is null)
    //            return;

    //        firstItem?.Invoke();

    //        EditButtonActionList.Clear();

    //        return;

    //    }   
    //}

    internal async ValueTask EditRow()
    {
        if (EditMode != EditMode.Inline)
        {
            await GridManager.Edit();
            return;
        }
         
        await InvokeLoad();
    }

    internal void OnBackUp(TModel element)
    {
        if (HasErrors()) return;

        NewDisabled = true;

        ExpandDisabled = true;

        SearchDisabled = true;

        SelectedItem = element;//.DeepClone();

        OriginalEditItem = element.DeepClone();

      
    }


    public async Task InvokeLoad()
    {
        if (Load.HasDelegate)
            await Load.InvokeAsync(this);
    }

    public async Task OnDeleteClicked(Action buttonAction)
    {
        ViewState = ViewState.Delete;

        TModel dataToRemove = (TModel)buttonAction.Target.CastTo<MudTr>().Item;
        OriginalEditItem = dataToRemove;

        await  InvokeCallBackByViewState(dataToRemove.CastTo<TModel>());
 
    }

    public new void StateHasChanged()
    {
        base.StateHasChanged();
    }
}
