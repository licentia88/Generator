using Generator.Components.Components;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.NonDB;
using Generator.UI.Models;

namespace Generator.UI.Helpers;

public class DataHelpers
{
    public static List<CODE_ENUM> GetEnumValues<TEnum>() where TEnum:Enum
    {
        var type = typeof(TEnum);

        return  ((IEnumerable<TEnum>)Enum.GetValues(type)).Select((TEnum arg1, int arg2) => new CODE_ENUM { C_CODE = arg2, C_DESC = arg1.ToString() }).ToList();
            
    }

    
}