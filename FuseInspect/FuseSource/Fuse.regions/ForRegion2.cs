using VL.Core;
using VL.Core.Import;

namespace Fuse.regions;

[ProcessNode]
public class ForRegion2 : FuseRegionBase<IForRegionInlay>
{
	private ShaderNode<int> _end;

	private readonly NodeContext _nodeContext;

	public ForRegion2(NodeContext nodeContext)
	{
		_nodeContext = nodeContext;
	}

	public void Update(ShaderNode<int> end)
	{
		if (end != _end)
		{
			_end = end;
		}
	}
}
