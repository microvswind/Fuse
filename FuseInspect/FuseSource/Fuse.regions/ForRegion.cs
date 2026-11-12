using System.Collections.Generic;
using VL.Core;
using VL.Core.PublicAPI;

namespace Fuse.regions;

public class ForRegion : AbstractRegion
{
	public string IndexName { get; }

	public ForRegion(NodeContext nodeContext, ShaderNode<int> inStart, ShaderNode<int> inEnd, bool theLoop, bool theUnroll, int theUnrollLoops, IEnumerable<AbstractShaderNode> theInputs, IEnumerable<AbstractShaderNode> theOutputs, IEnumerable<AbstractShaderNode> theCrossLinks, IEnumerable<BorderControlPointDescription> theDescriptions)
		: base(nodeContext, "forRegion")
	{
		ForRegion parentRegion = this;
		IndexName = "index_" + HashCode;
		SetupRegion((NodeSubContextFactory subContextFactory, List<AbstractShaderNode> myOutputs) => new ForGroup(subContextFactory.NextSubContext(), parentRegion, inStart, inEnd, theLoop, theUnroll, theUnrollLoops, myOutputs), (List<AbstractShaderNode> theInputList) =>
		{
			theInputList.Add(inStart);
			theInputList.Add(inEnd);
		}, theInputs, theOutputs, theCrossLinks, theDescriptions);
	}
}
