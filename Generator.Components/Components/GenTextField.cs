using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Generator.Components.Components;

public class GenTextField : MudTextField<object>, IGenTextField, IComponentMethods<GenTextField> 
{ 

    [CascadingParameter(Name = nameof(ParentGrid))]
    public INonGenGrid ParentGrid { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

 
    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    [Parameter]
    public int MinLength { get; set; }

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


    public Type DataType { get; set; }

    public object GetDefaultValue => DataType.GetDefaultValue();

    [CascadingParameter( Name = nameof(IsSearchField))]
    public bool IsSearchField { get; set; }

   
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Model is not null && Model.GetType().Name != "Object")
            base.BuildRenderTree(builder);

    }

    protected override  Task OnInitializedAsync()
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
        if (ParentGrid.EditMode != Enums.EditMode.Inline && ParentGrid.CurrentGenPage is null) return;

        if (InputType == InputType.Date)
        {
            Converter = _dateConverter;
            Culture = CultureInfo.GetCultureInfo("en-US");
            Format = "yyyy-MM-dd";
            DataType = typeof(DateTime);
        }
        else
        {
            Converter = _stringConverter;
            DataType = typeof(string);
        }

        Immediate = true;

    }
    private Converter<object> _stringConverter = new()
    {
        SetFunc = value => value?.ToString(),
        GetFunc = text => text?.ToString()
    };

    private Converter<object> _dateConverter = new()
    {
        SetFunc = value => Convert.ToDateTime(value).ToString("yyyy-MM-dd"),
        GetFunc = text =>
        {
            DateTime.TryParse(text, out var dataToReturn);
            return dataToReturn;
        }
    };

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new string Text => Model.GetPropertyValue(BindingField).ToString();

   

    public void SetValue(object value)
    {
        Model?.SetPropertyValue(BindingField, value);
     }

    public void OnClearClicked(MouseEventArgs arg)
    {
        Model?.SetPropertyValue(BindingField, null);
    }

    private void SetCallBackEvents()
    {
        if (!ValueChanged.HasDelegate)
            ValueChanged = EventCallback.Factory.Create<object>(this, SetValue);

        if (IsSearchField)
        {
            OnClearButtonClick = EventCallback.Factory.Create(this, (MouseEventArgs arg) =>
            {
                SetValue(null);
                ParentGrid.ValidateSearchFields(BindingField);
            });

           

            OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () => ParentGrid.ValidateSearchFields(BindingField));
        }
        else
        {
            
            OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () => { ParentGrid.ValidateValue(BindingField); });

            OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, OnClearClicked);
        }
    }

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) =>  builder =>
    {
        if(Model is null || Model.GetType().Name == "Object")
            Model = model;

        SetCallBackEvents();

        var loValue = Model.GetPropertyValue(BindingField);

        builder.RenderComponent(this,ignoreLabels, (nameof(Value), loValue), (nameof(Disabled), !EditorEnabled));
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var data = model.GetPropertyValue(BindingField);

        if (data is DateTime dt)
        {
            data = dt.ToString(Format);
        }

        RenderExtensions.RenderGrid(builder, data);
    };

    public void ValidateObject()
    {
          ParentGrid.ValidateValue( BindingField);


    }

    public object GetValue()
    {
        return this.GetFieldValue(nameof(_value));
    }

   
}






