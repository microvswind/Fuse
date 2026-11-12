using VL.Core;

namespace Fuse;

public class RestartStrip : SimpleKeyword
{
	public RestartStrip(NodeContext nodeContext)
		: base(nodeContext, "outputStream.RestartStrip();")
	{
	}
}
