using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class FloatJoinAdaptive<TIn, TExtension, TOut> : ResultNode<TOut> where TOut : struct
{
	private readonly ShaderNode<TExtension> _extension;

	private readonly ShaderNode<TIn> _input;

	public FloatJoinAdaptive(NodeContext nodeContext, ShaderNode<TIn> theInput, ShaderNode<TExtension> theExtension)
		: base(nodeContext, "join", (ShaderNode<TOut>)null, true)
	{
		_input = theInput ?? ConstantHelper.FromFloat<TIn>(0f);
		_extension = theExtension ?? ConstantHelper.FromFloat<TExtension>(0f);
		SetInputs(new List<AbstractShaderNode> { _input, _extension });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate(TypeHelpers.GetDimension(typeof(TOut)) switch
		{
			2 => "float2(${input},${extension})", 
			3 => "float3(${input},${extension})", 
			4 => "float4(${input},${extension})", 
			_ => "", 
		}, new Dictionary<string, string>
		{
			{ "input", _input.ID },
			{ "extension", _extension.ID }
		});
	}
}
