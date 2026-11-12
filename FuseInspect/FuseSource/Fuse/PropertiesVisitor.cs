using System.Collections;
using System.Collections.Generic;

namespace Fuse;

public class PropertiesVisitor : IShaderNodeVisitor
{
	public readonly Dictionary<string, IList> Result = new Dictionary<string, IList>();

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		foreach (KeyValuePair<string, IList> item in node.Property)
		{
			if (!Result.ContainsKey(item.Key))
			{
				ArrayList value = new ArrayList();
				Result[item.Key] = value;
			}
			foreach (object item2 in item.Value)
			{
				Result[item.Key].Add(item2);
			}
		}
	}
}
