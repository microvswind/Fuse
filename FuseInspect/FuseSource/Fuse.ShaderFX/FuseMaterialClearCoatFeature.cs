using Fuse.compute;
using Stride.Rendering;
using Stride.Rendering.Materials;

namespace Fuse.ShaderFX;

public class FuseMaterialClearCoatFeature : MaterialClearCoatFeature
{
	public Material OriginalMaterial { get; }

	public ShaderNode<GpuVoid> MaterialShading { get; }

	public FuseMaterialClearCoatFeature(Material theOriginalMaterial, ShaderNode<GpuVoid> theShaderNode, Mesh theMesh)
	{
		MaterialShading = theShaderNode;
		OriginalMaterial = theOriginalMaterial;
		((MaterialFeature)this).Enabled = false;
	}
}
