using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VL.Core;

namespace Fuse.compute;

public class DynamicStruct<T> : ShaderNode<T>
{
	private readonly string _sourceTemplate = "";

	private readonly string _structName;

	public List<string> Description
	{
		get
		{
			List<string> result = new List<string>();
			Ins.ForEach((AbstractShaderNode input) =>
			{
				result.Add(_structName + "." + TypeHelpers.GetDescription(input));
			});
			return result;
		}
	}

	public int Stride { get; private set; }

	public DynamicStruct(NodeContext nodeContext, IEnumerable<AbstractShaderNode> theInputs, string theName, T instance)
		: base(nodeContext, "GPUAttributeStruct", (ShaderNode<T>)null, true)
	{
		_structName = theName;
		int myStride = 0;
		StringBuilder call = new StringBuilder();
		theInputs.ForEach((AbstractShaderNode input) =>
		{
			if (TypeHelpers.IsGpuArray(input))
			{
				int num = ((!(input is IStructureBufferAttribute structureBufferAttribute)) ? 1 : structureBufferAttribute.ArrayCount);
				call.Append("        " + TypeHelpers.GetGpuType(input) + " " + input.Name + "[" + num + "];" + Environment.NewLine);
				myStride += TypeHelpers.GetSizeInBytes(input);
			}
			else
			{
				call.Append("        " + TypeHelpers.GetGpuType(input) + " " + input.Name + ";" + Environment.NewLine);
				myStride += TypeHelpers.GetSizeInBytes(input);
			}
		});
		string theProperty = ShaderNodesUtil.Evaluate("    struct ${structName}{\r\n${structMembers}\r\n    };", new Dictionary<string, string>
		{
			{ "structName", theName },
			{
				"structMembers",
				call.ToString()
			}
		});
		SetProperty("Structs", theProperty);
		Stride = myStride;
		TypeOverride = theName;
		SetInputs(theInputs);
	}

	public DynamicStruct(NodeContext nodeContext, Dictionary<string, AbstractShaderNode> theInputs, T instance, bool AddTypesToName = false)
		: base(nodeContext, "GPUAttributeStruct", (ShaderNode<T>)null, true)
	{
		string text = TypeHelpers.GetGpuType<T>();
		Name = ShaderNodesUtil.FirstLetterToLower(text);
		if (AddTypesToName)
		{
			text = theInputs.Aggregate(text, (string text2, KeyValuePair<string, AbstractShaderNode> input) => text2 + TypeHelpers.GetGpuType(input.Value));
		}
		_structName = text;
		int num = 0;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<string, AbstractShaderNode> theInput in theInputs)
		{
			stringBuilder.Append("        " + TypeHelpers.GetGpuType(theInput.Value) + " " + theInput.Key + ";" + Environment.NewLine);
			num += TypeHelpers.GetSizeInBytes(theInput.Value);
		}
		string theProperty = ShaderNodesUtil.Evaluate("    struct ${structName}{\r\n${structMembers}\r\n    };", new Dictionary<string, string>
		{
			{ "structName", _structName },
			{
				"structMembers",
				stringBuilder.ToString()
			}
		});
		StringBuilder stringBuilder2 = new StringBuilder();
		stringBuilder2.Append("${resultType} ${resultName};" + Environment.NewLine);
		foreach (KeyValuePair<string, AbstractShaderNode> theInput2 in theInputs)
		{
			stringBuilder2.Append("        ${resultName}." + theInput2.Key + " = " + theInput2.Value.ID + ";" + Environment.NewLine);
			num += TypeHelpers.GetSizeInBytes(theInput2.Value);
		}
		_sourceTemplate = ShaderNodesUtil.Evaluate(stringBuilder2.ToString(), new Dictionary<string, string> { { "resultType", _structName } });
		SetProperty("Structs", theProperty);
		Stride = num;
		TypeOverride = _structName;
		SetInputs(theInputs.Values);
	}

	protected override string SourceTemplate()
	{
		return _sourceTemplate;
	}
}
