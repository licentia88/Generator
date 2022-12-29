using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;

namespace Generator.Components.Validators;

public class ObjectValidator <TComponent> where TComponent : IGenComponent
{

    public ICollection<ValidationResult> results  { get; }

    public ObjectValidator()
    {
        results = new List<ValidationResult>();
    }



    internal void Validate(TComponent component)
    {
        if (component.Model is null) return;


        if(component.Model is ExpandoObject or Dictionary<string, object>)
        {
            if (component.Required)
            {
                var resIsNull = component.Model.GetPropertyValue(component.BindingField).IsNullOrDefault();

                if (!resIsNull) return;

                component.Error = true;

                component.ErrorText = $"The {component.BindingField} field is required";

                component.ParentComponent.IsModelValid = false;

                return;
            }

            if (component.HasProperty(nameof(IGenTextField.MaxLength)))
            {
                var maxlength = component.GetPropertyValue(nameof(IGenTextField.MaxLength)).CastTo<int>();

                var compLength = component.Model.GetPropertyValue(component.BindingField)?.ToString()?.Length??0;

                var lengthresult = compLength < maxlength;

                if (lengthresult) return;

                component.Error = true;

                component.ErrorText = $"The {component.BindingField} can not be more then {maxlength} characters";

                component.ParentComponent.IsModelValid = false;

                return;
            }

            return;
        }

        var valCOntext = new ValidationContext(component.Model)
        {
            MemberName = component.BindingField,
            DisplayName = component.Model.GetType().GetProperty(component.BindingField)?.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(DisplayNameAttribute))
            .ConstructorArguments.FirstOrDefault().Value?.ToString() ?? string.Empty

        };

        var result = Validator.TryValidateProperty(component.Model.GetPropertyValue(component.BindingField), valCOntext, results);

        component.ParentComponent.IsModelValid = true;
    }

}