using System.Collections.Generic;
using Fuse.compute;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.ComputeSystem;

public abstract class Attribute<T> : PassThroughNode<T>, IAttribute
{
	public AttributeType AttributeType { get; set; }

	public AbstractShaderNode ShaderNode { get; }

	public AbstractShaderNode InputAbstract
	{
		get
		{
			return Input;
		}
		set
		{
			SetInput(value);
		}
	}

	public ShaderNode<GpuVoid> WriteCall { get; set; }

	public AbstractShaderNode ReadCall { get; set; }

	public abstract Int3 Resolution { get; }

	public bool IsOverridden { get; protected set; }

	public Attribute(NodeContext nodeContext, string theName, AttributeType theType, AbstractShaderNode theValue = null)
		: base(nodeContext, theValue, theName)
	{
		AttributeType = theType;
		WriteCall = new EmptyVoid(new NodeSubContextFactory(nodeContext).NextSubContext());
		ShaderNode = new ShaderNode<T>(new NodeSubContextFactory(nodeContext).NextSubContext(), theName);
		IsOverridden = false;
		HasFixedName = true;
	}

	public virtual void Sync(IAttribute theAttribute)
	{
		if (theAttribute != null && theAttribute != this)
		{
			InputAbstract = theAttribute.InputAbstract;
			ReadCall = theAttribute.ReadCall;
			WriteCall = theAttribute.WriteCall;
		}
	}

	public void SetInput(AbstractShaderNode theNode)
	{
		Input = theNode as ShaderNode<T>;
		SetInputs(new List<AbstractShaderNode> { Input });
	}
}
