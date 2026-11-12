using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class Semantic<T> : ShaderNode<T>
{
	public override string ID => "streams." + Name;

	public override string DelegateID => base.ID;

	public Semantic(NodeContext nodeContext, string theName, bool define = false, string theSemantic = null)
		: base(nodeContext, "Semantic", (ShaderNode<T>)null, true)
	{
		Name = theName;
		HasFixedName = true;
		SetInputs(new List<AbstractShaderNode>());
		if (define)
		{
			if (theSemantic == null)
			{
				string theProperty = ShaderNodesUtil.Evaluate("    stage stream ${type} ${name};", new Dictionary<string, string>
				{
					{
						"type",
						TypeName()
					},
					{ "name", theName }
				});
				SetProperty("Streams", theProperty);
			}
			else
			{
				string theProperty2 = ShaderNodesUtil.Evaluate("    stage stream ${type} ${name} : ${semantic};", new Dictionary<string, string>
				{
					{
						"type",
						TypeName()
					},
					{ "name", theName },
					{ "semantic", theSemantic }
				});
				SetProperty("Streams", theProperty2);
			}
		}
	}

	public sealed override string TypeName()
	{
		return base.TypeName();
	}

	protected override string SourceTemplate()
	{
		return "";
	}
}
