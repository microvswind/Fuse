using VL.Core;

namespace Fuse;

public class NodeSubContextFactory
{
	private readonly NodeContext _context;

	private readonly int _startIndex;

	private int _subContextId;

	public NodeSubContextFactory(NodeContext nodeContext, int theStartIndex = 0)
	{
		_context = nodeContext;
		_startIndex = theStartIndex;
	}

	public void Reset()
	{
		_subContextId = _startIndex;
	}

	public NodeContext NextSubContext()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (_context == null)
		{
			return NodeContext.CurrentRoot;
		}
		NodeContext result = _context.CreateSubContext(new UniqueId("Fuse", _subContextId.ToString(), 0u));
		_subContextId++;
		return result;
	}
}
