using VL.Core;

namespace Fuse;

public class DiscardVoid : SimpleKeyword
{
	public DiscardVoid(NodeContext nodeContext)
		: base(nodeContext, "discard;")
	{
	}
}
