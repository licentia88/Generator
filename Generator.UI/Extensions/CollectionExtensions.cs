using System;
namespace Generator.UI.Extensions
{
	public static class CollectionExtensions
	{
		public static void AddOrReplace(this Dictionary<string,object> dictionary, string key, object value)
		{
			if (dictionary.Keys.Contains(key))
				dictionary[key] = value;
			else
				dictionary.Add(key, value);
		}
	}
}

