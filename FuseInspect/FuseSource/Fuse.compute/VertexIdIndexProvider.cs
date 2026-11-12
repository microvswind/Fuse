using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.compute;

public class VertexIdIndexProvider : IIndexProvider
{
	public void Index(NodeContext nodeContext, out ShaderNode<Int3> theReadIndex, out ShaderNode<Int3> theWriteIndex)
	{
		Semantic<int> x = new Semantic<int>(NodeContext.CurrentRoot, "VertexId");
		Int3Join int3Join = new Int3Join(nodeContext, x, new ConstantValue<int>(0), new ConstantValue<int>(0));
		theReadIndex = (theWriteIndex = int3Join);
	}
}
