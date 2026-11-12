using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;
using VL.Core.PublicAPI;

namespace Fuse.regions;

public class Grid3DRegion : AbstractRegion
{
	public string IndexName { get; }

	public Grid3DRegion(NodeContext nodeContext, ShaderNode<Int3> cell, ShaderNode<Int3> gridDim, BufferInput<Int2> gridIndices, BufferInput<Int2> elementIndices, IEnumerable<AbstractShaderNode> theInputs, IEnumerable<AbstractShaderNode> theOutputs, IEnumerable<AbstractShaderNode> theCrossLinks, IEnumerable<BorderControlPointDescription> theDescriptions)
		: base(nodeContext, "forRegion")
	{
		Grid3DRegion parentRegion = this;
		IndexName = "index_" + HashCode;
		SetupRegion((NodeSubContextFactory subContextFactory, List<AbstractShaderNode> myOutputs) => new Grid3DGroup(subContextFactory.NextSubContext(), parentRegion, cell, gridDim, gridIndices, elementIndices, myOutputs), (List<AbstractShaderNode> theInputList) =>
		{
			theInputList.Add(cell);
			theInputList.Add(gridDim);
			theInputList.Add(gridIndices);
			theInputList.Add(elementIndices);
		}, theInputs, theOutputs, theCrossLinks, theDescriptions);
	}
}
