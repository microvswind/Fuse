using System.Collections.Generic;
using Fuse.compute;

namespace Fuse.ShaderFX;

public class PixelStage : AbstractStage
{
	private const string ShaderSource = "\r\n    stage override void PSMain()\r\n    {\r\n${sourcePS}\r\n    }";

	public PixelStage(ShaderNode<GpuVoid> theShaderNode)
		: base("PS", theShaderNode)
	{
	}

	public override void AppendInputs(Dictionary<string, string> theTemplateMap)
	{
	}

	public override string Source()
	{
		return "\r\n    stage override void PSMain()\r\n    {\r\n${sourcePS}\r\n    }";
	}
}
