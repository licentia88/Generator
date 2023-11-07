﻿using DocumentFormat.OpenXml.EMMA;
using Generator.Components.Args;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Generator.Components.Components;


public class GenAutoComplete<T> : MudAutocomplete<T>, IGenAutoComplete<T>
{
    [CascadingParameter(Name = nameof(IGenComponent.Parent))]
    IPageBase IGenComponent.Parent { get; set; }

    [Parameter, EditorRequired]
    public string DisplayField { get; set; }

    [Parameter, EditorRequired]
    public string ValueField { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

    [Parameter]
    public Func<string,Task<IEnumerable<T>>> ServiceMethod { get; set; }

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    Type IGenComponent.DataType { get; set; } = typeof(object);

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

    [Parameter, EditorRequired]
    public IEnumerable<T> DataSource { get; set; }

    [Parameter]
    public object InitialValue { get; set; }


    [Parameter]
    public Func<ComponentArgs<T>, bool> Where { get; set; }

    [CascadingParameter(Name = nameof(IGenComponent.IsSearchField))]
    bool IGenComponent.IsSearchField { get; set; }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> EditorEnabledIf { get; set; }

    
    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }

//#nullable enable
    private T CurrentData;
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

    private async Task<IEnumerable<T>> ExecuteService(string value)
    {
         var serviceResult = await ServiceMethod.Invoke(value);

        if (serviceResult is null)
            return Array.Empty<T>();

        return serviceResult.Take(MaxItems.Value).ToList();
    }

    public override async Task ForceUpdate()
    {
        await SetValueAsync(Value, updateText: true, force: true);
        //return base.ForceUpdate();
    }
    private async Task<IEnumerable<T>> FindMethod(string value)
    {

        if (!string.IsNullOrEmpty(value) && value == Text)
        {
            //Count = null;
            return Array.Empty<T>();
        }

        if (ServiceMethod is null)
        {
            if (string.IsNullOrEmpty(value) )
            {
                var dataToReturn = DataSource.Where(x => Where?.Invoke(new ComponentArgs<T>(Model,x, ((IGenComponent)this).IsSearchField)) ?? true).Take(MaxItems.Value).ToList();
                Count = dataToReturn.Count;
                return dataToReturn;
            }


            var filteredData = DataSource
                               .Where(x => Where?.Invoke(new ComponentArgs<T>(Model, x, ((IGenComponent)this).IsSearchField)) ?? true)
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
        if (((IGenComponent)this).IsSearchField)
            ((INonGenGrid)((IGenComponent)this).Parent)?.AddSearchFieldComponent(this);
        else
            ((IGenComponent)this).Parent?.AddChildComponent(this);

    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (((IGenComponent)this).Parent is not null && Model is not null)
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


    public  void SetValue(object value)
    {
        //if (value is null) return;

        if (this is not IGenComponent comp) return;

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
        CurrentData = value.CastTo<T>();

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

    protected override async Task SetValueAsync(T value, bool updateText = true, bool force = false)
    {
        await base.SetValueAsync(value, updateText, force);
        await OnValueChanged.InvokeAsync(new ComponentArgs<T>(Model, value, ((IGenComponent)this).IsSearchField));
        Validate();
    }
   

    [Parameter]
    public EventCallback<ComponentArgs<T>> OnValueChanged { get; set; }

   


    protected virtual  void SetCallBackEvents()
    {
        ToStringFunc = x => x?.GetPropertyValue(DisplayField)?.ToString();

        SearchFunc = FindMethod;
 
        if (!ValueChanged.HasDelegate)
            ValueChanged = EventCallback.Factory.Create(this, (T arg) => SetValue(arg));


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


    public virtual RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => builder =>
    {
        //if (Model?.GetType().Name == "Object" || !((IGenComponent)this).IsSearchField)
        //    Model = model;

        if (!((IGenComponent)this).IsSearchField)
            Model = model;

        if (((IGenComponent)this).IsSearchField && Model is null)
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

        additionalParams.Add((nameof(Disabled), !(EditorEnabledIf?.Invoke(Model) ?? EditorEnabled)));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        additionalParams.Add(innerFragment);
       
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



    void IGenComponent.SetSearchValue(object value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = value.GetPropertyValue(BindingField);
        ((IGenComponent)this).Parent.StateHasChanged();
    }

    object IGenComponent.GetSearchValue()
    {
        return Model.GetPropertyValue(BindingField);
        
    }

    void IGenComponent.SetEmpty()
    {
        var defaultValue = ((IGenComponent)this).DataType.GetDefaultValue();

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
