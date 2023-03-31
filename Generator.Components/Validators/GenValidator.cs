using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using ProtoBuf.Meta;

namespace Generator.Components.Validators;

public class GenValidator<T> : AbstractValidator<T>
{
    private void GenericRuleFor(string propertyName)
    {
        var type = typeof(T);
        var property = type.GetProperty(propertyName);
        var param = Expression.Parameter(type);
        var propertyExpression = Expression.Property(param, property);
        var lambda = Expression.Lambda(typeof(Func<,>).MakeGenericType(type, property.PropertyType), propertyExpression, param);
        var thisType = GetType();
        var ruleForMethod = thisType.GetMethod("RuleFor", BindingFlags.Public | BindingFlags.Instance);
        var genericRuleForMethod = ruleForMethod.MakeGenericMethod(property.PropertyType);
        // result is used by extension method
        var result = genericRuleForMethod.Invoke(this, new object[] { lambda });
        //NotEmpty method is an Extension metot which is contained by DefaultValidatorExtensions
        var extensionsType = typeof(DefaultValidatorExtensions);
        var notEmptyMethod = extensionsType.GetMethod("NotEmpty", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(type, property.PropertyType);
        notEmptyMethod.Invoke(null, new object[] { result });
    }

    public bool ValidateModel(T obj)
    {
        return ValidateModel(obj, null);
    }

    public bool ValidateModel(T obj,IList<IGenComponent> components)
    {
        var results = new List<ValidationResult>();

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
        var results = new List<ValidationResult>();

        var context = new ValidationContext(model);

        context.MemberName = propertyName;

        context.DisplayName = AttributeExtensions.GetDisplayName<T>(propertyName);

        var value = model.GetPropertyValue(propertyName);

        if (value is null) return false;

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

    private async Task<bool> ValidateValue2(IGenComponent component, T model, string propertyName)
    {
        GenericRuleFor(propertyName);

        var valContext = ValidationContext<T>.CreateWithOptions(model, x => x.IncludeProperties(propertyName));

        var result = await ValidateAsync(valContext);

        if (result.IsValid)
        {
            ResetValidation(component);
            return true;
        }


        SetError(component, result.Errors.FirstOrDefault().ErrorMessage);

        return false;
    }

    private void SetError(IGenComponent component, string errorMessage)
    {
        component.Error = true;

        component.ErrorText = errorMessage;
    }

    public void ResetValidation(IGenComponent component)
    {
        component.Error = false;

        component.ErrorText = string.Empty;
    }
}


