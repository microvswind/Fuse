using VL.Core;

namespace Fuse;

public class EmptyVoid : SimpleKeyword
{
	public EmptyVoid(NodeContext nodeContext)
		: base(nodeContext, "")
	{
	}
}
