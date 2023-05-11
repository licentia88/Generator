using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Generator.Components.Helpers;
using Generator.Components.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.Components.Extensions
{



    public static class DependencyExtensions
    {
        public static void RegisterGeneratorComponents(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(GenValidator<>));
            Services.AddScoped<GeneratorJs>();
            Services.AddScoped<GenExcel>();
             

        }
    }
}

