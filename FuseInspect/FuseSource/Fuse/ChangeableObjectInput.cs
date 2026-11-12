using System;
using Stride.Core;
using Stride.Rendering;
using VL.Core;

namespace Fuse;

public abstract class ChangeableObjectInput<T> : ObjectInput<T> where T : class
{
	public GpuTypeTracker<T> TypeTracker { get; }

	protected ChangeableObjectInput(NodeContext nodeContext, GpuTypeTracker<T> theTypeTracker, string theName)
		: base(nodeContext, theName)
	{
		TypeTracker = theTypeTracker;
		SetFieldDeclaration(TypeTracker.GpuType, TypeTracker.ComputeGpuType);
	}

	public override void OnUpdateName()
	{
		ParameterKey = new ObjectParameterKey<T>(ID, 1, Array.Empty<PropertyKeyMetadata>());
		SetFieldDeclaration(TypeTracker.GpuType, TypeTracker.ComputeGpuType);
	}
}
