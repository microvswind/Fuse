using System.Collections.Generic;
using VL.Core;
using VL.Core.PublicAPI;

namespace Fuse.regions;

public class WhileRegion : AbstractRegion
{
	public WhileRegion(NodeContext nodeContext, ShaderNode<bool> inCheck, IEnumerable<AbstractShaderNode> theInputs, IEnumerable<AbstractShaderNode> theOutputs, IEnumerable<AbstractShaderNode> theCrossLinks, IEnumerable<BorderControlPointDescription> theDescriptions)
		: base(nodeContext, "WhileRegion")
	{
		SetupRegion((NodeSubContextFactory subContextFactory, List<AbstractShaderNode> myOutputs) => new WhileGroup(subContextFactory.NextSubContext(), inCheck, myOutputs), (List<AbstractShaderNode> theInputList) =>
		{
			theInputList.Add(inCheck);
		}, theInputs, theOutputs, theCrossLinks, theDescriptions);
	}
}
