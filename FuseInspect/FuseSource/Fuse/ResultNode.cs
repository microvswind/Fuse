using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public abstract class ResultNode<T> : ShaderNode<T>
{
	protected ResultNode(NodeContext nodeContext, string theId, ShaderNode<T> theDefault = null, bool theCreateDefault = true)
		: base(nodeContext, theId, theDefault, theCreateDefault)
	{
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = ${implementation};", new Dictionary<string, string> { 
		{
			"implementation",
			ImplementationTemplate()
		} });
	}

	protected abstract string ImplementationTemplate();
}
