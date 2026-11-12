using System.Collections.Generic;

namespace Fuse.ShaderFX;

public class ShadingStage : AbstractStage
{
	private const string ShaderSource = "\r\n\tstage override float4 Shading()\r\n\t{\r\n        ${sourceShading}\r\n\t\treturn ${resultShading};\r\n\t}";

	public ShadingStage(AbstractShaderNode theShaderNode)
		: base("Shading", theShaderNode)
	{
	}

	public override void AppendInputs(Dictionary<string, string> theTemplateMap)
	{
		theTemplateMap["resultShading"] = StageNode.ID + ";";
	}

	public override string Source()
	{
		return "\r\n\tstage override float4 Shading()\r\n\t{\r\n        ${sourceShading}\r\n\t\treturn ${resultShading};\r\n\t}";
	}
}
