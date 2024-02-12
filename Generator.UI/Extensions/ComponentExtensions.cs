using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Models.ServiceModels;
using MagicOnion;

namespace Generator.UI.Extensions;

public static class ComponentExtensions
{
	public static async Task<List<TModel>> FillAsync<TModel>(this GenComboBox genComboBox,Func<UnaryResult<RESPONSE_RESULT<List<TModel>>>> func) where TModel:class
	{
		var result = await func();

        genComboBox.DataSource = result.Data;

		return result.Data;
	}

    public static void VisibleIf(this IGenControl component, Func<bool> predicate)
    {
        component.EditorVisible = predicate();
    }

    public static void Hide(this IGenControl component)
    {
        component.SetEmpty();
        component.EditorVisible = false;
       
    }

    public static void Show(this IGenControl component)
    {
        component.EditorVisible = true;
    }

    public static void Disable(this IGenControl component)
    {
        component.SetEmpty();
        //component.EditorEnabled = false;

    }

    public static void Enable(this IGenControl component)
    {
        //component.EditorEnabled = true;
    }

    public static void EnabledIf(this IGenControl component, Func<bool> predicate)
    {
        //component.EditorEnabled = predicate();
    }

}
