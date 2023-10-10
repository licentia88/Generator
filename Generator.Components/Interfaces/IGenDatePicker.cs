namespace Generator.Components.Interfaces;

public interface IGenDatePicker : IGenComponent
{
    public void SetValue(DateTime? date);

    public DateTime? InitialValue { get; set; }
}
