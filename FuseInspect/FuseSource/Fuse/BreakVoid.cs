using VL.Core;

namespace Fuse;

public class BreakVoid : SimpleKeyword
{
	public BreakVoid(NodeContext nodeContext)
		: base(nodeContext, "break;")
	{
	}
}
