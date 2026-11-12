using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse.Function;

public class Delegate1In2Out<TIn, TOut1, TOut2> : DelegateStateful<Delegate<TOut1>>
{
	public Delegate1In2Out(NodeContext theNodeContext)
		: base(theNodeContext)
	{
	}

	public void Update(Delegate1In2OutCreate create, Delegate1In2OutUpdate<TIn, TOut1, TOut2> update)
	{
		if (State == null)
		{
			create(out State);
		}
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(NodeContext);
		FunctionParameter<TIn> functionParameter = new FunctionParameter<TIn>(nodeSubContextFactory.NextSubContext(), null);
		update(State, out State, functionParameter, out var Output, out var Output2);
		FunctionParameter<TOut2> functionParameter2 = new FunctionParameter<TOut2>(nodeSubContextFactory.NextSubContext(), null, InputModifier.Out, 1);
		AssignValue<TOut2> item = new AssignValue<TOut2>(nodeSubContextFactory.NextSubContext(), functionParameter2, Output2);
		Do2<TOut1> theDelegate = new Do2<TOut1>(nodeSubContextFactory.NextSubContext(), Output, new List<AbstractShaderNode> { item });
		Delegate = new Delegate<TOut1>(nodeSubContextFactory.NextSubContext(), theDelegate, new List<IFunctionParameter> { functionParameter, functionParameter2 });
	}
}
