using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Components;

 
public class GenDateRangePicker : MudDateRangePicker, IGenDateRangePicker, IComponentMethods<GenDatePicker>
{
    [CascadingParameter(Name = nameof(IGenComponent.Parent))]
    IPageBase IGenComponent.Parent { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }


    [Parameter]
    public DateRange InitialValue { get; set; }

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    Type IGenComponent.DataType { get; set; } = typeof(DateTime);

    object IGenComponent.GetDefaultValue => ((IGenComponent)this).DataType.GetDefaultValue();

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
    public bool ClearIfNotVisible { get; set; } = false;

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


    [CascadingParameter(Name = nameof(IGenComponent.IsSearchField))]
    bool IGenComponent.IsSearchField { get; set; }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> EditorEnabledIf { get; set; }

    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }


 
    protected new async Task SetDateRangeAsync(DateRange dateRange, bool updateValue)
    {
        await base.SetDateRangeAsync(dateRange, updateValue);
        await OnDateRangeChanged.InvokeAsync((Model, dateRange));
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
 
        AddComponents();

        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

        if (Model is null || Model.GetType().Name == "Object") return;

        if (InitialValue is not null && DateRange is null)
        {
            await SetDateRangeAsync(InitialValue, true);
            //await SetDateAsync(InitialValue, true);
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Model is not null && Model.GetType().Name != "Object")
            base.BuildRenderTree(builder);

        AddComponents();
    }

    public DateTime? _originalDate;

    public void SetValue(DateRange date)
    {

        if (this is not IGenComponent comp) return;

        if (date.Start is null && date.End is null)
            date = null;

        comp.SetSearchValue(date);

        //comp.Parent.StateHasChanged();
        //comp.Parent.CurrentGenPage?.StateHasChanged();
    }



    protected override void OnClosed()
    {
        if (!Error)
        {
            if (((IGenComponent)this).IsSearchField)
                ((INonGenGrid)((IGenComponent)this).Parent)?.ValidateSearchField(BindingField);

            if (((IGenComponent)this).Parent is INonGenGrid grid)
                grid.ValidateField(BindingField);

        }


        base.OnClosed();
    }

    private void AddComponents()
    {
        if (((IGenComponent)this).IsSearchField)
            ((INonGenGrid)((IGenComponent)this).Parent)?.AddSearchFieldComponent(this);
      
    }


    [Parameter]
    public EventCallback<(object Model, DateRange Value)> OnDateRangeChanged { get; set; }



    private void SetCallBackEvents()
    {
        if (((IGenComponent)this).IsSearchField)
        {

            DateRangeChanged = EventCallback.Factory.Create<DateRange>(this, x =>
            {
                SetValue(x.CastTo<DateRange>());
                Validate();
            });

 
        }
 
    }
 

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => (builder) =>
    {
        //if (Model is null || Model.GetType().Name == "Object")
        if (Model?.GetType().Name == "Object" || !((IGenComponent)this).IsSearchField)
            Model = model;

        SetCallBackEvents();

      

        var valDate = (DateRange)Model?.GetPropertyValue(BindingField);

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        //additionalParams.Add((nameof(_value), valDate));

        //additionalParams.Add((nameof(Date), valDate));

        additionalParams.Add((nameof(Disabled), !(EditorEnabledIf?.Invoke(Model) ?? EditorEnabled)));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        if (!Required && (!RequiredIf?.Invoke(Model) ?? false))
            Error = false;

        builder.RenderComponent(this, ignoreLabels, additionalParams.ToArray());
        //throw new NotImplementedException();
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var val = (DateTime?)model.GetPropertyValue(BindingField);

        if (val is not null)
            RenderExtensions.RenderGrid(builder, val.Value.ToString(DateFormat));

    };



    void IGenComponent.ValidateObject()
    {
        if (((IGenComponent)this).Parent is INonGenGrid grid)
            grid.ValidateField(BindingField);

        //((IGenComponent)this).Parent.ValidateField(BindingField);
    }
    public object GetValue()
    {
        if (((IGenComponent)this).IsSearchField)
            return ((IGenComponent)this).GetSearchValue();
        else
            return this.GetFieldValue(nameof(_value));
    }

    void IGenComponent.SetSearchValue(object Value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = Value;
        ((IGenComponent)this).Parent.StateHasChanged();

    }

    object IGenComponent.GetSearchValue()
    {
        return Model.GetPropertyValue(BindingField);
    }

    void IGenComponent.SetValue(object value)
    {
        SetValue((DateRange)value);
    }

    void IGenComponent.SetEmpty()
    {

        SetValue(null);

        _value = null;
    }

    public Task Clear()
    {
        base.Clear();

        ((IGenComponent)this).SetEmpty();

        return Task.CompletedTask;
    }

    public new bool Validate()
    {
        if (((IGenComponent)this).IsSearchField)
            return ((INonGenGrid)((IGenComponent)this).Parent).ValidateSearchField(BindingField);

        if (((IGenComponent)this).Parent is INonGenGrid grid)
            return grid.ValidateField(BindingField);

        return true;
    }

    bool IGenComponent.IsEditorVisible(object model)
    {
        return ((IGenComponent)this).EditorVisibleIf?.Invoke(model) ?? ((IGenComponent)this).EditorVisible;
    }

    bool IGenComponent.IsRequired(object model)
    {
        return ((IGenComponent)this).RequiredIf?.Invoke(model) ?? ((IGenComponent)this).Required;
    }

    void IGenComponent.ValidateField()
    {
        if (Model is null) return;

        if (((IGenComponent)this).IsEditorVisible(Model))
        {
            var loValue = Model.GetPropertyValue(BindingField);

            if (RequiredIf is not null)
            {
                Error = RequiredIf.Invoke(Model);
            }
            else if (Required)
            {
                Error = loValue is null || loValue.ToString() == string.Empty;
            }
        }
    }
}

