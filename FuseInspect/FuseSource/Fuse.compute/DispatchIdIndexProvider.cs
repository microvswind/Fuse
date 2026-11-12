using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.compute;

public class DispatchIdIndexProvider : IIndexProvider
{
	public void Index(NodeContext nodeContext, out ShaderNode<Int3> theReadIndex, out ShaderNode<Int3> theWriteIndex)
	{
		theReadIndex = (theWriteIndex = new Semantic<Int3>(nodeContext, "DispatchThreadId"));
	}
}
