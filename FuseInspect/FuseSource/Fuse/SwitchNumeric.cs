using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VL.Core;

namespace Fuse;

public class SwitchNumeric<T> : ShaderNode<T>
{
	private new const string ShaderCode = "\r\n    ${resultType} ${resultName}; \r\n    switch(${check}){\r\n${cases}\r\n    }\r\n        ";

	private readonly List<ShaderNode<T>> _cases;

	private readonly ShaderNode<int> _check;

	private readonly ShaderNode<T> _default;

	public SwitchNumeric(NodeContext nodeContext, ShaderNode<int> theCheck, IEnumerable<ShaderNode<T>> inputs, ShaderNode<T> theDefault)
		: base(nodeContext, "numericSwitch", (ShaderNode<T>)null, true)
	{
		_check = theCheck;
		_cases = inputs.ToList();
		_default = theDefault;
		List<AbstractShaderNode> list = new List<AbstractShaderNode> { _check };
		list.AddRange(_cases);
		if (theDefault != null)
		{
			list.Add(theDefault);
		}
		SetInputs(list);
	}

	private static string BuildCases(IEnumerable<AbstractShaderNode> inputs, ShaderNode<T> theDefault)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int c = 0;
		inputs.ForEach((AbstractShaderNode input) =>
		{
			string text = ((input != null) ? input.ID : TypeHelpers.GetDefaultForType<T>());
			stringBuilder.Append("    case " + c + ":" + Environment.NewLine);
			stringBuilder.Append("        ${resultName} = " + text + ";" + Environment.NewLine);
			stringBuilder.Append("        break;" + Environment.NewLine);
			c++;
		});
		if (theDefault == null)
		{
			return stringBuilder.ToString();
		}
		stringBuilder.Append("    default:" + Environment.NewLine);
		stringBuilder.Append("        ${resultName} = " + theDefault.ID + ";" + Environment.NewLine);
		stringBuilder.Append("        break;");
		return stringBuilder.ToString();
	}

	protected override Dictionary<string, string> CreateTemplateMap()
	{
		Dictionary<string, string> dictionary = base.CreateTemplateMap();
		dictionary["check"] = _check.ID;
		return dictionary;
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("\r\n    ${resultType} ${resultName}; \r\n    switch(${check}){\r\n${cases}\r\n    }\r\n        ", new Dictionary<string, string> { 
		{
			"cases",
			BuildCases(_cases, _default)
		} });
	}
}
