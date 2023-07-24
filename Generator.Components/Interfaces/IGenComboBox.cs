namespace Generator.Components.Interfaces;

public interface IGenComboBox: IGenComponent
{
    public IEnumerable<object> DataSource { get; set; }

    public string DisplayField { get; set; }

    public string ValueField { get; set; }

    public Func<object, bool> Where { get; set; }

}
