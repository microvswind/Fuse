using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse.Function;

public class Delegate0In0Out<TOut> : DelegateStateful<Delegate<TOut>>
{
	public Delegate0In0Out(NodeContext theNodeContext)
		: base(theNodeContext)
	{
	}

	public void Update(Delegate0In0OutCreate create, Delegate0In0OutUpdate<TOut> update)
	{
		if (State == null)
		{
			create(out State);
		}
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(NodeContext);
		update(State, out State, out var Output);
		Delegate = new Delegate<TOut>(nodeSubContextFactory.NextSubContext(), Output, new List<IFunctionParameter>());
	}
}
