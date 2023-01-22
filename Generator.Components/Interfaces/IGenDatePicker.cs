namespace Generator.Components.Interfaces;

public interface IGenDatePicker: IGenComponent
{
    public void OnDateChanged(DateTime? date);
}