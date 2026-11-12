using System;
using System.Collections;
using System.Collections.Generic;
using Fuse.Function;
using Fuse.function;
using Stride.Rendering.Materials;
using VL.Core;

namespace Fuse;

public sealed class CustomFunction<T> : AbstractFunction<T>
{
	private readonly string _codeTemplate;

	private readonly IDictionary<string, IDelegate> _delegates;

	private readonly IDictionary<string, string> _templateValues;

	private string Signature => base.FunctionName + HashCode;

	public override IDictionary<string, string> Functions { get; protected set; }

	public override string FunctionName => Signature;

	public CustomFunction(NodeContext nodeContext, string theFunction, string theCodeTemplate, ShaderNode<T> theDefault, IEnumerable<AbstractShaderNode> theArguments, IDictionary<string, IDelegate> theDelegates, IEnumerable<string> theMixins = null, IDictionary<string, string> theFunctionValues = null, bool theIsGroupable = false, IEnumerable<InputModifier> theModifiers = null)
		: base(nodeContext, theFunction, theArguments, theModifiers, theDefault, theIsGroupable, 0)
	{
		UpdateMixins(theMixins);
		Functions = new Dictionary<string, string>();
		_delegates = theDelegates;
		_codeTemplate = ShaderNodesUtil.IndentCode(theCodeTemplate);
		_templateValues = theFunctionValues;
		BuildFunction();
	}

	private void UpdateMixins(IEnumerable<string> theMixins)
	{
		if (theMixins == null)
		{
			return;
		}
		ArrayList arrayList = new ArrayList();
		foreach (string theMixin in theMixins)
		{
			arrayList.Add(theMixin);
		}
		AddProperties("Mixins", arrayList);
	}

	private void BuildFunction()
	{
		Dictionary<string, string> functionValueMap = new Dictionary<string, string>
		{
			{
				"resultType",
				TypeHelpers.GetGpuType<T>()
			},
			{ "signature", Signature }
		};
		HandleDelegates(_delegates, functionValueMap);
		_templateValues?.ForEach((KeyValuePair<string, string> kv) =>
		{
			functionValueMap.Add(kv.Key, kv.Value);
		});
		Functions.Add(Signature, ShaderNodesUtil.Evaluate(_codeTemplate, functionValueMap) + Environment.NewLine);
	}

	private void HandleDelegates(IDictionary<string, IDelegate> theDelegates, IDictionary<string, string> theFunctionValueMap)
	{
		if (theDelegates == null)
		{
			return;
		}
		foreach (KeyValuePair<string, IDelegate> theDelegate in theDelegates)
		{
			if (theDelegate.Value != null)
			{
				theFunctionValueMap.Add(theDelegate.Key, theDelegate.Value.FunctionName);
				theDelegate.Value.Functions.ForEach((KeyValuePair<string, string> kv) =>
				{
					Functions[kv.Key] = kv.Value;
				});
				theDelegate.Value.PropertiesForTree().ForEach((KeyValuePair<string, IList> kv) =>
				{
					AddProperties(kv.Key, kv.Value);
				});
			}
		}
	}

	public override void OnPassContext(ShaderGeneratorContext theContext)
	{
		_delegates?.ForEach((KeyValuePair<string, IDelegate> delegateNode) =>
		{
			if (delegateNode.Value != null)
			{
				delegateNode.Value.CheckContext(theContext);
			}
		});
	}
}
