namespace Generator.Components.Interfaces;

public interface IGenTextField : IGenComponent
{
    public int MaxLength { get; set; }

    public int MinLength { get; set; }

    public void SetValue(object value);

 }
