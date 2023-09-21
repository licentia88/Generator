namespace Generator.Components.Interfaces;

public interface IGenComboBox: IGenComponent
{
    public IEnumerable<object> DataSource { get; set; }

    public string DisplayField { get; set; }

    public string ValueField { get; set; }

    public Func<(object Model, object Value), bool> Where { get; set; }

    public object InitialValue { get; set; }
}
