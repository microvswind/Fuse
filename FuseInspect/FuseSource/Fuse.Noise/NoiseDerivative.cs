using System;
using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse.Noise;

public class NoiseDerivative<T> : IDisposable
{
	private readonly NodeContext _nodeContext;

	private object _state;

	public Delegate<float> Delegate { get; private set; }

	public NoiseDerivative(NodeContext theNodeContext)
	{
		_nodeContext = theNodeContext;
	}

	public void Dispose()
	{
		if (_state is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}

	public void Update(NoiseDerivativeCreate create, NoiseDerivativeUpdate<T> update)
	{
		if (_state == null)
		{
			create(out _state);
		}
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(_nodeContext);
		FunctionParameter<T> functionParameter = new FunctionParameter<T>(nodeSubContextFactory.NextSubContext(), null);
		update(_state, out _state, functionParameter, out var Value, out var Derivative);
		FunctionParameter<T> functionParameter2 = new FunctionParameter<T>(nodeSubContextFactory.NextSubContext(), null, InputModifier.Out, 1);
		AssignValue<T> item = new AssignValue<T>(nodeSubContextFactory.NextSubContext(), functionParameter2, Derivative);
		Do2<float> theDelegate = new Do2<float>(nodeSubContextFactory.NextSubContext(), Value, new List<AbstractShaderNode> { item });
		Delegate = new Delegate<float>(nodeSubContextFactory.NextSubContext(), theDelegate, new List<IFunctionParameter> { functionParameter, functionParameter2 });
	}
}
