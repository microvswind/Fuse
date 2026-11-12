using System.Collections;
using System.Collections.Generic;

namespace Fuse;

public class PropertyIdsVisitor : IShaderNodeVisitor
{
	public readonly HashSet<string> Result = new HashSet<string>();

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		foreach (KeyValuePair<string, IList> item in node.Property)
		{
			Result.Add(item.Key);
		}
	}
}
