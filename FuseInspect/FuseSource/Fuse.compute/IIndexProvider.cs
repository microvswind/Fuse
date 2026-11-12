using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.compute;

public interface IIndexProvider
{
	void Index(NodeContext nodeContext, out ShaderNode<Int3> theReadIndex, out ShaderNode<Int3> theWriteIndex);
}
