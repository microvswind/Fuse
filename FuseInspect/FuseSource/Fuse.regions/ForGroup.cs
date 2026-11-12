using System;
using System.Collections.Generic;
using System.Text;
using VL.Core;

namespace Fuse.regions;

public class ForGroup : Group
{
	private readonly ShaderNode<int> _inEnd;

	private readonly ShaderNode<int> _inStart;

	private readonly bool _loop;

	private readonly ForRegion _parentRegion;

	private readonly bool _unroll;

	private readonly int _unrollLoops;

	public ForGroup(NodeContext nodeContext, ForRegion parentRegion, ShaderNode<int> inStart, ShaderNode<int> inEnd, bool theLoop, bool theUnroll, int theUnrollLoops, IEnumerable<AbstractShaderNode> theInputs)
		: base(nodeContext, theInputs, "ForGroup")
	{
		Name = "ForGroup";
		_parentRegion = parentRegion;
		_inStart = inStart;
		_inEnd = inEnd;
		_loop = theLoop;
		_unroll = theUnroll;
		_unrollLoops = theUnrollLoops;
	}

	private string BuildAttributes()
	{
		if (_unrollLoops > 0)
		{
			return "[unroll(" + _unrollLoops + ")]";
		}
		if (_unroll)
		{
			return "[unroll]";
		}
		if (_loop)
		{
			return "[loop]";
		}
		return "";
	}

	protected override void BuildSource(StringBuilder theSourceBuilder, HashSet<AbstractShaderNode> theHashes, string thePrepend)
	{
		if (theHashes.Add(this))
		{
			if (ShaderNodesUtil.DebugShaderGeneration)
			{
				Console.WriteLine(thePrepend + ID);
			}
			theSourceBuilder.Append(ShaderNodesUtil.Evaluate("\r\n        ${attributes}\r\n        for(int ${index} = ${start}; ${index} < ${end};${index}++){", new Dictionary<string, string>
			{
				{ "start", _inStart.ID },
				{ "end", _inEnd.ID },
				{ "index", _parentRegion.IndexName },
				{
					"attributes",
					BuildAttributes()
				}
			}));
			theSourceBuilder.AppendLine();
			StringBuilder stringBuilder = new StringBuilder();
			BuildChildrenSource(stringBuilder, theHashes, thePrepend);
			string theCode = stringBuilder.ToString();
			theCode = ShaderNodesUtil.IndentCode(theCode);
			theSourceBuilder.Append(theCode);
			string sourceCode = SourceCode;
			if (!string.IsNullOrWhiteSpace(sourceCode.Trim()) && theHashes.Add(this))
			{
				theSourceBuilder.Append("        " + sourceCode + Environment.NewLine);
			}
			theSourceBuilder.Append("\r\n        }");
			theSourceBuilder.AppendLine();
		}
	}
}
