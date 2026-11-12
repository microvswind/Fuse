using Fuse.compute;

namespace Fuse.ShaderFX;

public class PreTransformPositionStage : AbstractStage
{
	private const string ShaderSource = "\r\n\tstage override void PreTransformPosition() \r\n\t{\r\n        ${sourcePreTransformPosition}\r\n\t}";

	public PreTransformPositionStage(ShaderNode<GpuVoid> theShaderNode)
		: base("PreTransformPosition", theShaderNode)
	{
	}

	public override string Source()
	{
		return "\r\n\tstage override void PreTransformPosition() \r\n\t{\r\n        ${sourcePreTransformPosition}\r\n\t}";
	}
}
