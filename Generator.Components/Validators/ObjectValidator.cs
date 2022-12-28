using System;
using FluentValidation;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using static MudBlazor.CategoryTypes;

namespace Generator.Components.Validators;

public class ObjectValidator : AbstractValidator<object>
{
    public ObjectValidator()
    {
        RuleFor(x => x.GetPropertyValue("Name")).NotEmpty();

    }

    public IEnumerable<string> ValidateValue(object model, string propertyName)
    {

        var result = Validate(ValidationContext<object>.CreateWithOptions((object)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
        {
            return Array.Empty<string>();
        }
        return result.Errors.Select(e => e.ErrorMessage);
    }

    public async Task<IEnumerable<string>> ValidateValueAsync(object model, string propertyName)
    {
        RuleFor(x => x.GetPropertyValue(propertyName)).NotEmpty().NotNull();

        var result = await ValidateAsync(ValidationContext<object>.CreateWithOptions((object)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
        {
            return Array.Empty<string>();
        }
        return result.Errors.Select(e => e.ErrorMessage);
    }
}