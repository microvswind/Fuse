using System;
using System.Collections.Generic;
using System.Text;
using VL.Core;

namespace Fuse;

public class Array<T> : ShaderNode<GpuArray<T>> where T : struct
{
	private readonly string _sourceTemplate = "";

	public int Size { get; }

	public Array(NodeContext nodeContext, ICollection<T> theInputs)
		: base(nodeContext, "Array", (ShaderNode<GpuArray<T>>)null, true)
	{
		_sourceTemplate = "";
		StringBuilder content = new StringBuilder();
		theInputs.ForEach((T input) =>
		{
			content.Append("        " + TypeHelpers.GetDefaultForType(input) + "," + Environment.NewLine);
		});
		string theProperty = ShaderNodesUtil.Evaluate("    static const ${arrayType} ${arrayName}[${arraySize}] = {\r\n${arrayContent}\r\n    };", new Dictionary<string, string>
		{
			{
				"arrayType",
				TypeHelpers.GetGpuType<T>()
			},
			{ "arrayName", ID },
			{
				"arraySize",
				theInputs.Count.ToString()
			},
			{
				"arrayContent",
				content.ToString()
			}
		});
		Size = theInputs.Count;
		SetProperty("ConstantArrays", theProperty);
	}

	public Array(NodeContext nodeContext, int theSize, bool theIsGroupShared = false)
		: base(nodeContext, "Array", (ShaderNode<GpuArray<T>>)null, true)
	{
		_sourceTemplate = ShaderNodesUtil.Evaluate("    ${groupshared} ${arrayType} ${arrayName}[${arraySize}];", new Dictionary<string, string>
		{
			{
				"groupshared",
				theIsGroupShared ? "groupshared" : ""
			},
			{
				"arrayType",
				TypeHelpers.GetGpuType<T>()
			},
			{ "arrayName", ID },
			{
				"arraySize",
				theSize.ToString()
			}
		});
		Size = theSize;
	}

	public Array(NodeContext nodeContext, ShaderNode<int> theSize, bool theIsGroupShared = false)
		: base(nodeContext, "Array", (ShaderNode<GpuArray<T>>)null, true)
	{
		_sourceTemplate = ShaderNodesUtil.Evaluate("    ${groupshared} ${arrayType} ${arrayName}[${arraySize}];", new Dictionary<string, string>
		{
			{
				"groupshared",
				theIsGroupShared ? "groupshared" : ""
			},
			{
				"arrayType",
				TypeHelpers.GetGpuType<T>()
			},
			{ "arrayName", ID },
			{ "arraySize", theSize.ID }
		});
	}

	public Array(NodeContext nodeContext, ICollection<ShaderNode<T>> theInputs)
		: base(nodeContext, "Array", (ShaderNode<GpuArray<T>>)null, true)
	{
		StringBuilder stringBuilder = new StringBuilder("    ${arrayType} ${arrayName}[${arraySize}];" + Environment.NewLine);
		int num = 0;
		foreach (ShaderNode<T> theInput in theInputs)
		{
			string text = ((theInput != null) ? theInput.ID : TypeHelpers.GetDefaultForType<T>());
			stringBuilder.Append("    ${arrayName}[" + num + "] =  " + text + ";" + Environment.NewLine);
			num++;
		}
		_sourceTemplate = ShaderNodesUtil.Evaluate(stringBuilder.ToString(), new Dictionary<string, string>
		{
			{
				"arrayType",
				TypeHelpers.GetGpuType<T>()
			},
			{ "arrayName", ID },
			{
				"arraySize",
				theInputs.Count.ToString()
			}
		});
		SetInputs(new List<AbstractShaderNode>(theInputs));
	}

	protected override string SourceTemplate()
	{
		return _sourceTemplate;
	}
}
