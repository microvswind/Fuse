using System.Collections.Generic;

namespace Fuse.ShaderFX;

public abstract class AbstractStage
{
	public readonly string Key;

	public AbstractShaderNode StageNode;

	public int Ticket { get; private set; }

	public AbstractStage(string theKey, AbstractShaderNode theShaderNode)
	{
		Key = theKey;
		StageNode = theShaderNode;
		Ticket = 0;
	}

	public virtual void AppendInputs(Dictionary<string, string> theTemplateMap)
	{
	}

	public abstract string Source();

	public void ChangeGraph(AbstractShaderNode theNode)
	{
		Ticket++;
	}
}
