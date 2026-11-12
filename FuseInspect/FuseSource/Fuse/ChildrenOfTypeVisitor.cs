using System.Collections.Generic;

namespace Fuse;

public class ChildrenOfTypeVisitor<TNode> : IShaderNodeVisitor where TNode : class
{
	public readonly HashSet<TNode> Result = new HashSet<TNode>();

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		if (node is TNode item)
		{
			Result.Add(item);
		}
	}
}
