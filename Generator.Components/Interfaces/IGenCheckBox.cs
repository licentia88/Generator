using Generator.Components.Components;
using Generator.Components.Validators;

namespace Generator.Components.Interfaces;

public interface IGenCheckBox : IGenComponent
{

    public string TrueText { get; set; }

    public string FalseText { get; set; }

    //public void OnCheckChanged(bool value);

 

}
