using Fuse.compute;

namespace Fuse.ShaderFX;

public class PostTransformPositionStage : AbstractStage
{
	private const string ShaderSource = "\r\n\tstage override void PostTransformPosition()\r\n\t{\r\n        ${sourcePostTransformPosition}\r\n\t}";

	public PostTransformPositionStage(ShaderNode<GpuVoid> theShaderNode)
		: base("PostTransformPosition", theShaderNode)
	{
	}

	public override string Source()
	{
		return "\r\n\tstage override void PostTransformPosition()\r\n\t{\r\n        ${sourcePostTransformPosition}\r\n\t}";
	}
}
