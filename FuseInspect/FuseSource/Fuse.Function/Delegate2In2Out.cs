using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse.Function;

public class Delegate2In2Out<TIn1, TIn2, TOut1, TOut2> : DelegateStateful<Delegate<TOut1>>
{
	public Delegate2In2Out(NodeContext theNodeContext)
		: base(theNodeContext)
	{
	}

	public void Update(Delegate2In2OutCreate create, Delegate2In2OutUpdate<TIn1, TIn2, TOut1, TOut2> update)
	{
		if (State == null)
		{
			create(out State);
		}
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(NodeContext);
		FunctionParameter<TIn1> functionParameter = new FunctionParameter<TIn1>(nodeSubContextFactory.NextSubContext(), null);
		FunctionParameter<TIn2> functionParameter2 = new FunctionParameter<TIn2>(nodeSubContextFactory.NextSubContext(), null);
		update(State, out State, functionParameter, functionParameter2, out var output, out var output2);
		FunctionParameter<TOut2> functionParameter3 = new FunctionParameter<TOut2>(nodeSubContextFactory.NextSubContext(), null, InputModifier.Out, 1);
		AssignValue<TOut2> item = new AssignValue<TOut2>(nodeSubContextFactory.NextSubContext(), functionParameter3, output2);
		Do2<TOut1> theDelegate = new Do2<TOut1>(nodeSubContextFactory.NextSubContext(), output, new List<AbstractShaderNode> { item });
		Delegate = new Delegate<TOut1>(nodeSubContextFactory.NextSubContext(), theDelegate, new List<IFunctionParameter> { functionParameter, functionParameter2, functionParameter3 });
	}
}
