using System;

namespace Fuse;

public class PrintShaderNodeVisitor : IShaderNodeVisitor
{
	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		if (node != null)
		{
			Console.WriteLine(new string('.', recursionLevel) + " " + node.ID + node.GetHashCode());
		}
	}
}
