using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Args;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Components;

 
public class GenDateRangePicker : MudDateRangePicker, IGenDateRangePicker, IDisposable, IComponentMethods<GenDatePicker>
{
    [CascadingParameter(Name = nameof(IGenControl.Parent))]
    IPageBase IGenComponent.Parent { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }


    // [Parameter,edi]
    public DateRange InitialValue { get; set; }

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    Type IGenControl.DataType { get; set; } = typeof(DateTime);

    object IGenControl.GetDefaultValue => ((IGenControl)this).DataType.GetDefaultValue();

    [Parameter]
    [Range(1, 12, ErrorMessage = "Column width must be between 1 and 12")]
    public int Width { get; set; }

    [Parameter]
    public int Order { get; set; }

    //[Parameter]
    //public bool EditorEnabled { get; set; } = true;

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


    [CascadingParameter(Name = nameof(IGenControl.IsSearchField))]
    bool IGenControl.IsSearchField { get; set; }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> DisabledIf { get; set; }

    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }


 
    protected new async Task SetDateRangeAsync(DateRange dateRange, bool updateValue)
    {
        await base.SetDateRangeAsync(dateRange, updateValue);
        await OnDateRangeChanged.InvokeAsync(new ValueChangedArgs<DateRange>(Model,DateRange,dateRange,((IGenControl)this).IsSearchField));
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
        if (((IGenControl)this).Parent is not null && Model is not null)
            base.BuildRenderTree(builder);

        //if (Model is not null && Model.GetType().Name != "Object")
        //    base.BuildRenderTree(builder);

        AddComponents();
    }

    public DateTime? _originalDate;

    public void SetValue(DateRange date)
    {

        if (this is not IGenControl comp) return;

        if (date.Start is null && date.End is null)
            date = null;

        comp.SetSearchValue(date);

        comp.Parent.StateHasChanged();
        if (comp.Parent is INonGenGrid grid)
            grid.CurrentGenPage?.StateHasChanged();
    }


    protected override Task OnClosedAsync()
    {
        if (Error) return base.OnClosedAsync();
        if (((IGenControl)this).IsSearchField)
            ((INonGenGrid)((IGenControl)this).Parent)?.ValidateSearchField(BindingField);

        if (((IGenControl)this).Parent is INonGenGrid grid)
            grid.ValidateField(BindingField);
        return base.OnClosedAsync();
    }

    

    private void AddComponents()
    {
        if (((IGenControl)this).IsSearchField)
            ((INonGenGrid)((IGenControl)this).Parent)?.AddSearchFieldComponent(this);
      
    }


    [Parameter]
    public EventCallback<ValueChangedArgs<DateRange>> OnDateRangeChanged { get; set; }



    private void SetCallBackEvents()
    {
        if (((IGenControl)this).IsSearchField)
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
        //if (Model?.GetType().Name == "Object" || !((IGenComponent)this).IsSearchField)
        //    Model = model;


        if (!((IGenControl)this).IsSearchField)
            Model = model;

        if (((IGenControl)this).IsSearchField && Model is null)
        {
            Model = model;
        }

        SetCallBackEvents();

      

        var valDate = (DateRange)Model?.GetPropertyValue(BindingField);

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        additionalParams.Add((nameof(Label), Label is null or "" ? " " : Label));

        //additionalParams.Add((nameof(_value), valDate));

        //additionalParams.Add((nameof(Date), valDate));
        additionalParams.Add(("tabindex", -1));
        additionalParams.Add((nameof(Disabled), DisabledIf?.Invoke(Model) ?? Disabled));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        additionalParams.Add((nameof(Color), ((IGenControl)this).Parent.TemplateColor));

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



    void IGenControl.ValidateObject()
    {
        if (((IGenControl)this).Parent is INonGenGrid grid)
            grid.ValidateField(BindingField);

        //((IGenComponent)this).Parent.ValidateField(BindingField);
    }
    // public object GetValue()
    // {
    //     if (((IGenControl)this).IsSearchField)
    //         return ((IGenControl)this).GetSearchValue();
    //     else
    //         return Model.GetFieldValue(BindingField);
    //
    // }

    void IGenControl.SetSearchValue(object Value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = Value;
        ((IGenControl)this).Parent.StateHasChanged();

    }

    object IGenControl.GetValue()
    {
        return Model.GetPropertyValue(BindingField);
    }

    void IGenControl.SetValue(object value)
    {
        SetValue((DateRange)value);
    }

    void IGenControl.SetEmpty()
    {

        SetValue(null);

        _value = null;
    }

    public Task ClearAsync()
    {
        ClearAsync(true);
        return Task.CompletedTask;
    }

    public new bool Validate()
    {
        if (((IGenControl)this).IsSearchField)
            return ((INonGenGrid)((IGenControl)this).Parent).ValidateSearchField(BindingField);

        else if (((IGenControl)this).Parent is INonGenGrid grid)
            return grid.ValidateField(BindingField);

        return true;
    }

    bool IGenComponent.IsEditorVisible(object model)
    {
        return ((IGenControl)this).EditorVisibleIf?.Invoke(model) ?? ((IGenControl)this).EditorVisible;
    }

    bool IGenControl.IsRequired(object model)
    {
        return ((IGenControl)this).RequiredIf?.Invoke(model) ?? ((IGenControl)this).Required;
    }

    void IGenControl.ValidateField()
    {
        if (Model is null) return;

        if (((IGenControl)this).IsEditorVisible(Model))
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
    
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose managed resources
            Model = null;
            // ((IGenComponent)this).Parent = null;
            InitialValue = null;
            // BindingField = null;
            EditorVisibleIf = null;
            DisabledIf = null;
            RequiredIf = null;
            OnClick=default;
            OnDateRangeChanged=default;
            TextChanged = default;
            PickerMonthChanged = default;
        }

        base.Dispose(disposing);
    }
}

