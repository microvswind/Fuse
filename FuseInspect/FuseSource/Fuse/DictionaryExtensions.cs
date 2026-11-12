using System;
using System.Collections.Generic;

namespace Fuse;

public static class DictionaryExtensions
{
	public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dict, Action<KeyValuePair<TKey, TValue>> action)
	{
		foreach (KeyValuePair<TKey, TValue> item in dict)
		{
			action(item);
		}
	}
}
