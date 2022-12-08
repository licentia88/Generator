using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Generator.Components.Interfaces;

public interface IGenTextField : IGenComponent
{
    public void OnValueChanged(object value);
}
