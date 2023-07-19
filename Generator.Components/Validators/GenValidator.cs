using System.ComponentModel.DataAnnotations;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
 
namespace Generator.Components.Validators;

public class GenValidator<T> 
{
    

    public bool ValidateModel(T obj)
    {
        return ValidateModel(obj, null);
    }


    public bool ValidateModel(T obj, IEnumerable<IGenComponent> components)
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

        foreach (var comp in components)
        {
            comp.Required = comp.RequiredIf?.Invoke(obj) ?? comp.Required;

            if (comp.Required)
            {
                var errorText = string.IsNullOrEmpty(comp.ErrorText) ? "*" : comp.ErrorText;
                SetError(comp, errorText);
                isValid = false;
            }
        }

        return isValid;
    }

    public bool ValidateValue(IGenComponent component)
    {
        bool isValid = true;

        if (component.Model.IsModel())
            isValid = ValidateModelValue(component);
 
        if (component.IsRequired(component.Model))
        {
            SetError(component);
            isValid = false;
        }
        return isValid;
    }


     
    private bool ValidateModelValue(IGenComponent component)
        {
        var modelType = component.Model.GetType();

        if (!modelType.HasProperty(component.BindingField)) return true;

        var results = new List<ValidationResult>();

        var context = new ValidationContext(component.Model);

        context.MemberName = component.BindingField;

        context.DisplayName = AttributeExtensions.GetDisplayName<T>(component.BindingField);

        var value = component.Model.GetPropertyValue(component.BindingField);

        bool isValid = Validator.TryValidateProperty(value, context, results);

        if (isValid)
        {
            ResetValidation(component);
            return true;
        }

        SetError(component, results.FirstOrDefault().ErrorMessage);

        return false;
    }

    private bool ValidateExpandObject(IGenComponent component)
    {
        var Model = component.Model;

        if (component.RequiredIf?.Invoke(Model) ?? component.Required)
        {
            var resIsNull = Model.GetPropertyValue(component.BindingField).IsNullOrDefault();

            if (resIsNull)
            {
                SetError(component, $"*");

                return false;
            }
        }

        if (component.HasProperty(nameof(IGenTextField.MaxLength)))
        {
            var maxlength = component.GetPropertyValue(nameof(IGenTextField.MaxLength)).CastTo<int>();

            var compLength = Model.GetPropertyValue(component.BindingField)?.ToString()?.Length ?? 0;

            var isBigger = compLength > maxlength;

            if (isBigger)
            {
                SetError(component, $"Max {maxlength} characters");

                return false;
            }
        }

        if (component.HasProperty(nameof(IGenTextField.MinLength)))
        {
            var minLength = component.GetPropertyValue(nameof(IGenTextField.MinLength)).CastTo<int>();

            var compLength = Model.GetPropertyValue(component.BindingField)?.ToString()?.Length ?? 0;

            var isSmaller = compLength < minLength;

            if (isSmaller)
            {
                SetError(component, $"Min {minLength} characters");

                return false;
            }
        }

        //ResetValidation(component);
        return true;
       
    }

    private void SetError(IGenComponent component)
    {
        if (component is null) return;
        component.Error = true;

        component.ErrorText = string.IsNullOrEmpty(component.ErrorText) ? "*" : component.ErrorText;
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

        //component.ErrorText = string.Empty;

    }
}


