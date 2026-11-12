using Fuse.ComputeSystem;
using Stride.Graphics;

namespace Fuse.compute;

public interface IStructureBufferAttribute : IAttribute
{
	Buffer Buffer { get; set; }

	int ArrayCount { get; }
}
