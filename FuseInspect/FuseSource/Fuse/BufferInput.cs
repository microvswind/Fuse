using System.Collections.Generic;
using Fuse.compute;
using Stride.Graphics;
using VL.Core;

namespace Fuse;

public class BufferInput<T> : ChangeableObjectInput<Buffer>, IBufferInput<T>
{
	private readonly BufferTypeTracker<T> _typeTracker;

	public ShaderNode<T> Type { get; }

	public BufferType BufferType
	{
		get
		{
			return _typeTracker.BufferType;
		}
		set
		{
			_typeTracker.BufferType = value;
			SetFieldDeclaration(_typeTracker.ComputeGpuType, _typeTracker.GpuType);
		}
	}

	public BufferInput(NodeContext nodeContext, BufferTypeTracker<T> theTypeTracker, ShaderNode<T> theType)
		: base(nodeContext, (GpuTypeTracker<Buffer>)theTypeTracker, "DynamicBufferInput")
	{
		_typeTracker = theTypeTracker;
		Value = null;
		Type = theType;
		SetInputs(new List<AbstractShaderNode> { Type });
	}

	public override string TypeName()
	{
		if (Type != null)
		{
			return Type.TypeName();
		}
		return TypeHelpers.GetGpuType<T>();
	}
}
