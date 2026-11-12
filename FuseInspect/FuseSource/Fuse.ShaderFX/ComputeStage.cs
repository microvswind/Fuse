namespace Fuse.ShaderFX;

public class ComputeStage<T> : AbstractStage
{
	private const string ShaderSource = "\r\n    override void Compute()\r\n    {\r\n${sourceFX}\r\n    }";

	public ComputeStage(ShaderNode<T> theShaderNode)
		: base("FX", theShaderNode)
	{
	}

	public override string Source()
	{
		return "\r\n    override void Compute()\r\n    {\r\n${sourceFX}\r\n    }";
	}
}
