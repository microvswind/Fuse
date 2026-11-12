using System.Collections.Generic;
using Stride.Rendering.Materials;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse.ShaderFX;

public class ToComputeFx<T> : AbstractToShaderFX<T>, IComputeVoid, IComputeNode
{
	private const string ShaderSource = "\r\nshader ${shaderID} : ComputeVoid, ComputeShaderBase${mixins}\r\n{\r\n\r\n    rgroup ResourceGroup{\r\n${groupDeclarations}\r\n    }\r\n\r\n    cbuffer PerDispatch{\r\n${declarations}\r\n    }\r\n\r\n${constantArrays}\r\n\r\n${structs}\r\n\r\n${functions}\r\n\r\n${compositions}\r\n\r\n${stageFX}\r\n};";

	public ToComputeFx(ShaderNode<T> theCompute)
		: base(new List<AbstractStage>
		{
			new ComputeStage<T>(theCompute)
		}, new Dictionary<string, string>(), true, "\r\nshader ${shaderID} : ComputeVoid, ComputeShaderBase${mixins}\r\n{\r\n\r\n    rgroup ResourceGroup{\r\n${groupDeclarations}\r\n    }\r\n\r\n    cbuffer PerDispatch{\r\n${declarations}\r\n    }\r\n\r\n${constantArrays}\r\n\r\n${structs}\r\n\r\n${functions}\r\n\r\n${compositions}\r\n\r\n${stageFX}\r\n};")
	{
	}
}
