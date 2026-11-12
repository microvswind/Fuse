using System;
using System.Collections.Generic;
using System.Text;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.regions;

public class Grid3DGroup : Group
{
	private readonly ShaderNode<Int3> _cell;

	private readonly BufferInput<Int2> _elementIndices;

	private readonly ShaderNode<Int3> _gridDim;

	private readonly BufferInput<Int2> _gridIndices;

	private readonly Grid3DRegion _parentRegion;

	public Grid3DGroup(NodeContext nodeContext, Grid3DRegion parentRegion, ShaderNode<Int3> cell, ShaderNode<Int3> gridDim, BufferInput<Int2> gridIndices, BufferInput<Int2> elementIndices, IEnumerable<AbstractShaderNode> theInputs)
		: base(nodeContext, theInputs, "Grid3DGroup")
	{
		Name = "Grid3DGroup";
		_parentRegion = parentRegion;
		_cell = cell;
		_gridDim = gridDim;
		_gridIndices = gridIndices;
		_elementIndices = elementIndices;
	}

	protected override void BuildSource(StringBuilder theSourceBuilder, HashSet<AbstractShaderNode> theHashes, string thePrepend)
	{
		if (theHashes.Add(this))
		{
			if (ShaderNodesUtil.DebugShaderGeneration)
			{
				Console.WriteLine(thePrepend + ID);
			}
			theSourceBuilder.Append(ShaderNodesUtil.Evaluate("\r\n        for (int Z = max(${cell}.z - 1, 0); Z <= min(${cell}.z + 1, ${dim}.z - 1); Z++) \r\n\t    for (int Y = max(${cell}.y - 1, 0); Y <= min(${cell}.y + 1, ${dim}.y - 1); Y++) \r\n\t    for (int X = max(${cell}.x - 1, 0); X <= min(${cell}.x + 1, ${dim}.x - 1); X++){\r\n            \r\n            const unsigned int g_cell = X + Y * ${dim}.x + Z * ${dim}.x * ${dim}.y;\t// Calculate Neighbor (or own) Cell ID\r\n            uint2 g_start_end = ${gridIndices}[g_cell];\r\n            \r\n            for (unsigned int n_id = g_start_end.x; n_id < g_start_end.y; n_id++) {\r\n\t\t\t\tint ${index} = ${elementIndices}[n_id].y;\r\n\r\n                if(((int)streams.DispatchThreadId.x) == ${index})continue;\r\n                ", new Dictionary<string, string>
			{
				{ "cell", _cell.ID },
				{ "dim", _gridDim.ID },
				{ "gridIndices", _gridIndices.ID },
				{ "elementIndices", _elementIndices.ID },
				{ "index", _parentRegion.IndexName }
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
			theSourceBuilder.Append("\r\n            }\r\n        }");
			theSourceBuilder.AppendLine();
		}
	}
}
