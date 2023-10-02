using Generator.Components.Args;
using Generator.Components.Helpers;
using Generator.Components.Validators;
using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;

public interface IGenGrid<TModel> : INonGenGrid, IGenView<TModel> where TModel:new()  
{

    public GeneratorJs GeneratorJs { get; set; }

    public GenExcel GenExcel { get; set; }

    public GenValidator<TModel> GenValidator { get; set; }

    public ICollection<TModel> DataSource { get; set; }

    public RenderFragment<TModel> GenDetailGrid { get; set; }

    public EventCallback<GenArgs<TModel>> Create { get; set; }

    public EventCallback<GenArgs<TModel>> Update { get; set; }

    public EventCallback<GenArgs<TModel>> Delete { get; set; }

    public EventCallback<GenArgs<TModel>> Cancel { get; set; }

    public EventCallback<TModel> OnBeforeSubmit { get; set; }

    public EventCallback<TModel> OnAfterSubmit { get; set; }

    public EventCallback<TModel> OnBeforeCancel { get; set; }

    public EventCallback<TModel> OnAfterCancel { get; set; }

    //public EventCallback<IGenGrid<TModel>> OnBeforeShowDialog { get; set; }

    public EventCallback Close { get; set; }

    public EventCallback<SearchArgs> Search { get; set; }

    Task OnDetailClicked(TModel context);
}
