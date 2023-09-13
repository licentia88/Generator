namespace Generator.Components.Interfaces;

public interface IGenAutoComplete : IGenComponent
{

    public IEnumerable<object> DataSource { get; set; }

    public string DisplayField { get; set; }

    public string ValueField { get; set; }
}
