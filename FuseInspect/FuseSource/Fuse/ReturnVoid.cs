using VL.Core;

namespace Fuse;

public class ReturnVoid : SimpleKeyword
{
	public ReturnVoid(NodeContext nodeContext)
		: base(nodeContext, "return;")
	{
	}
}
