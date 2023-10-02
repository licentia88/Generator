using Generator.Components.Args;

namespace Generator.Components.Interfaces;

public interface IGenAutoComplete : IGenComponent
{
    public string DisplayField { get; set; }

    public string ValueField { get; set; }

 }

public interface IGenAutoComplete<T> : IGenAutoComplete
{
    public IEnumerable<T> DataSource { get; set; }

    //public Func<ComponentArgs<T>, bool> Where { get; set; }

}

