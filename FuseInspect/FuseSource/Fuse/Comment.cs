using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class Comment<T> : PassThroughNode<T>
{
	private readonly string _comment;

	public Comment(NodeContext nodeContext, string theComment, ShaderNode<T> theIn)
		: base(nodeContext, (AbstractShaderNode)theIn, "Comment")
	{
		_comment = theComment;
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("\r\n        // ${comment}\r\n        ", new Dictionary<string, string> { { "comment", _comment } });
	}
}
