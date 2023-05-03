using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using ProtoBuf.Meta;

namespace Generator.Components.Validators;

public class GenValidator<T> 
{
    

    public bool ValidateModel(T obj)
    {
        return ValidateModel(obj, null);
    }

    public bool ValidateModel(T obj,IList<IGenComponent> components)
    {
        var results = new List<ValidationResult>();

        if (obj is null) return default;
        var context = new ValidationContext(obj);

        bool isValid = Validator.TryValidateObject(obj, context, results, true);

        if(components is not null)
        {
            foreach (var result in results)
            {
                var errorMessage = result.ErrorMessage;
                var property = result.MemberNames.FirstOrDefault();
                var component = components.FirstOrDefault(x => x.BindingField == property);

                SetError(component, errorMessage);
            }
        }
        return isValid;
    }

    public bool ValidateValue(IGenComponent component, T model, string propertyName)
    {
        if (model.IsModel())
            return ValidateModelValue(component, model, propertyName);

        return ValidateExpandObject(component, model, propertyName);
    }


     
    private bool ValidateModelValue(IGenComponent component, T model, string propertyName)
    {
        var modelHasProperty = typeof(T).GetProperties().Any(x => x.Name.Equals(propertyName));

        if (!typeof(T).HasProperty(propertyName)) return true;

        var results = new List<ValidationResult>();

        var context = new ValidationContext(model);

        context.MemberName = propertyName;

        context.DisplayName = AttributeExtensions.GetDisplayName<T>(propertyName);

        var value = model.GetPropertyValue(propertyName);

        //if (value is null) return false;

        bool isValid = Validator.TryValidateProperty(value, context, results);

        if (isValid)
        {
            ResetValidation(component);
            return true;
        }

        SetError(component, results.FirstOrDefault().ErrorMessage);

        return false;
    }

    private bool ValidateExpandObject(IGenComponent component, T model, string propertyName)
    {

        if (component.Required)
        {
            var resIsNull = model.GetPropertyValue(component.BindingField).IsNullOrDefault();

            if (resIsNull)
            {
                SetError(component, $"Required");

                return false;
            }
        }

        if (component.HasProperty(nameof(IGenTextField.MaxLength)))
        {
            var maxlength = component.GetPropertyValue(nameof(IGenTextField.MaxLength)).CastTo<int>();

            var compLength = model.GetPropertyValue(component.BindingField)?.ToString()?.Length ?? 0;

            var lengthresult = compLength > maxlength;

            if (!lengthresult)
            {
                SetError(component, $"Max {maxlength} characters");

                return false;
            }
        }

        if (component.HasProperty(nameof(IGenTextField.MinLength)))
        {
            var minLength = component.GetPropertyValue(nameof(IGenTextField.MinLength)).CastTo<int>();

            var compLength = model.GetPropertyValue(component.BindingField)?.ToString()?.Length ?? 0;

            var lengthresult = compLength < minLength;

            if (!lengthresult)
            {
                SetError(component, $"Min {minLength} characters");

                return false;
            }
        }

        return true;
       
    }

    

    private void SetError(IGenComponent component, string errorMessage)
    {
        if (component is null) return;
        component.Error = true;

        component.ErrorText = errorMessage;
    }

    public void ResetValidation(IGenComponent component)
    {
        if (component is null) return;

        component.Error = false;

        component.ErrorText = string.Empty;
    }
}


