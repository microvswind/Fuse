using System.Collections.Generic;
using Fuse.function;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.sdf;

public class ShadeSDFInvoke : Invoke<Vector4>
{
	public ShadeSDFInvoke(NodeContext nodeContext, ShadeSDF theDelegate, ShaderNode<MarchSurface> surface, ShaderNode<MarchRay> ray, string theId = "Invoke", ShaderNode<Vector4> theDefault = null)
		: base(nodeContext, (Delegate<Vector4>)null, (IEnumerable<AbstractShaderNode>)new List<AbstractShaderNode> { surface, ray }, theId, theDefault)
	{
	}
}
