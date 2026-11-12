using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class ComputeNode<T> : ShaderNode<T>, IComputeVoid, IComputeNode
{
	public ComputeNode(NodeContext nodeContext, string theId, ShaderNode<T> theDefault = null)
		: base(nodeContext, theId, theDefault, true)
	{
	}
}
