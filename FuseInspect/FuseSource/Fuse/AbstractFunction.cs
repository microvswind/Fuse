using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fuse.function;
using VL.Core;

namespace Fuse;

public abstract class AbstractFunction<T> : ShaderNode<T>
{
	private readonly int _groupOptions;

	private readonly IEnumerable<InputModifier> _modifiers;

	private bool _isGroupable;

	public virtual string FunctionName { get; }

	protected AbstractFunction(NodeContext nodeContext, string theFunction, IEnumerable<AbstractShaderNode> theArguments, IEnumerable<InputModifier> theModifiers, ShaderNode<T> theDefault, bool theIsGroupable = false, int theGroupOptions = 0)
		: base(nodeContext, theFunction, theDefault, true)
	{
		FunctionName = theFunction;
		_modifiers = theModifiers;
		_isGroupable = theIsGroupable;
		_groupOptions = theGroupOptions;
		OptionalOutputs = new List<AbstractShaderNode>();
		Setup(theArguments);
	}

	protected void Setup(IEnumerable<AbstractShaderNode> theArguments)
	{
		List<AbstractShaderNode> list = theArguments.ToList();
		if (_modifiers == null || _modifiers.Count() != Enumerable.Count(list))
		{
			SetInputs(list);
			return;
		}
		NodeSubContextFactory theSubContextFactory = new NodeSubContextFactory(NodeContext);
		List<AbstractShaderNode> list2 = new List<AbstractShaderNode>();
		List<InputModifier> list3 = _modifiers.ToList();
		for (int i = 0; i < list.Count; i++)
		{
			if (list3[i] == InputModifier.In)
			{
				list2.Add(list[i]);
				continue;
			}
			AbstractShaderNode abstractShaderNode = AbstractCreation.AbstractDeclareValueAssigned(theSubContextFactory, list[i]);
			list2.Add(abstractShaderNode);
			AbstractShaderNode item = AbstractCreation.AbstractOutput(theSubContextFactory, this, abstractShaderNode);
			OptionalOutputs.Add(item);
			_isGroupable = false;
		}
		SetInputs(list2);
	}

	protected override Dictionary<string, string> CreateTemplateMap()
	{
		Dictionary<string, string> dictionary = base.CreateTemplateMap();
		dictionary["function"] = FunctionName;
		return dictionary;
	}

	protected override string SourceTemplate()
	{
		string text = "";
		if (!TypeHelpers.IsVoidNode(this))
		{
			text = "${resultType} ${resultName} = ";
		}
		if (!_isGroupable || Ins.Count <= 2)
		{
			return text + "${function}(${arguments});";
		}
		List<AbstractShaderNode> list = new List<AbstractShaderNode>(Ins);
		StringBuilder stringBuilder = new StringBuilder("${function}(" + list[0].ID + ", " + list[1].ID);
		int num = list.Count - _groupOptions;
		for (int i = num; i < list.Count; i++)
		{
			stringBuilder.Append(", ");
			stringBuilder.Append(list[i].ID);
		}
		stringBuilder.Append(')');
		for (int j = 2; j < num; j++)
		{
			stringBuilder.Insert(0, "${function}(");
			stringBuilder.Append(", ");
			stringBuilder.Append(list[j].ID);
			for (int k = num; k < list.Count; k++)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(list[k].ID);
			}
			stringBuilder.Append(')');
		}
		return ShaderNodesUtil.Evaluate(text + "${Call};", new Dictionary<string, string> { 
		{
			"Call",
			stringBuilder.ToString()
		} });
	}
}
