using System.Collections.Generic;
using Fuse.compute;

namespace Fuse.ShaderFX;

public class VertexStage : AbstractStage
{
	private const string ShaderSource = "\r\n    stage override void VSMain()\r\n    {\r\n${sourceVS}\r\n    }";

	public VertexStage(ShaderNode<GpuVoid> theShaderNode)
		: base("VS", theShaderNode)
	{
	}

	public override void AppendInputs(Dictionary<string, string> theTemplateMap)
	{
	}

	public override string Source()
	{
		return "\r\n    stage override void VSMain()\r\n    {\r\n${sourceVS}\r\n    }";
	}
}
