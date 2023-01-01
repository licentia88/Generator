using Generator.Components.Components;
using Generator.Components.Validators;

namespace Generator.Components.Interfaces;

public interface IGenComponent: IGenCompRenderer
{
    public string BindingField { get; set; }

    public Type DataType { get; set; }

    public int Order { get; set; }

    public bool VisibleOnEdit { get; set; } 

    public bool VisibleOnGrid { get; set; } 

    public bool EnabledOnEdit { get; set; } 

    public int xs { get; set; }

    public int sm { get; set; }

    public int md { get; set; }

    public int lg { get; set; }

    public int xl { get; set; }

    public int xxl { get; set; }

    public GenGrid ParentComponent { get; set; }

    public object Model { get; set; }

    public object GetDefaultValue { get; }

    public string Label { get; set; }

    public bool Required { get; set; }

    public bool Error { get; set; }

    public string ErrorText { get; set; }

    public void ValidateObject();

}

