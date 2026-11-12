using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using VL.Core;

namespace Fuse.regions;

public class IndexNode : ShaderNode<int>
{
	[field: ThreadStatic]
	private static NodeContext Current { get; set; }

	public override string ID { get; }

	public IndexNode(NodeContext nodeContext)
		: base(nodeContext, "index", (ShaderNode<int>)null, true)
	{
		ID = ((Current == null) ? "0" : $"index_{ShaderNodesUtil.GetHashCode(Current)}");
		SetInputs(new List<AbstractShaderNode>());
	}

	public static IDisposable MakeCurrent(NodeContext context)
	{
		NodeContext previous = Current;
		Current = context;
		return Disposable.Create((Action)(() =>
		{
			Current = previous;
		}));
	}

	protected override string SourceTemplate()
	{
		return "";
	}
}
