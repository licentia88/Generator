namespace Generator.Components.Interfaces;

public interface IGenTextField : IGenControl
{
    public int MaxLength { get; set; }

    public int MinLength { get; set; }

    public object InitialValue { get; set; }

}
