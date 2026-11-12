using System.Collections.Generic;

namespace Fuse.ShaderFX;

public class ToShaderFX<T> : AbstractToShaderFX<T>
{
	private const string ShaderSource = "\r\nshader ${shaderID} : Compute${shaderType}${mixins}\r\n{\r\n    rgroup PerMaterial{\r\n${groupDeclarations}\r\n    }\r\n\r\n    cbuffer PerMaterial{\r\n${declarations}\r\n    }\r\n\r\n${constantArrays}\r\n\r\n${structs}\r\n\r\n${streams}\r\n\r\n${functions}\r\n\r\n${compositions}\r\n\r\n${stageFX}\r\n\r\n};";

	public ToShaderFX(ShaderNode<T> theCompute, bool theIsCompute = false)
		: base(new List<AbstractStage>
		{
			new ShaderFXStage<T>(theCompute)
		}, new Dictionary<string, string>(), theIsCompute, "\r\nshader ${shaderID} : Compute${shaderType}${mixins}\r\n{\r\n    rgroup PerMaterial{\r\n${groupDeclarations}\r\n    }\r\n\r\n    cbuffer PerMaterial{\r\n${declarations}\r\n    }\r\n\r\n${constantArrays}\r\n\r\n${structs}\r\n\r\n${streams}\r\n\r\n${functions}\r\n\r\n${compositions}\r\n\r\n${stageFX}\r\n\r\n};")
	{
	}
}
