using Fuse.compute;

namespace Fuse.ShaderFX;

public class TransformPositionStage : AbstractStage
{
	private const string ShaderSource = "\r\n\tstage override void TransformPosition()\r\n\t{\r\n        ${sourceTransformPosition}\r\n\t}";

	public TransformPositionStage(ShaderNode<GpuVoid> theShaderNode)
		: base("TransformPosition", theShaderNode)
	{
	}

	public override string Source()
	{
		return "\r\n\tstage override void TransformPosition()\r\n\t{\r\n        ${sourceTransformPosition}\r\n\t}";
	}
}
