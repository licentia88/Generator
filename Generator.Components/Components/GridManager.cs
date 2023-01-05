using Generator.Components.Enums;
using Generator.Shared.Extensions;
using Mapster;

namespace Generator.Components.Components;

public class GridManager
{
    public GenGrid Grid { get; }

    public GridManager(GenGrid grid)
    {
        Grid = grid;
    }

    public async Task Create()
    {
        Grid.EditButtonActionList.Clear();

        Grid.ViewState = ViewState.Create;

        var datasourceModelType = Grid.DataSource.GetType().GenericTypeArguments[0];

        var newData = Grid.Components.Where(x => x is not GenSpacer).ToDictionary(comp => comp.BindingField, comp => comp.GetDefaultValue);

        var adaptedData = newData.Adapt(typeof(Dictionary<string, object>), datasourceModelType);

        Grid.SelectedItem = adaptedData;

        if (Grid.EditMode == EditMode.Inline)
        {
            Grid.DataSource.Insert(0, Grid.SelectedItem);

            return;
        }


        await Grid.ShowDialogAsync<GenPage>();

    }

    public async Task Edit()
    {
        Grid.ViewState = ViewState.Update;

        await Grid.ShowDialogAsync<GenPage>();

    }
}
