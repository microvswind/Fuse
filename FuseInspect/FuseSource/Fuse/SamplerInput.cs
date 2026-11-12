using System;
using Stride.Core;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Rendering.Materials;
using VL.Core;

namespace Fuse;

public class SamplerInput : ObjectInput<SamplerState>, IGpuInput, IComputeNode
{
	public SamplerInput(NodeContext nodeContext)
		: base(nodeContext, "SamplerInput")
	{
		SetFieldDeclaration("SamplerState", "SamplerState");
	}

	public override void OnUpdateName()
	{
		ParameterKey = new ObjectParameterKey<SamplerState>(ID, 1, Array.Empty<PropertyKeyMetadata>());
		SetFieldDeclaration("SamplerState", "SamplerState");
	}
}
