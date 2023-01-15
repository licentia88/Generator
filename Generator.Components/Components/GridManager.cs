using Force.DeepCloner;
using Generator.Components.Enums;
using Generator.Shared.Extensions;
using Mapster;
using static MudBlazor.CategoryTypes;

namespace Generator.Components.Components;

public class GridManager<TModel> where TModel :new()
{
    public GenGrid<TModel> Grid { get; }

    public GridManager(GenGrid<TModel> grid)
    {
        Grid = grid;
    }

    public async Task Create()
    {

        Grid.EditButtonActionList.Clear();

        Grid.ViewState = ViewState.Create;

        var newData = Grid.Components.Where(x => x is not GenSpacer).ToDictionary(comp => comp.BindingField, comp => comp.GetDefaultValue);

        TypeAdapterConfig.GlobalSettings.NewConfig(newData.GetType(), typeof(TModel)).AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);

        var adaptedData = newData.Adapt<TModel>();

        Grid.SelectedItem = adaptedData;

        if (Grid.EditMode == EditMode.Inline)
        {
            Grid.DataSource.Insert(0,Grid.SelectedItem);

            Grid.SetEditingItem(Grid.SelectedItem);
            //Grid.SetSelectedItem(Grid.SelectedItem);

            //Grid.originalTable.SetEditingItem(Grid.SelectedItem);

            //Grid.originalTable.SetSelectedItem(Grid.SelectedItem);
            return;
        }

        Grid.OnBackUp(Grid.SelectedItem);
        await Grid.ShowDialogAsync<GenPage<TModel>>();

    }

    public async Task Edit()
    {
        Grid.ViewState = ViewState.Update;

        await Grid.ShowDialogAsync<GenPage<TModel>>();

    }
}
