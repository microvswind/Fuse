using Stride.Rendering.Materials;

namespace Fuse;

public class CheckContextVisitor : IShaderNodeVisitor
{
	private readonly ShaderGeneratorContext _context;

	public CheckContextVisitor(ShaderGeneratorContext theContext)
	{
		_context = theContext;
	}

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		node.OnPassContext(_context);
	}
}
