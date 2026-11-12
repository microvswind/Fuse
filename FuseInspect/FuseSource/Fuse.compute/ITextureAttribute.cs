using Fuse.ComputeSystem;
using Stride.Core.Mathematics;

namespace Fuse.compute;

public interface ITextureAttribute : IAttribute
{
	bool DoubleBuffered { get; }

	ShaderNode<Int3> Index { get; set; }

	TextureInput TextureInput { get; set; }
}
