using Fuse.compute;

namespace Fuse.ShaderFX;

public class GenerateNormal_VSStage : AbstractStage
{
	private const string ShaderSource = "\r\n\tstage override void GenerateNormal_VS() \r\n\t{\r\n        ${sourceGenerateNormal_VS}\r\n\t}";

	public GenerateNormal_VSStage(ShaderNode<GpuVoid> theShaderNode)
		: base("GenerateNormal_VS", theShaderNode)
	{
	}

	public override string Source()
	{
		return "\r\n\tstage override void GenerateNormal_VS() \r\n\t{\r\n        ${sourceGenerateNormal_VS}\r\n\t}";
	}
}
