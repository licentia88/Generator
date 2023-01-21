using Generator.Components.Components;
using MudBlazor;

namespace Generator.Components.Interfaces;

public interface IGenPage<TModel> : INonGenPage, IGenView<TModel> where TModel:new()
{
    public GenGrid<TModel> GenGrid { get; set; }
}

public interface INonGenPage: INonGenView
{
    public MudDialogInstance MudDialog { get; set; }

    public bool EnableModelValidation { get; set; }

    public bool IsTopLevel { get; set; }

    public void Close();

    public Task<bool> ValidateAsync();
}
