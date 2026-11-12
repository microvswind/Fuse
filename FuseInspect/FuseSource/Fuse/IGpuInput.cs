using Stride.Rendering;
using Stride.Rendering.Materials;

namespace Fuse;

public interface IGpuInput : IComputeNode
{
	void SetParameterValue(ParameterCollection theCollection);

	void OnUpdateName();
}
