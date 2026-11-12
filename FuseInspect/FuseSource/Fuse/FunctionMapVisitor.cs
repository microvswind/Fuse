using System.Collections.Generic;

namespace Fuse;

public class FunctionMapVisitor : IShaderNodeVisitor
{
	public readonly Dictionary<string, string> Result = new Dictionary<string, string>();

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		if (node.Functions == null)
		{
			return;
		}
		foreach (KeyValuePair<string, string> function in node.Functions)
		{
			if (!Result.ContainsKey(function.Key))
			{
				Result.Add(function.Key, function.Value);
			}
		}
	}
}
