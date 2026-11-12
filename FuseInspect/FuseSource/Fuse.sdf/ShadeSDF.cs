using System;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.sdf;

public class ShadeSDF : IDisposable
{
	private object State;

	private ShadeSDFUpdate UpdateDelegate;

	public ShadeSDF(NodeContext theNodeContext)
	{
	}

	public void Dispose()
	{
		if (State is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}

	public void Update(ShadeSDFCreate create, ShadeSDFUpdate update)
	{
		if (State == null)
		{
			create(out State);
		}
		UpdateDelegate = update;
	}

	public void Invoke(ShaderNode<MarchSurface> Surface, ShaderNode<MarchRay> Ray, out ShaderNode<Vector4> Color)
	{
		UpdateDelegate(State, out State, Surface, Ray, out Color);
	}
}
