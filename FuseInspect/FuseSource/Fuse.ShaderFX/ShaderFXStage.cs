using System.Collections.Generic;

namespace Fuse.ShaderFX;

public class ShaderFXStage<T> : AbstractStage
{
	private const string ShaderSource = "\r\n    override ${resultType} Compute()\r\n    {\r\n${sourceFX}\r\n        ${resultFX}\r\n    }";

	public ShaderFXStage(ShaderNode<T> theShaderNode)
		: base("FX", theShaderNode)
	{
	}

	public override void AppendInputs(Dictionary<string, string> theTemplateMap)
	{
		if (TypeHelpers.GetGpuType<T>().Equals("void"))
		{
			theTemplateMap["resultFX"] = "";
		}
		else
		{
			theTemplateMap["resultFX"] = "return " + StageNode.ID + ";";
		}
	}

	public override string Source()
	{
		return "\r\n    override ${resultType} Compute()\r\n    {\r\n${sourceFX}\r\n        ${resultFX}\r\n    }";
	}
}
