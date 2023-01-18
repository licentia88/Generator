namespace Generator.Components.Interfaces;

public interface IGenDatePicker: IGenComponent
{
    public Task OnDateChanged(DateTime? date);
}