using Fuse.compute;
using Stride.Core.Mathematics;

namespace Fuse.ComputeSystem;

public interface IAttribute
{
	string Name { get; }

	AttributeType AttributeType { get; set; }

	AbstractShaderNode ShaderNode { get; }

	AbstractShaderNode InputAbstract { get; set; }

	ShaderNode<GpuVoid> WriteCall { get; set; }

	AbstractShaderNode ReadCall { get; set; }

	Int3 Resolution { get; }

	bool IsOverridden { get; }

	void Sync(IAttribute theAttribute);
}
