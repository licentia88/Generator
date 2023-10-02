using Generator.Components.Args;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Generator.Components.Components;


public class GenAutoComplete<T> : MudAutocomplete<T>, IGenAutoComplete<T>
{
    [CascadingParameter(Name = nameof(IGenComponent.ParentGrid))]
    INonGenGrid IGenComponent.ParentGrid { get; set; }

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

    //[Parameter]
    //public object InitialValue { get; set; }
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

    private T CurrentData;

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
        //if (string.IsNullOrEmpty(value))
        //    return serviceResult.Take(MaxItems.Value).ToList();

          
        //return serviceResult.Where(x => Where?.Invoke((Model, x)) ?? true).Take(MaxItems.Value).ToList();

    }
    private async Task<IEnumerable<T>> FindMethod(string value)
    {
        //if (CurrentData is null && string.IsNullOrEmpty(Text))
        //{
        //     await ForceUpdate();
        //    Count = null;
        //}

        if (!string.IsNullOrEmpty(value) && value == Text)
            return Array.Empty<T>();

        if (ServiceMethod is null)
        {
            if (string.IsNullOrEmpty(value))
            {
                var dataToReturn = DataSource.Where(x => Where?.Invoke(new ComponentArgs<T>(Model,x)) ?? true).Take(MaxItems.Value).ToList();
                Count = dataToReturn.Count;
                return dataToReturn;
            }
            //return Task.FromResult<IEnumerable<T>>(DataSource.Where(x => Where?.Invoke((Model, x)) ?? true).Take(MaxItems.Value).ToList());

            var filteredData = DataSource.Where(x => Where?.Invoke(new ComponentArgs<T>(Model, x)) ?? true)
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
            ((IGenComponent)this).ParentGrid?.AddSearchFieldComponent(this);
        else
            ((IGenComponent)this).ParentGrid?.AddChildComponent(this);

    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Model is not null && Model.GetType().Name != "Object")
        {
            TextUpdateSuppression = true;

            base.BuildRenderTree(builder);
        }

        AddComponents();
    }


    public void OnClearClicked(MouseEventArgs arg)
    {
        ForceUpdate();
        Count = 0;
        TextUpdateSuppression = false;
       
        if (((IGenComponent)this).IsSearchField)
        {
            Validate();
            return;
        }

    }


    public void SetValue(object value)
    {
        //if (value is null) return;

        if (this is not IGenComponent comp) return;

        var loValue = value.GetPropertyValue(ValueField);

        if (comp.IsSearchField)
        {
            //comp.SetSearchValue(value);
            Model?.SetPropertyValue(BindingField, loValue);
            comp.ParentGrid.ResetConditionalSearchFields();

        }
        else
        {
            Model?.SetPropertyValue(BindingField, loValue);
        }


        //var textValue = value.GetPropertyValue(DisplayField)?.ToString();
        CurrentData = value.CastTo<T>();



        comp.ParentGrid.StateHasChanged();
        comp.ParentGrid.CurrentGenPage?.StateHasChanged();
    }

    protected override void OnConversionErrorOccurred(string error)
    {
        base.OnConversionErrorOccurred(error);
    }

    protected override async Task SetValueAsync(T value, bool updateText = true, bool force = false)
    {
        await base.SetValueAsync(value, updateText, force);
        await OnValueChanged.InvokeAsync(new ComponentArgs<T>(Model, value));
    }
   

    [Parameter]
    public EventCallback<ComponentArgs<T>> OnValueChanged { get; set; }

   


    protected virtual void SetCallBackEvents()
    {
        ToStringFunc = x => x?.GetPropertyValue(DisplayField)?.ToString();

        SearchFunc = FindMethod;
 
        if (!ValueChanged.HasDelegate)
            ValueChanged = EventCallback.Factory.Create(this, (T arg) => SetValue(arg));


        if (!OnClearButtonClick.HasDelegate)
            OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, OnClearClicked);


        OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () =>
        {

 
            if (Count == 0 || CurrentData is null)
            {
                TextUpdateSuppression = false;
                ForceUpdate();
                Count = null;
                //TextUpdateSuppression = true;
            }
              
        });

        //OnKeyDown = EventCallback.Factory.Create<KeyboardEventArgs>(this, () => TextUpdateSuppression = true);



    }

    


    public virtual RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => builder =>
    {
        if (Model is null || Model.GetType().Name == "Object")
            Model = model;
        
        SetCallBackEvents();

        ShowProgressIndicator = true;

        var innerFragment = (nameof(ProgressIndicatorTemplate), (RenderFragment)(treeBuilder =>
        {
            //< MudProgressLinear Size = "Size.Small" Indeterminate = "true" Color = "SelectedColor" />

            treeBuilder.OpenComponent(1000, typeof(MudProgressLinear));
            treeBuilder.AddAttribute(1001, nameof(Size), Size.Small);
            treeBuilder.AddAttribute(1002, nameof(MudProgressLinear.Indeterminate), true);
            treeBuilder.AddAttribute(1003, nameof(MudProgressLinear.Color), ProgressIndicatorColor);
 
            treeBuilder.CloseComponent();

        }));

        //var loValue = Model.GetPropertyValue(BindingField);

        //var selectedField = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == loValue.ToString());

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        //additionalParams.Add((nameof(Value), selectedField));

        additionalParams.Add((nameof(Disabled), !(EditorEnabledIf?.Invoke(Model) ?? EditorEnabled)));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        additionalParams.Add(innerFragment);
       
        SelectValueOnTab = true;
        Clearable = true;

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
        ((IGenComponent)this).ParentGrid.ValidateField(BindingField);
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
        ((IGenComponent)this).ParentGrid.StateHasChanged();
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

    public new async Task Clear()
    {
        await base.Clear();

        ((IGenComponent)this).SetEmpty();
    }

    public new bool Validate()
    {
        if (((IGenComponent)this).IsSearchField)
            return ((IGenComponent)this).ParentGrid.ValidateSearchField(BindingField);

        return ((IGenComponent)this).ParentGrid.ValidateField(BindingField);
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
