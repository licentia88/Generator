using Generator.Components.Args;
using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;

public interface IGenComboBox: IGenComponent
{
    public IEnumerable<object> DataSource { get; set; }

    public string DisplayField { get; set; }

    public string ValueField { get; set; }

    public Func<ComponentArgs<object>, bool> Where { get; set; }

    public object InitialValue { get; set; }
}
