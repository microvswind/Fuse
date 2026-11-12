using System.Collections.Generic;
using VL.Core;

namespace Fuse.compute;

public class DeclareStructVariable<T> : AbstractTypedFunction<T, T> where T : struct
{
	public DeclareStructVariable(NodeContext nodeContext, ConstantValue<T> theDefault)
		: base(nodeContext, "getBuffer", (ShaderNode<T>)null)
	{
		SetInputs(new List<AbstractShaderNode>());
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName};", new Dictionary<string, string>());
	}
}
