using Generator.Components.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.Components.Extensions
{
    public static class DependencyExtensions
    {
        public static void RegisterComponents(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(GenValidator<>));
        }
    }
}

