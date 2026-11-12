using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Fuse;

public class BuildSourceVisitor : IShaderNodeVisitor
{
	private HashSet<AbstractShaderNode> myHashes = new HashSet<AbstractShaderNode>();

	private StringBuilder myStringBuilder = new StringBuilder();

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		foreach (KeyValuePair<string, IList> item in node.Property)
		{
			_ = item;
		}
	}
}
