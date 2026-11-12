using System;
using System.Collections.Generic;
using Fuse.ShaderFX;
using Fuse.compute;
using Stride.Graphics;
using Stride.Rendering.Materials;
using Stride.Shaders;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

[MonadicTypeFilter(typeof(ShaderNodeMonadicTypeFilter))]
public class ShaderNode<T> : AbstractShaderNode, IComputeValue<T>, IComputeNode, IMonadicValue<T>, IMonadicValue
{
	public List<AbstractShaderNode> OptionalOutputs { get; protected set; }

	public ShaderNode<T> Default { get; set; }

	public override AbstractShaderNode AbstractDefault => Default;

	public string TypeOverride { get; set; }

	public override string ID => Name + "_" + HashCode;

	bool IMonadicValue.HasValue => HasValue();

	bool IMonadicValue.AcceptsValue => true;

	T IMonadicValue<T>.Value => GetValue();

	public ShaderNode(NodeContext nodeContext, string theId, ShaderNode<T> theDefault = null, bool theCreateDefault = true)
		: base(nodeContext, theId)
	{
		Default = theDefault ?? (theCreateDefault ? ConstantHelper.FromFloat<T>(0f) : null);
	}

	public override ShaderSource GenerateShaderSource(ShaderGeneratorContext context, MaterialComputeColorKeys baseKeys)
	{
		AbstractToShaderFX<T> abstractToShaderFX = ((!(this is IComputeVoid)) ? ((AbstractToShaderFX<T>)new ToShaderFX<T>(this)) : ((AbstractToShaderFX<T>)new ToComputeFx<T>(this)));
		ShaderSource result = abstractToShaderFX.GenerateShaderSource(context, baseKeys);
		ShaderCode = abstractToShaderFX.ShaderCode;
		return result;
	}

	static IMonadicValue<T> IMonadicValue<T>.Create(NodeContext nodeContext, T value)
	{
		return ((IMonadicValue<T>)(object)Create_NonGeneric(nodeContext)).SetValue(value);
		static AbstractShaderNode Create_NonGeneric(NodeContext val)
		{
			if (typeof(T).IsValueType)
			{
				return (AbstractShaderNode)Activator.CreateInstance(typeof(ValueInput<>).MakeGenericType(typeof(T)), val);
			}
			if (typeof(Buffer).IsAssignableFrom(typeof(T)))
			{
				return (AbstractShaderNode)Activator.CreateInstance(typeof(DelegatingBufferInput<>).MakeGenericType(typeof(T)), val);
			}
			if (typeof(T) == typeof(Texture))
			{
				return new DelegatingTextureInput(val);
			}
			if (typeof(T) == typeof(SamplerState))
			{
				return new SamplerInput(val);
			}
			if (typeof(T) == typeof(GpuStruct))
			{
				return new DynamicStruct<GpuStruct>(val, new Dictionary<string, AbstractShaderNode>(), null);
			}
			if (typeof(T) == typeof(GpuVoid))
			{
				return new EmptyVoid(val);
			}
			throw new NotImplementedException(typeof(T).FullName + "Not Implemented");
		}
	}

	IMonadicValue<T> IMonadicValue<T>.SetValue(T value)
	{
		return SetValue(value);
	}

	protected override Dictionary<string, string> CreateTemplateMap()
	{
		return new Dictionary<string, string>
		{
			{ "resultName", ID },
			{
				"resultType",
				TypeName()
			},
			{
				"default",
				(Default == null) ? "" : Default.ID
			},
			{
				"arguments",
				ShaderNodesUtil.BuildArguments(Ins)
			}
		};
	}

	public override string TypeName()
	{
		if (!(typeof(T) == typeof(GpuStruct)) && !(typeof(T) == typeof(Buffer)))
		{
			return TypeHelpers.GetGpuType<T>();
		}
		return TypeOverride;
	}

	protected override string SourceTemplate()
	{
		return "";
	}

	public override int Dimension()
	{
		return TypeHelpers.GetDimension<T>();
	}

	protected virtual T GetValue()
	{
		return default;
	}

	protected virtual ShaderNode<T> SetValue(T value)
	{
		return this;
	}

	protected virtual bool HasValue()
	{
		return false;
	}
}
