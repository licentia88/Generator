using Generator.Components.Components;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Models;

namespace Generator.UI.Helpers
{
    public class DataHelpers
	{
        public static List<CODE_ENUM> GetEnumValues<TEnum>() where TEnum:Enum
        {
            var type = typeof(TEnum);

            return  ((IEnumerable<TEnum>)Enum.GetValues(type)).Select((TEnum arg1, int arg2) => new CODE_ENUM { C_CODE = arg2, C_DESC = arg1.ToString() }).ToList();
            
        }

        public static void FillGenComponents(ref List<GEN_COMPONENT_TYPES> componentList)
        {
            componentList.Add(new GEN_COMPONENT_TYPES { GCT_NAME = nameof(GenTextField) });
            componentList.Add(new GEN_COMPONENT_TYPES { GCT_NAME = nameof(GenComboBox) });
            componentList.Add(new GEN_COMPONENT_TYPES { GCT_NAME = nameof(GenCheckBox) });
            componentList.Add(new GEN_COMPONENT_TYPES { GCT_NAME = nameof(GenDatePicker) });
            componentList.Add(new GEN_COMPONENT_TYPES { GCT_NAME = nameof(GenSpacer) });

        }
    }
}

