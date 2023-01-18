using Generator.Components.Components;
using Generator.Components.Validators;

namespace Generator.Components.Interfaces;

public interface IGenTextField : IGenComponent
{
    public int MaxLength { get; set; }

    public Task OnValueChanged(object value);
}
