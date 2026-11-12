using Fuse.ComputeSystem;
using VL.Core;

namespace Fuse.compute;

public class StructuredBufferAttribute<T> : AbstractStructuredBufferAttribute<T>
{
	public StructuredBufferAttribute(NodeContext nodeContext, string theName, bool theDefineSemantic = true, ShaderNode<GpuStruct> theOverrideInstance = null, ShaderNode<T> theDefault = null, int theArrayCount = 0)
		: base(nodeContext, theName, AttributeType.StructuredBuffer, theArrayCount)
	{
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(nodeContext);
		Default = theDefault ?? ConstantHelper.FromFloat<T>(0f);
		if (theOverrideInstance != null)
		{
			GetMember<GpuStruct, T> input = new GetMember<GpuStruct, T>(nodeSubContextFactory.NextSubContext(), theOverrideInstance, theName);
			SetInput(input);
			IsOverridden = true;
		}
		else
		{
			SetInput(new Semantic<T>(nodeSubContextFactory.NextSubContext(), theName, theDefineSemantic, theName.ToUpper()));
			SetProperty("ComputeSystemAttribute", this);
			SetViewerID("ComputeAttribute");
		}
	}
}
