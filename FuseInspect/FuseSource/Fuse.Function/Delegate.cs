using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fuse.Function;
using Stride.Rendering.Materials;
using VL.Core;

namespace Fuse.function;

public class Delegate<T> : ShaderNode<T>, IDelegate
{
	private AbstractShaderNode _delegate;

	public List<IFunctionParameter> Parameters { get; protected init; }

	public sealed override IDictionary<string, string> Functions { get; protected set; }

	public virtual string FunctionName { get; }

	public Delegate(NodeContext nodeContext, AbstractShaderNode theDelegate, List<IFunctionParameter> theParameters, string theId = "Function", ShaderNode<T> theDefault = null)
		: base(nodeContext, theId, theDefault, true)
	{
		Parameters = theParameters ?? new List<IFunctionParameter>();
		if (theDelegate != null)
		{
			FunctionName = Name + "_" + HashCode;
			BuildFunction(theDelegate);
		}
	}

	public static List<IFunctionParameter> GetUniqueParameters(List<IFunctionParameter> parameters)
	{
		List<IFunctionParameter> uniqueParams = new List<IFunctionParameter>();
		foreach (IFunctionParameter item in parameters.Where((IFunctionParameter parameter) => uniqueParams.All((IFunctionParameter p) => p.ID != parameter.ID)))
		{
			uniqueParams.Add(item);
		}
		return uniqueParams.OrderBy((IFunctionParameter p) => p.ID).ToList();
	}

	protected void BuildFunction(AbstractShaderNode theDelegate)
	{
		_delegate = theDelegate;
		theDelegate.CheckHashCodes();
		Functions = new Dictionary<string, string>();
		Dictionary<string, string> theKeys = new Dictionary<string, string>
		{
			{
				"resultType",
				TypeHelpers.GetGpuType<T>()
			},
			{ "functionName", FunctionName },
			{
				"arguments",
				BuildArguments(Parameters)
			},
			{
				"functionImplementation",
				theDelegate.BuildSourceCode()
			},
			{ "result", theDelegate.ID }
		};
		string theShaderTemplate = ((!TypeHelpers.IsVoidNode(theDelegate)) ? "${resultType} ${functionName}(${arguments}){\r\n                ${functionImplementation}\r\n                return ${result};\r\n            }" : "${resultType} ${functionName}(${arguments}){\r\n                ${functionImplementation}\r\n            }");
		Functions.Add(FunctionName, ShaderNodesUtil.Evaluate(theShaderTemplate, theKeys) + Environment.NewLine);
		theDelegate.FunctionMap().ForEach((KeyValuePair<string, string> kv2) =>
		{
			Functions.Add(kv2);
		});
		theDelegate.PropertiesForTree().ForEach((KeyValuePair<string, IList> kv) =>
		{
			AddProperties(kv.Key, kv.Value);
		});
	}

	private static string BuildArguments(List<IFunctionParameter> inputs)
	{
		inputs.Sort((IFunctionParameter a, IFunctionParameter b) => string.Compare(a.ID, b.ID, StringComparison.Ordinal));
		HashSet<string> usedIDs = new HashSet<string>();
		StringBuilder stringBuilder = new StringBuilder();
		inputs.ForEach((IFunctionParameter input) =>
		{
			if (usedIDs.Add(input.ID))
			{
				stringBuilder.Append(input.ModifierString());
				stringBuilder.Append(' ');
				stringBuilder.Append(input.TypeName());
				stringBuilder.Append(' ');
				stringBuilder.Append(ShaderNodesUtil.FixName(input.ID));
				stringBuilder.Append(", ");
			}
		});
		if (stringBuilder.Length > 2)
		{
			stringBuilder.Remove(stringBuilder.Length - 2, 2);
		}
		return stringBuilder.ToString();
	}

	public override void OnPassContext(ShaderGeneratorContext nodeContext)
	{
		_delegate?.CheckContext(nodeContext);
	}

	protected override string SourceTemplate()
	{
		return "";
	}
}
