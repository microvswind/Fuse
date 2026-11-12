using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse.compute;

public class InterlockedBuffer<T> : ShaderNode<GpuVoid>, IComputeVoid, IComputeNode
{
	private readonly IBufferInput<T> _buffer;

	private readonly string _function;

	private readonly ShaderNode<int> _index;

	private readonly ShaderNode<T> _lastValue;

	private readonly ShaderNode<T> _value;

	public InterlockedBuffer(NodeContext nodeContext, string theFunction, IBufferInput<T> theBuffer, ShaderNode<int> theIndex, ShaderNode<T> theValue)
		: base(nodeContext, "interlocked", (ShaderNode<GpuVoid>)null, true)
	{
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(NodeContext);
		_lastValue = new DeclareValue<T>(nodeSubContextFactory.NextSubContext());
		int num = 1;
		List<AbstractShaderNode> list = new List<AbstractShaderNode>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<AbstractShaderNode> span = CollectionsMarshal.AsSpan(list);
		int index = 0;
		span[index] = _lastValue;
		OptionalOutputs = list;
		_function = theFunction;
		_buffer = theBuffer;
		_index = theIndex;
		_value = theValue;
		SetInputs(new List<AbstractShaderNode>
		{
			_buffer as AbstractShaderNode,
			_index,
			_value,
			_lastValue
		});
	}

	protected override Dictionary<string, string> CreateTemplateMap()
	{
		return new Dictionary<string, string>();
	}

	protected override string GenerateDefaultSource()
	{
		return "";
	}

	protected override string SourceTemplate()
	{
		if (TypeHelpers.GetDimension<T>() == 1)
		{
			return ShaderNodesUtil.Evaluate("${function}(${bufferName}[${index}], ${value}, ${lastValue});", new Dictionary<string, string>
			{
				{ "function", _function },
				{ "bufferName", _buffer.ID },
				{ "index", _index.ID },
				{ "value", _value.ID },
				{ "lastValue", _lastValue.ID }
			});
		}
		string[] array = new string[4] { "x", "y", "z", "w" };
		string text = "";
		for (int i = 0; i < TypeHelpers.GetDimension<T>(); i++)
		{
			text = text + "${function}(${bufferName}[${index}]." + array[i] + ", ${value}." + array[i] + ", ${lastValue}." + array[i] + ");\n";
		}
		return ShaderNodesUtil.Evaluate(text, new Dictionary<string, string>
		{
			{ "function", _function },
			{ "bufferName", _buffer.ID },
			{ "index", _index.ID },
			{ "value", _value.ID },
			{ "lastValue", _lastValue.ID }
		});
	}
}
