using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fuse;

public class PropertiesTypedVisitor<TProperty> : IShaderNodeVisitor
{
	public readonly Dictionary<string, List<TProperty>> Result = new Dictionary<string, List<TProperty>>();

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		foreach (KeyValuePair<string, IList> item in node.Property)
		{
			IEnumerable<TProperty> enumerable = item.Value.OfType<TProperty>();
			if (EnumerableEx.IsEmpty<TProperty>(enumerable))
			{
				break;
			}
			if (!Result.ContainsKey(item.Key))
			{
				List<TProperty> value = new List<TProperty>();
				Result[item.Key] = value;
			}
			foreach (TProperty item2 in enumerable)
			{
				Result[item.Key].Add(item2);
			}
		}
	}
}
