using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using VL.Core;

namespace Fuse.compute;

public abstract class AbstractTypedFunction<TIn, TOut> : ShaderNode<TOut> where TIn : struct
{
	protected AbstractTypedFunction(NodeContext nodeContext, string theName, ShaderNode<TOut> theDefault = null)
		: base(nodeContext, theName, theDefault, true)
	{
		AddProperty("Structs", BuildStructs());
	}

	private static string BuildStructs()
	{
		if (!TypeHelpers.IsStructType<TIn>())
		{
			return "";
		}
		StringBuilder call = new StringBuilder();
		typeof(TIn).GetProperties().ForEach((PropertyInfo property) =>
		{
			call.Append("        " + TypeHelpers.GetGpuType(property.PropertyType) + " " + property.Name + ";" + Environment.NewLine);
		});
		return ShaderNodesUtil.Evaluate("    struct ${structName}{\r\n${structMembers}\r\n    };", new Dictionary<string, string>
		{
			{
				"structName",
				typeof(TIn).Name
			},
			{
				"structMembers",
				call.ToString()
			}
		});
	}
}
