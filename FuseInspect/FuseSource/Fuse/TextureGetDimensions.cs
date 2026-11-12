using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Fuse.compute;
using VL.Core;

namespace Fuse;

public class TextureGetDimensions : ShaderNode<GpuVoid>
{
	private readonly List<AbstractShaderNode> _arguments;

	private readonly ITextureInput _texture;

	public TextureGetDimensions(NodeContext nodeContext, int dimension, ITextureInputProvider theTextureProvider, ShaderNode<int> mipLevel)
		: base(nodeContext, "textureGetDimension", (ShaderNode<GpuVoid>)null, true)
	{
		OptionalOutputs = new List<AbstractShaderNode>();
		_texture = theTextureProvider?.GetTextureInput();
		OptionalOutputs.Clear();
		int num = 1;
		List<AbstractShaderNode> list = new List<AbstractShaderNode>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<AbstractShaderNode> span = CollectionsMarshal.AsSpan(list);
		int index = 0;
		span[index] = mipLevel;
		_arguments = list;
		List<AbstractShaderNode> list2 = new List<AbstractShaderNode>
		{
			_texture as AbstractShaderNode,
			mipLevel
		};
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(NodeContext);
		for (int i = 0; i < dimension + 1; i++)
		{
			DeclareValue<float> item = new DeclareValue<float>(nodeSubContextFactory.NextSubContext());
			list2.Add(item);
			_arguments.Add(item);
			OptionalOutputs.Add(item);
		}
		SetInputs(list2);
	}

	protected override string SourceTemplate()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string> { 
		{
			"arguments",
			ShaderNodesUtil.BuildArguments(_arguments)
		} };
		if (_texture != null)
		{
			dictionary["texture"] = _texture.TextureID();
		}
		return ShaderNodesUtil.Evaluate("${texture}.GetDimensions(${arguments});", dictionary);
	}
}
