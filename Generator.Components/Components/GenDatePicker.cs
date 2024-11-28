using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Args;
using Generator.Components.Enums;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Components;

public class GenDatePicker : MudDatePicker, IGenDatePicker, IAsyncDisposable, IComponentMethods<GenDatePicker>
{
    [CascadingParameter(Name = nameof(IGenComponent.Parent))]
    IPageBase IGenComponent.Parent { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }


    [Parameter]
    public DateTime? InitialValue { get; set; }

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
    public bool ClearIfNotVisible { get; set; }

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

    public new DateTime? Date
    {
        get
        {
            return _value;
        }
        set
        {
            SetDateAsync(value, updateValue: true).CatchAndLog();
        }
    }

 
    protected new  async Task SetDateAsync(DateTime? date, bool updateValue)
    {
        if (!(_value != date))
        {
            return;
        }
        Touched = true;
        if (date.HasValue && IsDateDisabledFunc(date.Value.Date))
        {
            await SetTextAsync(null, callback: false);
            return;
        }
        _value = date;
        if (updateValue)
        {
            Converter.GetError = false;
            await SetTextAsync(Converter.Set(_value), callback: false);
        }
        await DateChanged.InvokeAsync(_value);
        await BeginValidateAsync();
        FieldChanged(_value);

         await OnDateChanged.InvokeAsync(new ValueChangedArgs<DateTime?>(Model,Date,date,((IGenControl)this).IsSearchField));

    }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (Model is not null)
            Date = (DateTime?)Model?.GetPropertyValue(BindingField);

        AddComponents();

        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

        if (Model is null || Model.GetType().Name == "Object") return;

        if (InitialValue is not null && ((INonGenGrid)((IGenControl)this).Parent).ViewState != ViewState.Update)
            await SetDateAsync(InitialValue, true);


    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {

        if (((IGenControl)this).Parent is not null && Model is not null)
            base.BuildRenderTree(builder);

        //if (Model is not null && Model.GetType().Name != "Object")
        //    base.BuildRenderTree(builder);

        AddComponents();
    }

    public DateTime? OriginalDate;

    public async  void SetValue(DateTime? date)
    {
        
        // ReSharper disable once HeuristicUnreachableCode
        if (this is not IGenControl comp) return;

        if (comp.IsSearchField)
            comp.SetSearchValue(date);
        else
        {
            Model?.SetPropertyValue(BindingField, date);
            //await SetTextAsync(base.Converter.Set(date), true);

        }
        await OnDateChanged.InvokeAsync(new ValueChangedArgs<DateTime?>(Model,Date,date,((IGenControl)this).IsSearchField));


        comp.Parent.StateHasChanged();
        if (comp.Parent is INonGenGrid grid)
            grid.CurrentGenPage?.StateHasChanged();
        //comp.Parent.StateHasChanged();
        //comp.Parent.CurrentGenPage?.StateHasChanged();
    }


    protected override Task OnClosedAsync()
    {
        if (Error) return base.OnClosedAsync();
        if (((IGenControl)this).IsSearchField)
            ((INonGenGrid)((IGenControl)this).Parent)?.ValidateSearchField(BindingField);
            
        else if (((IGenControl)this).Parent is INonGenGrid grid)
            grid.ValidateField(BindingField);
        return base.OnClosedAsync();
    }
    

   

    private void AddComponents()
    {
        if (((IGenControl)this).IsSearchField)
            ((INonGenGrid)((IGenControl)this).Parent)?.AddSearchFieldComponent(this);
        else
            ((IGenControl)this).Parent?.AddChildComponent(this);

       
    }

    
    [Parameter]
    public EventCallback<ValueChangedArgs<DateTime?>> OnDateChanged { get; set; }

 
    
    private void SetCallBackEvents()
    {
        if (((IGenControl)this).IsSearchField)
        {
            DateChanged = EventCallback.Factory.Create<DateTime?>(this, x =>
            {
                SetValue(x.CastTo<DateTime?>());
                Validate();
            });
        }
        else
        {
            // if (!DateChanged.HasDelegate)
            DateChanged = EventCallback.Factory.Create<DateTime?>(this, SetValue);

        }
    }

    //public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    //{
    //    RenderAsComponent(model, ignoreLabels, new KeyValuePair<string, object>(nameof(Disabled), !(EditorEnabledFunc?.Invoke(Model) ?? EditorEnabled))).Invoke(builder);
    // };

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

        if (((IGenControl)this).IsSearchField)
        {
            Clearable = true;
        }

        // var valDate = (DateTime?)Model?.GetPropertyValue(BindingField);
        // Set a tabindex attribute dynamically
        // builder.AddAttribute(1, "tabindex", -1);
       
        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();
        additionalParams.Add(("tabindex", -1));
        //additionalParams.Add((nameof(_value), valDate?? _value));

        //additionalParams.Add((nameof(Date), valDate));

        additionalParams.Add((nameof(Disabled), DisabledIf?.Invoke(Model) ?? Disabled));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        additionalParams.Add((nameof(Color), ((IGenControl)this).Parent.TemplateColor));

        additionalParams.Add((nameof(Label), Label is null or "" ? " " : Label));

        if (!Required && (!RequiredIf?.Invoke(Model) ?? false))
            Error = false;

        builder.RenderComponent(this, ignoreLabels,  additionalParams.ToArray());
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
    // }

    void IGenControl.SetSearchValue(object value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = value;
        ((IGenControl)this).Parent.StateHasChanged();

    }

    object IGenControl.GetValue()
    {
        return Model.GetPropertyValue(BindingField);
    }

    void IGenControl.SetValue(object value)
    {
        SetValue((DateTime?)value);
    }

    public Task ClearAsync()
    {
        ClearAsync(true);
        return Task.CompletedTask;
    }

    void IGenControl.SetEmpty()
    {
        if (Model is null) return;

        var defaultValue = ((IGenControl)this).DataType.GetDefaultValue().CastTo<DateTime?>();

        Model.SetPropertyValue(BindingField, defaultValue);

        _value = defaultValue;
    }


    public override Task ClearAsync(bool close = true)
    {
        ((IGenControl)this).SetEmpty();
        return base.ClearAsync(close);
    }
    
    

    public new bool Validate()
    {
        if (((IGenControl)this).IsSearchField)
            return ((INonGenGrid)((IGenControl)this).Parent).ValidateSearchField(BindingField);

        if (((IGenControl)this).Parent is INonGenGrid grid)
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

    private void ReleaseUnmanagedResources()
    {
        // TODO release unmanaged resources here
    }

     
    public async ValueTask DisposeAsync()
    {
        // Dispose of any resources here
        await Task.CompletedTask;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose managed resources
            // Model = null;
            // // ((IGenComponent)this).Parent = null;
            // InitialValue = null;
            // // BindingField = null;
            // EditorVisibleIf = null;
            // DisabledIf = null;
            // RequiredIf = null;
            // OnDateChanged = default;
            // OnClick=default;
            // DateChanged=default;
            // OnDateChanged = default;
            
        }

        base.Dispose(disposing);
        GC.SuppressFinalize(this);
    }

   
    ~GenDatePicker()
    {
        Dispose(false);
    }
}