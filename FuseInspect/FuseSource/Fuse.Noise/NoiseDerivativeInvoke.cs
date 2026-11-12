using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse.Noise;

public class NoiseDerivativeInvoke<T> : Invoke<float>
{
	public NoiseDerivativeInvoke(NodeContext nodeContext, NoiseDerivative<T> theDelegate, ShaderNode<T> domain)
		: base(nodeContext, theDelegate?.Delegate, (IEnumerable<AbstractShaderNode>)new List<AbstractShaderNode> { domain }, "Invoke", (ShaderNode<float>)null)
	{
	}
}
