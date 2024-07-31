using DocumentFormat.OpenXml.EMMA;
using Generator.Components.Args;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Enums;

namespace Generator.Components.Components;


public class GenAutoComplete : MudAutocomplete<object>, IGenAutoComplete<object>
{
    [CascadingParameter(Name = nameof(IGenControl.Parent))]
    IPageBase IGenComponent.Parent { get; set; }

    [Parameter, EditorRequired]
    public string DisplayField { get; set; }

    [Parameter, EditorRequired]
    public string ValueField { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

    [Parameter]
    public Func<string, Task<IEnumerable<object>>> ServiceMethod { get; set; }

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    Type IGenControl.DataType { get; set; } = typeof(object);

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

    [Parameter, EditorRequired]
    public IEnumerable<object> DataSource { get; set; }

    [Parameter]
    public object InitialValue { get; set; }


    [Parameter]
    public Func<ComponentArgs<object>, bool> Where { get; set; }

    [CascadingParameter(Name = nameof(IGenControl.IsSearchField))]
    bool IGenControl.IsSearchField { get; set; }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> DisabledIf { get; set; }


    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }

    //#nullable enable
    private object CurrentData;
    //#nullable disable
    //protected override void OnInitialized()
    //{
    //    base.OnInitialized();

    //    AddComponents();

    //    ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

    //    //if (Model is null || Model.GetType().Name == "Object") return;

    //    //if (InitialValue is not null)
    //    //    SetValue(InitialValue);

    //}
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        AddComponents();

        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

        //if (Model is null || Model.GetType().Name == "Object") return;

        //if (InitialValue is not null)
        //    SetValue(InitialValue);
        if (InitialValue is not null && ((INonGenGrid)((IGenControl)this).Parent).ViewState != ViewState.Update)
            await SetInitialValue();

    }

    private async Task SetInitialValue()
    {
        if (Model is not null)
        {
            var currentPropertyValue = Model.GetPropertyValue(BindingField);

            if (currentPropertyValue is null) return;

            if (DataSource is null) return;

            CurrentData = DataSource.FirstOrDefault(x => x.GetType().GetProperty(ValueField).GetValue(x).ToString() == currentPropertyValue.ToString());

            var tosetValue = CurrentData.GetPropertyValue(DisplayField).ToString();

            await SetTextAsync(tosetValue);
            await SetValueAsync(CurrentData);


        }
    }



    /// <summary>
    /// Toggle the menu (if not disabled or not readonly, and is opened).
    /// </summary>
    public new async Task ToggleMenu()
    {

        await base.ToggleMenu();
    }

    protected int? Count = null;

    private async Task<IEnumerable<object>> ExecuteService(string value)
    {
        var serviceResult = await ServiceMethod.Invoke(value);

        if (serviceResult is null)
            return Array.Empty<object>();

        return serviceResult.Take(MaxItems.Value).ToList();
    }

    public override async Task ForceUpdate()
    {
        await SetValueAsync(Value, updateText: true, force: true);
        //return base.ForceUpdate();
    }
    private async Task<IEnumerable<object>> FindMethod(string value)
    {

        if (!string.IsNullOrEmpty(value) && value == Text)
        {
            //Count = null;
            return Array.Empty<object>();
        }

        if (ServiceMethod is null)
        {
            if (string.IsNullOrEmpty(value))
            {
                var dataToReturn = DataSource.Where(x => Where?.Invoke(new ComponentArgs<object>(Model, x, ((IGenControl)this).IsSearchField)) ?? true).Take(MaxItems.Value).ToList();
                Count = dataToReturn.Count;
                return dataToReturn;
            }


            var filteredData = DataSource
                               .Where(x => Where?.Invoke(new ComponentArgs<object>(Model, x, ((IGenControl)this).IsSearchField)) ?? true)
                               .Where(x => x.GetType().GetProperty(DisplayField).GetValue(x).ToString().StartsWith(value, StringComparison.InvariantCultureIgnoreCase))
                               .ToList();

            Count = filteredData.Count;

            return filteredData;
            //return Task.FromResult<IEnumerable<T>>(filteredData);
        }

        var serviceData = await ExecuteService(value);

        Count = serviceData.Count();

        return serviceData;
    }

    protected void AddComponents()
    {
        if (((IGenControl)this).IsSearchField)
            ((INonGenGrid)((IGenControl)this).Parent)?.AddSearchFieldComponent(this);
        else
            ((IGenControl)this).Parent?.AddChildComponent(this);

    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (((IGenControl)this).Parent is not null && Model is not null)
            base.BuildRenderTree(builder);

        //if (Model is not null && Model.GetType().Name != "Object")
        //{
        //    //TextUpdateSuppression = true;

        //    base.BuildRenderTree(builder);
        //}

        AddComponents();
    }


    public async void OnClearClicked(MouseEventArgs arg)
    {
        SetValue(null);
        Text = string.Empty;
        await ForceUpdate();

        //await ForceUpdate();
        //Count = 0;
        //TextUpdateSuppression = false;


        //if (((IGenComponent)this).IsSearchField)
        //{
        //    Validate();
        //    return;
        //}

    }


    public void SetValue(object value)
    {
        //if (value is null) return;

        if (this is not IGenControl comp) return;

        var loValue = value.GetPropertyValue(ValueField);

        if (comp.IsSearchField)
        {
            //comp.SetSearchValue(value);
            Model?.SetPropertyValue(BindingField, loValue);
            ((INonGenGrid)comp.Parent).ResetConditionalSearchFields();

        }
        else
        {
            Model?.SetPropertyValue(BindingField, loValue);
        }


        //var textValue = value.GetPropertyValue(DisplayField)?.ToString();
        CurrentData = value;

        //CanFetch = true;

        comp.Parent.StateHasChanged();
        if (comp.Parent is INonGenGrid grid)
            grid.CurrentGenPage?.StateHasChanged();

        //comp.Parent.StateHasChanged();

        //comp.Parent.CurrentGenPage?.StateHasChanged();
    }

    protected override void OnConversionErrorOccurred(string error)
    {
        base.OnConversionErrorOccurred(error);
    }

    protected override async Task SetValueAsync(object value, bool updateText = true, bool force = false)
    {
        await base.SetValueAsync(value, updateText, force);
        await OnValueChanged.InvokeAsync(new ComponentArgs<object>(Model, value, ((IGenControl)this).IsSearchField));
        Validate();
    }


    [Parameter]
    public EventCallback<ComponentArgs<object>> OnValueChanged { get; set; }




    protected virtual void SetCallBackEvents()
    {
        ToStringFunc = x => x?.GetPropertyValue(DisplayField)?.ToString();

        SearchFunc = FindMethod;

        //if (!ValueChanged.HasDelegate)
        ValueChanged = EventCallback.Factory.Create<object>(this, SetValue);


        if (!OnClearButtonClick.HasDelegate)
            OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, OnClearClicked);




        //OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, async () => {

        //    //eger acik degilse ama count varsa mevcut datayi set et

        //    if (Count != 0 || CurrentData is null)
        //    {
        //        var selectedField = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == Model.GetPropertyValue(BindingField)?.ToString());
        //        if (selectedField is not null)
        //            await SetValueAsync(selectedField);
        //    }



        //});

        //OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, async () =>
        //{
        //    if (Count == 0 || CurrentData is null)
        //    {
        //        //TextUpdateSuppression = false;
        //        await ForceUpdate();
        //        Count = null;
        //        //TextUpdateSuppression = true;
        //    }

        //});

    }

    protected override Task OnBlurredAsync(FocusEventArgs obj)
    {
        return base.OnBlurredAsync(obj);
    }

 
    public bool IsExecuting { get; set; }

    public virtual RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) =>   async builder =>
    {
        //if (Model?.GetType().Name == "Object" || !((IGenComponent)this).IsSearchField)
        //    Model = model;

        if (!((IGenControl)this).IsSearchField)
            Model = model;

        if (((IGenControl)this).IsSearchField && Model is null)
        {
            Model = model;
        }

        //if (Model.GetType().Name == "Object")
        //    return;



        SetCallBackEvents();

        ShowProgressIndicator = true;

        CoerceText = false;
        CoerceValue = false;

        var innerFragment = (nameof(ProgressIndicatorTemplate), (RenderFragment)(treeBuilder =>
        {
            treeBuilder.OpenComponent(1000, typeof(MudProgressLinear));
            treeBuilder.AddAttribute(1001, nameof(Size), Size.Small);
            treeBuilder.AddAttribute(1002, nameof(MudProgressLinear.Indeterminate), true);
            treeBuilder.AddAttribute(1003, nameof(MudProgressLinear.Color), ProgressIndicatorColor);
            treeBuilder.CloseComponent();
        }));





        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        var currentModelValue = Model.GetPropertyValue(BindingField)?.ToString();


        if (DataSource is not null)
        {
            var loValue = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == currentModelValue);
            additionalParams.Add((nameof(Value), loValue ?? Value));

        }
        

        //if(OperatingSystem.IsWindows())
        //{
        //    if (DataSource is not null)
        //    {
        //        var loValue = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == currentModelValue);
        //        additionalParams.Add((nameof(Value), loValue ?? Value));

        //    }
        //    else if (ServiceMethod is not null && !string.IsNullOrEmpty(currentModelValue))
        //    {

        //        var serviceResult = await ServiceMethod(currentModelValue);

        //        try
        //        {
        //            DataSource = serviceResult;

        //            var loValue = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == currentModelValue);
        //            additionalParams.Add((nameof(Value), loValue ?? Value));
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //}




        additionalParams.Add((nameof(Disabled), (DisabledIf?.Invoke(Model) ?? Disabled)));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        if (ProgressIndicatorColor is not Color.Default)
            additionalParams.Add((nameof(ProgressIndicatorColor), ProgressIndicatorColor));
        else
            additionalParams.Add((nameof(ProgressIndicatorColor), ((IGenControl)this).Parent.TemplateColor));

        additionalParams.Add(innerFragment);
        additionalParams.Add((nameof(Color), ((IGenControl)this).Parent.TemplateColor));

        //SelectValueOnTab = true;
        //Clearable = true;

        //additionalParams.Add((nameof(ToStringFunc), (object x) => x.GetPropertyValue(DisplayField)?.ToString()));
        //TODO burada render yapmadan value yu kontrol et
        if (!Required && (!RequiredIf?.Invoke(Model) ?? false))
            Error = false;

        builder.RenderComponent(this, ignoreLabels, additionalParams.ToArray());
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var selectedField = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == model.GetPropertyValue(BindingField)?.ToString());

        RenderExtensions.RenderGrid(builder, selectedField.GetPropertyValue(DisplayField));
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



    void IGenControl.SetSearchValue(object value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = value.GetPropertyValue(BindingField);
        ((IGenControl)this).Parent.StateHasChanged();
    }

    object IGenControl.GetSearchValue()
    {
        return Model.GetPropertyValue(BindingField);

    }

    void IGenControl.SetEmpty()
    {
        var defaultValue = ((IGenControl)this).DataType.GetDefaultValue();

        Model?.SetPropertyValue(BindingField, defaultValue);

        //Value = null;

        //_value = Model;
    }

    //protected new virtual async Task SetTextAsync(string text, bool updateValue = true)
    //{
    //    if (Text != text)
    //    {
    //        Text = text;
    //        if (!string.IsNullOrWhiteSpace(Text))
    //        {
    //            base.Touched = true;
    //        }
    //        if (updateValue)
    //        {
    //            await UpdateValuePropertyAsync(updateText: false);
    //        }
    //        await TextChanged.InvokeAsync(Text);
    //    }
    //}
    //public new Task SetTextAsync(string text, bool updateValue = true)
    //{
    //    return base.SetTextAsync(text, updateValue);
    //}


    protected override async Task SetTextAsync(string text, bool updateValue = true)
    {
        if (Text != text)
        {
            Text = text;
            if (!string.IsNullOrWhiteSpace(Text))
            {
                base.Touched = true;
            }
            if (updateValue)
            {
                await UpdateValuePropertyAsync(updateText: false);
            }
            await TextChanged.InvokeAsync(Text);
        }
    }

    public new async Task Clear()
    {
        await OnClearButtonClick.InvokeAsync();
        //await base.Clear();



        //((IGenComponent)this).SetEmpty();


        ////await UpdateValuePropertyAsync(updateText: false);

        ////await TextChanged.InvokeAsync(string.Empty);
        //Text = string.Empty;
        //await UpdateValuePropertyAsync(updateText: false);
        ////await TextChanged.InvokeAsync(Text);

        ////await SetTextAsync(string.Empty);
        ////isCleared = false;
        //Count = 0;
        //CurrentData = default;

        //StateHasChanged();
        //ForceRender(true);
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
}