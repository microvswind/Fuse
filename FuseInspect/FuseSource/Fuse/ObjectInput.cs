using System;
using Stride.Core;
using Stride.Rendering;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public abstract class ObjectInput<T> : AbstractInput<T, ObjectParameterKey<T>, ObjectParameterUpdater<T>> where T : class
{
	protected ObjectInput(NodeContext nodeContext, string theName)
		: base(nodeContext, theName, true)
	{
		ParameterKey = new ObjectParameterKey<T>(ID, 1, Array.Empty<PropertyKeyMetadata>());
	}

	protected override ObjectParameterUpdater<T> CreateUpdater()
	{
		return new ObjectParameterUpdater<T>((ParameterCollection)null, (ObjectParameterKey<T>)null);
	}

	public override void OnPassContext(ShaderGeneratorContext nodeContext)
	{
		((ParameterUpdater<T, ObjectParameterKey<T>>)(object)Updater).Track(nodeContext, ParameterKey);
		((ParameterUpdater<T, ObjectParameterKey<T>>)(object)Updater).Value = _value;
	}

	public override void SetParameterValue(ParameterCollection theCollection)
	{
		theCollection.Set<T>(ParameterKey, _value);
	}
}
