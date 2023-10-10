using MudBlazor;

namespace Generator.Components.Interfaces;

public interface IGenDateRangePicker : IGenComponent
{
    public void SetValue(DateRange range);

    public DateRange InitialValue { get; set; }
}