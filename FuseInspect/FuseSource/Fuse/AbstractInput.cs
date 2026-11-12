using System.Collections.Generic;
using Stride.Rendering;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public abstract class AbstractInput<T, TParameterKeyType, TParameterUpdaterType> : ShaderNode<T>, IGpuInput, IComputeNode where TParameterKeyType : ParameterKey<T> where TParameterUpdaterType : ParameterUpdater<T, TParameterKeyType>
{
	private readonly FieldDeclaration _fieldDeclaration;

	protected readonly TParameterUpdaterType Updater;

	protected T _value;

	protected TParameterKeyType ParameterKey { get; set; }

	public virtual T Value
	{
		get
		{
			return ((ParameterUpdater<T, TParameterKeyType>)Updater).Value;
		}
		set
		{
			_value = value;
			((ParameterUpdater<T, TParameterKeyType>)Updater).Value = value;
		}
	}

	protected AbstractInput(NodeContext nodeContext, string theName, bool theIsResource)
		: base(nodeContext, theName, (ShaderNode<T>)null, true)
	{
		Updater = CreateUpdater();
		SetInputs(new List<AbstractShaderNode>());
		AddProperty("Inputs", this);
		_fieldDeclaration = new FieldDeclaration(theIsResource, "", "", ID);
		AddProperty("Declarations", _fieldDeclaration);
	}

	public abstract void SetParameterValue(ParameterCollection theCollection);

	public abstract void OnUpdateName();

	protected abstract TParameterUpdaterType CreateUpdater();

	protected override string SourceTemplate()
	{
		return "";
	}

	protected override string GenerateDefaultSource()
	{
		return "";
	}

	protected override T GetValue()
	{
		return Value;
	}

	protected override ShaderNode<T> SetValue(T value)
	{
		Value = value;
		return this;
	}

	protected void SetFieldDeclaration(string theTypeName, string theComputeTypeName)
	{
		_fieldDeclaration.Set(theComputeTypeName, theTypeName, ID);
	}

	protected void SetFieldDeclaration(string theTypeName)
	{
		SetFieldDeclaration(theTypeName, theTypeName);
	}
}
