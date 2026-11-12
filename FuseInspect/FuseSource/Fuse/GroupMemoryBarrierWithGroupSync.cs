using VL.Core;

namespace Fuse;

public class GroupMemoryBarrierWithGroupSync : SimpleKeyword
{
	public GroupMemoryBarrierWithGroupSync(NodeContext nodeContext)
		: base(nodeContext, "GroupMemoryBarrierWithGroupSync();")
	{
	}
}
