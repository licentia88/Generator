using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Extensions;

namespace Generator.Components.Components;

public class GenTimePicker : MudTimePicker, IGenTimePicker, IComponentMethods<GenTimePicker>
{
    [CascadingParameter(Name = nameof(IGenComponent.Parent))]
    IPageBase IGenComponent.Parent { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }


    [Parameter]
    public TimeSpan? InitialValue { get; set; }

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    Type IGenControl.DataType { get; set; } = typeof(TimeSpan);

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


    [Parameter]
    public EventCallback<(object Model, TimeSpan? Value)> OnTimeChanged { get; set; }


    protected new async Task SetTimeAsync(TimeSpan? time, bool updateValue)
    {
         await base.SetTimeAsync(time, updateValue);
        await OnTimeChanged.InvokeAsync((Model, time));
    }

 
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (Model is not null)
            Time = (TimeSpan?)Model?.GetPropertyValue(BindingField);

        AddComponents();

        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

        if (Model is null || Model.GetType().Name == "Object") return;

        if (InitialValue is not null && ((INonGenGrid)((IGenControl)this).Parent).ViewState != ViewState.Update)
            await SetTimeAsync(InitialValue, true);

    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {

        if (((IGenControl)this).Parent is not null && Model is not null)
            base.BuildRenderTree(builder);

        //if (Model is not null && Model.GetType().Name != "Object")
        //    base.BuildRenderTree(builder);

        AddComponents();
    }

    public async void SetValue(TimeSpan? date)
    {

        if (this is not IGenControl comp) return;

        if (comp.IsSearchField)
            comp.SetSearchValue(date);
        else
        {
            Model?.SetPropertyValue(BindingField, date);
            //await SetTextAsync(base.Converter.Set(date), true);

        }
        await OnTimeChanged.InvokeAsync((Model, date));

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


   


    private void SetCallBackEvents()
    {
        if (((IGenControl)this).IsSearchField)
        {
            TimeChanged = EventCallback.Factory.Create<TimeSpan?>(this, x =>
            {
                SetValue(x.CastTo<TimeSpan?>());
                Validate();
            });
        }
        else
        {
            // if (!DateChanged.HasDelegate)
            TimeChanged = EventCallback.Factory.Create<TimeSpan?>(this, SetValue);

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

        var valDate = (TimeSpan?)Model?.GetPropertyValue(BindingField);

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        //additionalParams.Add((nameof(_value), valDate?? _value));

        //additionalParams.Add((nameof(Date), valDate));

        additionalParams.Add((nameof(Disabled), DisabledIf?.Invoke(Model) ?? Disabled));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        if (!Required && (!RequiredIf?.Invoke(Model) ?? false))
            Error = false;

        builder.RenderComponent(this, ignoreLabels, additionalParams.ToArray());
        //throw new NotImplementedException();
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var val = (TimeSpan?)model.GetPropertyValue(BindingField);

        if (val is not null)
            RenderExtensions.RenderGrid(builder, val.Value.ToString("hh':'mm':'ss"));

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
        SetValue((TimeSpan?)value);
    }

    void IGenControl.SetEmpty()
    {
        if (Model is null) return;

        var defaultValue = ((IGenControl)this).DataType.GetDefaultValue().CastTo<TimeSpan?>();

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
