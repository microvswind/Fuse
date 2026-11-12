using System;
using System.Collections.Generic;
using System.Linq;
using VL.Core;

namespace Fuse.function;

public class Invoke<T> : ShaderNode<T>
{
	private readonly Delegate<T> _delegate;

	public sealed override IDictionary<string, string> Functions { get; protected set; }

	public Invoke(NodeContext nodeContext, Delegate<T> theDelegate, IEnumerable<AbstractShaderNode> theValues, string theId = "Invoke", ShaderNode<T> theDefault = null)
		: base(nodeContext, theId, theDefault, true)
	{
		Functions = new Dictionary<string, string>();
		Name = theId;
		_delegate = theDelegate;
		OptionalOutputs = new List<AbstractShaderNode>();
		SetInputs(theDelegate, theValues);
	}

	private void SetInputs(Delegate<T> theDelegate, IEnumerable<AbstractShaderNode> theValues)
	{
		if (theDelegate == null)
		{
			return;
		}
		List<AbstractShaderNode> list = new List<AbstractShaderNode> { theDelegate };
		NodeSubContextFactory theSubContextFactory = new NodeSubContextFactory(NodeContext);
		List<AbstractShaderNode> list2 = theValues.ToList();
		int num = 0;
		foreach (IFunctionParameter parameter in theDelegate.Parameters)
		{
			switch (parameter.Modifier)
			{
			case InputModifier.In:
			{
				AbstractShaderNode theValue = list2[num++];
				list.Add(theValue);
				break;
			}
			case InputModifier.Out:
			{
				AbstractShaderNode abstractShaderNode2 = AbstractCreation.AbstractDeclareValue(theSubContextFactory, parameter as AbstractShaderNode);
				list.Add(abstractShaderNode2);
				AbstractShaderNode item2 = AbstractCreation.AbstractOutput(theSubContextFactory, this, abstractShaderNode2);
				OptionalOutputs.Add(item2);
				break;
			}
			case InputModifier.InOut:
			{
				AbstractShaderNode theValue = list2[num++];
				AbstractShaderNode abstractShaderNode = AbstractCreation.AbstractDeclareValueAssigned(theSubContextFactory, theValue);
				list.Add(abstractShaderNode);
				AbstractShaderNode item = AbstractCreation.AbstractOutput(theSubContextFactory, this, abstractShaderNode);
				OptionalOutputs.Add(item);
				break;
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
		SetInputs(list);
	}

	protected override string SourceTemplate()
	{
		if (_delegate == null)
		{
			return GenerateDefaultSource();
		}
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = ${functionName}(${arguments});", new Dictionary<string, string>
		{
			{ "functionName", _delegate.FunctionName },
			{
				"arguments",
				ShaderNodesUtil.BuildArguments(Ins.GetRange(1, Ins.Count - 1))
			}
		});
	}
}
