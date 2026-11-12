using VL.Core;

namespace Fuse;

public class ContinueVoid : SimpleKeyword
{
	public ContinueVoid(NodeContext nodeContext)
		: base(nodeContext, "continue;")
	{
	}
}
