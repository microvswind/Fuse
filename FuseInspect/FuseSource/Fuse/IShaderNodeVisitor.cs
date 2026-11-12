namespace Fuse;

public interface IShaderNodeVisitor
{
	void Visit(AbstractShaderNode node, int recursionLevel);
}
