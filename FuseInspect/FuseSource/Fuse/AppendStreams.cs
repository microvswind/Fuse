using VL.Core;

namespace Fuse;

public class AppendStreams : SimpleKeyword
{
	public AppendStreams(NodeContext nodeContext)
		: base(nodeContext, "outputStream.Append(streams);")
	{
	}
}
