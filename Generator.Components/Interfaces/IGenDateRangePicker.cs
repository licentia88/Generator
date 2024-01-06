using MudBlazor;

namespace Generator.Components.Interfaces;

public interface IGenDateRangePicker : IGenControl
{
    public void SetValue(DateRange range);

    public DateRange InitialValue { get; set; }
}