using Generator.UI.Models;

namespace Generator.UI.Helpers;

public class DataHelpers
{
    public static List<CODE_ENUM> GetEnumValues<TEnum>() where TEnum:Enum
    {
        var type = typeof(TEnum);

        return  Enum.GetValues(type).Cast<TEnum>().Select((TEnum @enum) => new CODE_ENUM { C_CODE = Convert.ToInt32(@enum), C_DESC = @enum.ToString() }).ToList();
            
    }

    
}