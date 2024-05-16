namespace Generator.Components.Interfaces;

public interface IGenTimePicker : IGenControl
{
    public void SetValue(TimeSpan? date);

    public TimeSpan? InitialValue { get; set; }
}
