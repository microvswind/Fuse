using System;
using Stride.Core;
using Stride.Rendering;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class ValueInput<T> : AbstractInput<T, ValueParameterKey<T>, ValueParameterUpdater<T>>, AbstractSetValueInput where T : struct
{
	public ValueInput(NodeContext nodeContext)
		: base(nodeContext, "input", false)
	{
		ParameterKey = new ValueParameterKey<T>(ID, 1, Array.Empty<PropertyKeyMetadata>());
		SetFieldDeclaration(TypeHelpers.GetGpuType<T>());
	}

	public void SetAbstractValue(object Value)
	{
		SetValue((T)Value);
	}

	protected override ValueParameterUpdater<T> CreateUpdater()
	{
		return new ValueParameterUpdater<T>((ParameterCollection)null, (ValueParameterKey<T>)null, false);
	}

	public override void OnPassContext(ShaderGeneratorContext nodeContext)
	{
		try
		{
			((ParameterUpdater<T, ValueParameterKey<T>>)(object)Updater).Track(nodeContext, ParameterKey);
			((ParameterUpdater<T, ValueParameterKey<T>>)(object)Updater).Value = _value;
		}
		catch (Exception)
		{
		}
	}

	public override void SetParameterValue(ParameterCollection theCollection)
	{
		theCollection.Set<T>(ParameterKey, Value);
	}

	public override void OnUpdateName()
	{
		ParameterKey = new ValueParameterKey<T>(ID, 1, Array.Empty<PropertyKeyMetadata>());
		SetFieldDeclaration(TypeHelpers.GetGpuType<T>());
	}

	protected override bool HasValue()
	{
		return true;
	}
}
