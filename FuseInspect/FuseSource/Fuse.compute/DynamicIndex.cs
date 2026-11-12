using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.compute;

public class DynamicIndex : PassThroughNode<Int3>
{
	private readonly bool _useReadIndex;

	private IIndexProvider _indexProvider;

	public IIndexProvider IndexProvider
	{
		get
		{
			return _indexProvider;
		}
		set
		{
			_indexProvider = value;
			_indexProvider.Index(NodeContext, out var theReadIndex, out var theWriteIndex);
			Input = (_useReadIndex ? theReadIndex : theWriteIndex);
			SetInputs(new List<AbstractShaderNode> { Input });
		}
	}

	public DynamicIndex(NodeContext nodeContext, IIndexProvider theIndexProvider, bool useReadIndex = false)
		: base(nodeContext, "Index")
	{
		IndexProvider = theIndexProvider ?? new DefaultIndexProvider();
		_useReadIndex = useReadIndex;
		AddProperty("DynamicIndex", this);
		IndexProvider.Index(NodeContext, out var theReadIndex, out var theWriteIndex);
		Input = (useReadIndex ? theReadIndex : theWriteIndex);
		SetInputs(new List<AbstractShaderNode> { Input });
	}
}
