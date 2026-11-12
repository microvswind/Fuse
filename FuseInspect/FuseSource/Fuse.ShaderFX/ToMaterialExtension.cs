using System.Collections.Generic;
using Fuse.compute;
using Stride.Core.Mathematics;

namespace Fuse.ShaderFX;

public class ToMaterialExtension : AbstractToShaderFX<GpuVoid>
{
	private const string ShaderSource = "\r\nshader ${shaderID} : MaterialExtension${mixins}\r\n{\r\n    rgroup PerMaterial{\r\n${groupDeclarations}\r\n    }\r\n\r\n    cbuffer PerMaterial{\r\n${declarations}\r\n    }\r\n\r\n${constantArrays}\r\n\r\n${structs}\r\n\r\n${streams}\r\n\r\n${functions}\r\n\r\n${compositions}\r\n\r\n${stageGenerateNormal_VS}\r\n\r\n${stagePreTransformPosition}\r\n\r\n${stageTransformPosition}\r\n\r\n${stagePostTransformPosition}\r\n\r\n${stageGS}\r\n\r\n${stageShading}\r\n\r\n};";

	public ToMaterialExtension(ShaderNode<GpuVoid> theGenerateNormal_VS, ShaderNode<GpuVoid> thePreTransform, ShaderNode<GpuVoid> theTransform, ShaderNode<GpuVoid> thePostTransform, GeometryStage theGeometryStage, ShaderNode<Vector4> theShading)
		: base(new List<AbstractStage>
		{
			(theGenerateNormal_VS == null) ? null : new GenerateNormal_VSStage(theGenerateNormal_VS),
			(thePreTransform == null) ? null : new PreTransformPositionStage(thePreTransform),
			(theTransform == null) ? null : new TransformPositionStage(theTransform),
			(thePostTransform == null) ? null : new PostTransformPositionStage(thePostTransform),
			theGeometryStage,
			(theShading == null) ? null : new ShadingStage(theShading)
		}, new Dictionary<string, string>(), false, "\r\nshader ${shaderID} : MaterialExtension${mixins}\r\n{\r\n    rgroup PerMaterial{\r\n${groupDeclarations}\r\n    }\r\n\r\n    cbuffer PerMaterial{\r\n${declarations}\r\n    }\r\n\r\n${constantArrays}\r\n\r\n${structs}\r\n\r\n${streams}\r\n\r\n${functions}\r\n\r\n${compositions}\r\n\r\n${stageGenerateNormal_VS}\r\n\r\n${stagePreTransformPosition}\r\n\r\n${stageTransformPosition}\r\n\r\n${stagePostTransformPosition}\r\n\r\n${stageGS}\r\n\r\n${stageShading}\r\n\r\n};")
	{
	}
}
