using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.EMMA;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
 
namespace Generator.Components.Validators;

public class GenValidator<T> 
{
    public bool ValidateDataSource(T model)
    {
        return ValidateDataSource(model, null);
    }

    public bool ValidateDataSource(T model, IEnumerable<IGenComponent> components)
    {
         
        foreach (var component in components)
        {
            ResetValidation(component);
        }

        bool isValid = true;

        //Dictionary ise sadece bunu kontrol edip don
        if (!model.IsModel())
        {
            ValidateByRequiredTags(components.Where(x=> x.IsRequired(model)), ref isValid);
            return isValid;
        }

        var results = new List<ValidationResult>();

        if (model is null) return default;

        var context = new ValidationContext(model);

        isValid = Validator.TryValidateObject(model, context, results, true);

 
        if(components is not null && !isValid)
        {
            foreach (var result in results)
            {
                var errorMessage = result.ErrorMessage;
                var property = result.MemberNames.FirstOrDefault();
                var component = components.FirstOrDefault(x => x.BindingField == property);

                SetError(component, errorMessage);
            }
        }

        ValidateByRequiredTags(components.Where(x => x.IsRequired(model)), ref isValid);

        return isValid;
    }

    private void ValidateByRequiredTags(IEnumerable<IGenComponent> components, ref bool isValid)
    {
        foreach (var comp in components)
        {
            comp.ValidateField();

            if (comp.Error)
                isValid = false;
        }
    }
  
    public bool ValidateComponentValue(IGenComponent component)
    {
        bool isValid = true;

        if (component.Model.IsModel())
            isValid = ValidateModelValue(component);

        return isValid;
    }


    private bool ValidateModelValue(IGenComponent component)
    {
        bool isValid;

        //Dictionary ise sadece bunu kontrol edip don
        if (!component.Model.IsModel())
        {
            component.ValidateField();

            isValid = !component.Error;

            return isValid;
        }

        var modelType = component.Model.GetType();

        if (!modelType.HasProperty(component.BindingField)) return true;

        var results = new List<ValidationResult>();

        var context = new ValidationContext(component.Model)
        {
            MemberName = component.BindingField
        };


        context.DisplayName = AttributeExtensions.GetDisplayName<T>(component.BindingField);

        var value = component.Model.GetPropertyValue(component.BindingField);

        isValid = Validator.TryValidateProperty(value, context, results);

        if (isValid)
            ResetValidation(component);
        else
            SetError(component, results.FirstOrDefault().ErrorMessage);

        component.ValidateField();
 
        if (component.Error)
        {
            var errorText = string.IsNullOrEmpty(component.ErrorText) ? "*" : component.ErrorText;
            isValid = !component.Error;
            SetError(component, errorText);
        }
          
 
        return isValid;
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

    //private bool ValidateExpandObject(IGenComponent component)
    //{
    //    var Model = component.Model;

    //    if (component.RequiredIf?.Invoke(Model) ?? component.Required)
    //    {
    //        var resIsNull = Model.GetPropertyValue(component.BindingField).IsNullOrDefault();

    //        if (resIsNull)
    //        {
    //            SetError(component, $"*");

    //            return false;
    //        }
    //    }

    //    if (component.HasProperty(nameof(IGenTextField.MaxLength)))
    //    {
    //        var maxlength = component.GetPropertyValue(nameof(IGenTextField.MaxLength)).CastTo<int>();

    //        var compLength = Model.GetPropertyValue(component.BindingField)?.ToString()?.Length ?? 0;

    //        var isBigger = compLength > maxlength;

    //        if (isBigger)
    //        {
    //            SetError(component, $"Max {maxlength} characters");

    //            return false;
    //        }
    //    }

    //    if (component.HasProperty(nameof(IGenTextField.MinLength)))
    //    {
    //        var minLength = component.GetPropertyValue(nameof(IGenTextField.MinLength)).CastTo<int>();

    //        var compLength = Model.GetPropertyValue(component.BindingField)?.ToString()?.Length ?? 0;

    //        var isSmaller = compLength < minLength;

    //        if (isSmaller)
    //        {
    //            SetError(component, $"Min {minLength} characters");

    //            return false;
    //        }
    //    }

    //    //ResetValidation(component);
    //    return true;

    //}

    //private void SetError(IGenComponent component)
    //{
    //    if (component is null) return;
    //    component.Error = true;

    //    component.ErrorText = string.IsNullOrEmpty(component.ErrorText) ? "*" : component.ErrorText;
    //}
}


