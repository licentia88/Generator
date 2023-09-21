namespace Generator.Components.Interfaces;

public interface IGenCheckBox : IGenComponent
{

    public string TrueText { get; set; }

    public string FalseText { get; set; }

    public void SetValue(bool value);

    public bool? InitialValue { get; set; }
    //public Func<object,bool> CheckedFunc { get; set; }

}
