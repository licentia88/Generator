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

    public static void VisibleIf(this IGenComponent component, Func<bool> predicate)
    {
        component.EditorVisible = predicate();
    }

    public static void Hide(this IGenComponent component)
    {
        component.SetEmpty();
        component.EditorVisible = false;
       
    }

    public static void Show(this IGenComponent component)
    {
        component.EditorVisible = true;
    }

    public static void Disable(this IGenComponent component)
    {
        component.SetEmpty();
        component.EditorEnabled = false;

    }

    public static void Enable(this IGenComponent component)
    {
        component.EditorEnabled = true;
    }

    public static void EnabledIf(this IGenComponent component, Func<bool> predicate)
    {
        component.EditorEnabled = predicate();
    }

}
