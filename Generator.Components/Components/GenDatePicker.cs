using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Enums;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Components;

public class GenDatePicker : MudDatePicker, IGenDatePicker, IComponentMethods<GenDatePicker>
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

    public new DateTime? Date
    {
        get
        {
            return _value;
        }
        set
        {
            SetDateAsync(value, updateValue: true).AndForget();
        }
    }

 
    protected new  async Task SetDateAsync(DateTime? date, bool updateValue)
    {
        if (!(_value != date))
        {
            return;
        }
        base.Touched = true;
        if (date.HasValue && base.IsDateDisabledFunc(date.Value.Date))
        {
            await SetTextAsync(null, callback: false);
            return;
        }
        _value = date;
        if (updateValue)
        {
            base.Converter.GetError = false;
            await SetTextAsync(base.Converter.Set(_value), callback: false);
        }
        await DateChanged.InvokeAsync(_value);
        await BeginValidateAsync();
        FieldChanged(_value);

         await OnDateChanged.InvokeAsync((Model, date));

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

    public DateTime? _originalDate;

    public async  void SetValue(DateTime? date)
    {
        
        if (this is not IGenControl comp) return;

        if (comp.IsSearchField)
            comp.SetSearchValue(date);
        else
        {
            Model?.SetPropertyValue(BindingField, date);
            //await SetTextAsync(base.Converter.Set(date), true);

        }
        await OnDateChanged.InvokeAsync((Model, date));

        comp.Parent.StateHasChanged();
        if (comp.Parent is INonGenGrid grid)
            grid.CurrentGenPage?.StateHasChanged();
        //comp.Parent.StateHasChanged();
        //comp.Parent.CurrentGenPage?.StateHasChanged();
    }



    protected override void OnClosed()
    {
        if (!Error)
        {
            if (((IGenControl)this).IsSearchField)
                ((INonGenGrid)((IGenControl)this).Parent)?.ValidateSearchField(BindingField);
            
            else if (((IGenControl)this).Parent is INonGenGrid grid)
                grid.ValidateField(BindingField);
         
        }
            

        base.OnClosed();
    }

    private void AddComponents()
    {
        if (((IGenControl)this).IsSearchField)
            ((INonGenGrid)((IGenControl)this).Parent)?.AddSearchFieldComponent(this);
        else
            ((IGenControl)this).Parent?.AddChildComponent(this);

       
    }

    
    [Parameter]
    public EventCallback<(object Model, DateTime? Value)> OnDateChanged { get; set; }

 
    
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

        var valDate = (DateTime?)Model?.GetPropertyValue(BindingField);

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        //additionalParams.Add((nameof(_value), valDate?? _value));

        //additionalParams.Add((nameof(Date), valDate));

        additionalParams.Add((nameof(Disabled), DisabledIf?.Invoke(Model) ?? Disabled));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

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

    public object GetValue()
    {
        if (((IGenControl)this).IsSearchField)
            return ((IGenControl)this).GetSearchValue();
        else
            return Model.GetFieldValue(BindingField);
    }

    void IGenControl.SetSearchValue(object Value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = Value;
        ((IGenControl)this).Parent.StateHasChanged();

    }

    object IGenControl.GetSearchValue()
    {
        return Model.GetPropertyValue(BindingField);
    }

    void IGenControl.SetValue(object value)
    {
        SetValue((DateTime?)value);
    }

    void IGenControl.SetEmpty()
    {
        if (Model is null) return;

        var defaultValue = ((IGenControl)this).DataType.GetDefaultValue().CastTo<DateTime?>();

        Model.SetPropertyValue(BindingField, defaultValue);

        _value = defaultValue;
    }

    public Task Clear()
    {
        base.Clear();

        ((IGenControl)this).SetEmpty();
        
        return Task.CompletedTask;
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
}