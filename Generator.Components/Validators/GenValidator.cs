using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation;
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
        var thisType = this.GetType();
        var ruleForMethod = thisType.GetMethod("RuleFor", BindingFlags.Public | BindingFlags.Instance);
        var genericRuleForMethod = ruleForMethod.MakeGenericMethod(property.PropertyType);
        // result is used by extension method
        var result = genericRuleForMethod.Invoke(this, new object[] { lambda });
        //NotEmpty method is an Extension metot which is contained by DefaultValidatorExtensions
        var extensionsType = typeof(DefaultValidatorExtensions);
        var notEmptyMethod = extensionsType.GetMethod("NotEmpty", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(type, property.PropertyType);
        notEmptyMethod.Invoke(null, new object[] { result });
    }

    public async Task<bool> ValidateModel(T obj)
    {
        var results = new List<ValidationResult>();

        var context = new ValidationContext(obj);

        bool isValid = await Task.Run(() => Validator.TryValidateObject(obj, context, results, true));

        if (isValid)
            return true; 

         
        return false;
    }
 
    public async Task<bool> ValidateValue(IGenComponent component, T model, string propertyName)
    {
        var results = new List<ValidationResult>();

        var context = new ValidationContext(model);

        context.MemberName = propertyName;

        context.DisplayName = AttributeExtensions.GetDisplayName<T>(propertyName);

        var value = model.GetPropertyValue(propertyName);

        bool isValid = await Task.Run(() => Validator.TryValidateProperty(value, context, results));

        if (isValid)
        {
            ResetValidation(component);
            return true;
        }

        SetError(component, results.FirstOrDefault().ErrorMessage);

        return false;
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


