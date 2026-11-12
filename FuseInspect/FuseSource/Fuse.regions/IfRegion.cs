using System.Collections.Generic;
using VL.Core;
using VL.Core.PublicAPI;

namespace Fuse.regions;

public class IfRegion : AbstractRegion
{
	public IfRegion(NodeContext nodeContext, ShaderNode<bool> inCheck, IEnumerable<AbstractShaderNode> theInputs, IEnumerable<AbstractShaderNode> theOutputs, IEnumerable<AbstractShaderNode> theCrossLinks, IEnumerable<BorderControlPointDescription> theDescriptions)
		: base(nodeContext, "ifRegion")
	{
		SetupRegion((NodeSubContextFactory subContextFactory, List<AbstractShaderNode> myOutputs) => new IfGroup(subContextFactory.NextSubContext(), inCheck, myOutputs), (List<AbstractShaderNode> theInputList) =>
		{
			theInputList.Add(inCheck);
		}, theInputs, theOutputs, theCrossLinks, theDescriptions);
	}
}
