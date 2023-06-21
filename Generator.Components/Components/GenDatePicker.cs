using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Components.Validators;
//using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Generator.Components.Components;

public class GenDatePicker : MudDatePicker, IGenDatePicker, IComponentMethods<GenDatePicker>
{
    [CascadingParameter(Name = nameof(ParentGrid))]
    public INonGenGrid  ParentGrid { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

     

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    public Type DataType { get; set; } = typeof(DateTime);

    public object GetDefaultValue => DataType.GetDefaultValue();

    [Parameter]
    [Range(1, 12, ErrorMessage = "Column width must be between 1 and 12")]
    public int Width { get; set; }

    [Parameter]
    public int Order { get; set; }

    [Parameter]
    public bool EditorEnabled { get; set; } = true;

    [Parameter]
    public bool EditorVisible { get; set; } = true;

    [Parameter]
    public bool GridVisible { get; set; } = true;

    [Parameter]
    public int xs { get; set; } = 12;

    [Parameter]
    public int sm { get; set; } = 12;

    [Parameter]
    public int md { get; set; } = 12;

    [Parameter]
    public int lg { get; set; } = 12;

    [Parameter]
    public int xl { get; set; } = 12;

    [Parameter]
    public int xxl { get; set; } = 12;


    [CascadingParameter(Name = nameof(IsSearchField))]
    public bool IsSearchField { get; set; }

       
    protected override Task OnInitializedAsync()
    {
        Initialize();
        if (IsSearchField)
            ParentGrid?.AddSearchFieldComponent(this);
        else
            ParentGrid?.AddChildComponent(this);

        return Task.CompletedTask;
    }

    public void Initialize()
    {
        //if (ParentGrid.EditMode != Enums.EditMode.Inline && ParentGrid.CurrentGenPage is null) return;

        Date = (DateTime?)Model?.GetPropertyValue(BindingField);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Model is not null && Model.GetType().Name != "Object")
            base.BuildRenderTree(builder);

        AddComponents();
    }

    public void SetValue(DateTime? date)
    {
        Model?.SetPropertyValue(BindingField, date);

        ParentGrid.StateHasChanged();
        ParentGrid.CurrentGenPage?.StateHasChanged();
        //ParentGrid.ValidateValue(BindingField);
    }

    protected override void OnClosed()
    {
        if (!Error)
        {
            AddComponents();
        }
            

        base.OnClosed();
    }

    private void AddComponents()
    {
        if (!IsSearchField)
            ParentGrid.ValidateValue(BindingField);
        else
            ParentGrid.ValidateSearchFields(BindingField);
    }

    private void SetCallBackEvents()
    {
        if (IsSearchField)
        {
            this.DateChanged = EventCallback.Factory.Create<DateTime?>(this, x =>
            {
                SetValue(x.CastTo<DateTime?>());
                ParentGrid.ValidateSearchFields(BindingField);
            });
        }
        else
        {
            if (!DateChanged.HasDelegate)
            {
                DateChanged = EventCallback.Factory.Create<DateTime?>(this, x => SetValue(x.CastTo<DateTime?>()));
            }
        }
    }

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    {
        RenderAsComponent(model, ignoreLabels,new KeyValuePair<string, object>(nameof(Disabled),!EditorEnabled)).Invoke(builder);
    };

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params KeyValuePair<string, object>[] valuePairs) => (builder) =>
    {
        if (Model is null || Model.GetType().Name == "Object")
            Model = model;

        SetCallBackEvents();

        var valDate = (DateTime?)Model?.GetPropertyValue(BindingField);

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();
        additionalParams.Add((nameof(_value), valDate));
        builder.RenderComponent(this, ignoreLabels,  additionalParams.ToArray());
        //throw new NotImplementedException();
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var val = (DateTime?)model.GetPropertyValue(BindingField);
             
        if (val is not null)
            RenderExtensions.RenderGrid(builder, val.Value.ToString(DateFormat));

    };

  

    public void ValidateObject()
    {
        ParentGrid.ValidateValue(BindingField);
    }
    public object GetValue()
    {
        return this.GetFieldValue(nameof(_value));
    }

    public void SetSearchValue(object Value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = Value;
        ParentGrid.StateHasChanged();

    }

    public object GetSearchValue()
    {
        return Model.GetPropertyValue(BindingField);
    }

    
}