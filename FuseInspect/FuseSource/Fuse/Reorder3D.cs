using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Reorder3D : GetMember<Vector3, Vector3>
{
	public Reorder3D(NodeContext nodeContext, ShaderNode<Vector3> theInput, Reorder3DMode theMember, ShaderNode<Vector3> theDefault = null)
		: base(nodeContext, theInput, theMember.ToString().ToLower(), theDefault)
	{
	}
}
