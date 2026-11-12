using System;
using System.Collections.Generic;
using System.Text;
using VL.Core;

namespace Fuse.regions;

public class IfGroup : Group
{
	private readonly ShaderNode<bool> _inCheck;

	public IfGroup(NodeContext nodeContext, ShaderNode<bool> inCheck, IEnumerable<AbstractShaderNode> theInputs)
		: base(nodeContext, theInputs, "IfGroup")
	{
		Name = "IfGroup";
		_inCheck = inCheck;
	}

	protected override void BuildSource(StringBuilder theSourceBuilder, HashSet<AbstractShaderNode> theHashes, string thePrepend)
	{
		if (theHashes.Add(this))
		{
			if (ShaderNodesUtil.DebugShaderGeneration)
			{
				Console.WriteLine(thePrepend + ID);
			}
			theSourceBuilder.Append(ShaderNodesUtil.Evaluate("\r\n        if(${check}){", new Dictionary<string, string> { { "check", _inCheck.ID } }));
			theSourceBuilder.AppendLine();
			StringBuilder stringBuilder = new StringBuilder();
			BuildChildrenSource(stringBuilder, theHashes, thePrepend);
			string theCode = stringBuilder.ToString();
			theCode = ShaderNodesUtil.IndentCode(theCode);
			theSourceBuilder.Append(theCode);
			string sourceCode = SourceCode;
			if (!string.IsNullOrWhiteSpace(sourceCode.Trim()) && theHashes.Add(this))
			{
				theSourceBuilder.Append("        ");
				theSourceBuilder.Append(sourceCode);
				theSourceBuilder.Append(Environment.NewLine);
			}
			theSourceBuilder.Append("        }\r\n");
		}
	}
}
