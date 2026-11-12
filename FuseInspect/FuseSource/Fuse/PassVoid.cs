using Fuse.compute;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class PassVoid : PassThroughNode<GpuVoid>, IComputeVoid, IComputeNode
{
	public PassVoid(NodeContext nodeContext, string theName, AbstractShaderNode theValue)
		: base(nodeContext, theValue, theName)
	{
	}
}
