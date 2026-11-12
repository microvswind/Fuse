using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.compute;

public class DefaultIndexProvider : IIndexProvider
{
	public void Index(NodeContext nodeContext, out ShaderNode<Int3> theReadIndex, out ShaderNode<Int3> theWriteIndex)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		theReadIndex = (theWriteIndex = new ConstantValue<Int3>(new Int3(0, 0, 0)));
	}
}
