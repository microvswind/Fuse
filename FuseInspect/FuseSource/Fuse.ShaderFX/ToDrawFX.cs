using System.Collections.Generic;
using Fuse.compute;

namespace Fuse.ShaderFX;

public class ToDrawFX : AbstractToShaderFX<GpuVoid>
{
	private const string ShaderSource = "shader ${shaderID} : ShaderBase${mixins}\r\n{\r\n    rgroup Inputs{\r\n${groupDeclarations}\r\n    }\r\n\r\n    cbuffer Inputs{\r\n${declarations}\r\n    }\r\n\r\n${constantArrays}\r\n\r\n${structs}\r\n\r\n${streams}\r\n\r\n${functions}\r\n\r\n${compositions}\r\n\r\n${stageVS}\r\n\r\n${stageGS}\r\n\r\n${stagePS}\r\n\r\n};";

	public ToDrawFX(ShaderNode<GpuVoid> theVertexNode, GeometryStage theGeometryStage, ShaderNode<GpuVoid> thePixelNode, string theTemplate = "shader ${shaderID} : ShaderBase${mixins}\r\n{\r\n    rgroup Inputs{\r\n${groupDeclarations}\r\n    }\r\n\r\n    cbuffer Inputs{\r\n${declarations}\r\n    }\r\n\r\n${constantArrays}\r\n\r\n${structs}\r\n\r\n${streams}\r\n\r\n${functions}\r\n\r\n${compositions}\r\n\r\n${stageVS}\r\n\r\n${stageGS}\r\n\r\n${stagePS}\r\n\r\n};")
		: base(new List<AbstractStage>
		{
			new VertexStage(theVertexNode),
			theGeometryStage,
			new PixelStage(thePixelNode)
		}, new Dictionary<string, string>(), false, theTemplate)
	{
	}
}
