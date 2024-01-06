namespace Generator.Components.Interfaces;

public interface IGenDatePicker : IGenControl
{
    public void SetValue(DateTime? date);

    public DateTime? InitialValue { get; set; }
}
