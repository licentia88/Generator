namespace Generator.Components.Interfaces;

public interface IGenTextField : IGenComponent
{
    public int MaxLength { get; set; }

    public void OnValueChanged(object value);
}
