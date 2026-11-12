using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class ArrayGet<T> : ShaderNode<T> where T : struct
{
	private readonly Array<T> _array;

	private readonly ShaderNode<int> _index;

	public ArrayGet(NodeContext nodeContext, Array<T> theArray, ShaderNode<int> theIndex)
		: base(nodeContext, "getItem", (ShaderNode<T>)null, true)
	{
		_array = theArray;
		_index = theIndex;
		SetInputs(new List<AbstractShaderNode> { theArray, theIndex });
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = ${arrayName}[${index}];", new Dictionary<string, string>
		{
			{ "arrayName", _array.ID },
			{ "index", _index.ID }
		});
	}
}
