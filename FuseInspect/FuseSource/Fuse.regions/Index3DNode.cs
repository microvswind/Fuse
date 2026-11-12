using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using VL.Core;

namespace Fuse.regions;

public class Index3DNode : ShaderNode<uint>
{
	[field: ThreadStatic]
	public static NodeContext Current { get; private set; }

	public override string ID { get; }

	public Index3DNode(NodeContext nodeContext)
		: base(nodeContext, "index", (ShaderNode<uint>)null, true)
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
