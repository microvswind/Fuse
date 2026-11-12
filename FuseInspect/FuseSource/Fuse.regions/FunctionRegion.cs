using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fuse.Function;
using Fuse.function;
using VL.Core;

namespace Fuse.regions;

public class FunctionRegion
{
	public sealed class RegionFunctionParameter<T> : ShaderNode<T>
	{
		public RegionFunctionParameter(NodeContext nodeContext, ShaderNode<T> theType, int theId = 0)
			: base(nodeContext, "argument", (ShaderNode<T>)null, true)
		{
			Ins = new List<AbstractShaderNode>();
			Name = "arg_" + theId;
		}

		public override string TypeName()
		{
			return TypeHelpers.GetGpuType<T>();
		}

		public string Namer()
		{
			return ID;
		}

		protected override string SourceTemplate()
		{
			return "";
		}
	}

	public sealed class RegionFunction<T> : AbstractFunction<T>
	{
		private readonly string _name;

		private readonly string _signature;

		public override IDictionary<string, string> Functions { get; protected set; }

		public RegionFunction(NodeContext nodeContext, string theName, ShaderNode<T> theDefault, AbstractShaderNode theFunction, IEnumerable<AbstractShaderNode> theArguments, IEnumerable<IDelegate> theDelegates = null, IEnumerable<string> theMixins = null, IDictionary<string, string> theFunctionValues = null, bool theIsGroupable = false, IEnumerable<InputModifier> theModifiers = null)
			: base(nodeContext, "patchedFunction", theArguments, theModifiers, theDefault, theIsGroupable, 0)
		{
			_name = theName;
			if (theMixins != null)
			{
				ArrayList mixinList = new ArrayList();
				theMixins.ForEach((string m) =>
				{
					mixinList.Add(m);
				});
				AddProperties("Mixins", mixinList);
				Functions = new Dictionary<string, string>();
				_signature = _name + BuildSignature(theArguments) + "To" + TypeHelpers.GetSignature<T>();
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{
						"resultType",
						TypeHelpers.GetGpuType<T>()
					},
					{ "functionName", _signature },
					{
						"arguments",
						BuildArguments(theArguments)
					},
					{
						"functionImplementation",
						theFunction.BuildSourceCode()
					},
					{ "result", theFunction.ID }
				};
				List<AbstractShaderNode> inputs = (Ins = theArguments.ToList());
				HandleDelegates(theDelegates, dictionary);
				Functions.Add(_signature, ShaderNodesUtil.Evaluate("    ${resultType} ${functionName}(${arguments}){\r\n${functionImplementation}\r\n        return ${result};\r\n    }", dictionary) + Environment.NewLine);
				SetInputs(inputs);
			}
		}

		protected override Dictionary<string, string> CreateTemplateMap()
		{
			Dictionary<string, string> dictionary = base.CreateTemplateMap();
			dictionary["function"] = _signature;
			return dictionary;
		}

		private void HandleDelegates(IEnumerable<IDelegate> theDelegates, IDictionary<string, string> theFunctionValueMap)
		{
			theDelegates?.ForEach((IDelegate delegateNode) =>
			{
				if (delegateNode != null)
				{
					theFunctionValueMap.Add(delegateNode.Name, delegateNode.FunctionName);
					delegateNode.Functions.ForEach((KeyValuePair<string, string> kv) =>
					{
						Functions[kv.Key] = kv.Value;
					});
					delegateNode.PropertiesForTree().ForEach((KeyValuePair<string, IList> kv) =>
					{
						AddProperties(kv.Key, kv.Value);
					});
				}
			});
		}

		private static string BuildSignature(IEnumerable<AbstractShaderNode> inputs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			inputs.ForEach((AbstractShaderNode input) =>
			{
				stringBuilder.Append(TypeHelpers.GetSignature(input.GetType().GetGenericArguments()[0]));
			});
			return stringBuilder.ToString();
		}

		private static string BuildArguments(IEnumerable<AbstractShaderNode> inputs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int c = 0;
			inputs.ForEach((AbstractShaderNode input) =>
			{
				stringBuilder.Append(input.TypeName());
				stringBuilder.Append(" ");
				stringBuilder.Append("arg_" + c);
				stringBuilder.Append(", ");
				c++;
			});
			if (stringBuilder.Length > 2)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			return stringBuilder.ToString();
		}
	}
}
