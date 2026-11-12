using System;
using System.Collections.Generic;

namespace Fuse;

public static class EnumerableExtensionForEach
{
	public static void ForEach<T>(this IEnumerable<T> list, Action<T> block)
	{
		foreach (T item in list)
		{
			block(item);
		}
	}
}
