using System;
using System.Collections.Generic;
using System.Linq;
using Fuse.compute;
using Fuse.function;
using Stride.Graphics;
using VL.Core;

namespace Fuse;

public static class AbstractCreation
{
	public static AbstractShaderNode CreateAbstract(AbstractShaderNode theValue, Type theBaseType, object[] theArguments)
	{
		Type baseType = GetBaseType(theValue);
		Type[] typeArguments = new Type[1] { baseType.GetGenericArguments()[0] };
		return Activator.CreateInstance(theBaseType.MakeGenericType(typeArguments), theArguments) as AbstractShaderNode;
	}

	private static Type GetBaseType(AbstractShaderNode theNode)
	{
		Type type = theNode.GetType();
		while ((object)type != null && (object)type.BaseType != null && type.BaseType != typeof(AbstractShaderNode))
		{
			type = type.BaseType;
		}
		return type;
	}

	private static Type GetBaseType(Type type)
	{
		Type type2 = type;
		while ((object)type2 != null && (object)type2.BaseType != null && type2.BaseType != typeof(AbstractShaderNode))
		{
			type2 = type2.BaseType;
		}
		return type2;
	}

	public static string GetFormattedName(this Type type)
	{
		if (type.IsGenericType)
		{
			string text = (from x in type.GetGenericArguments()
				select x.Name).Aggregate((string x1, string x2) => x1 + ", " + x2);
			return type.Name.Substring(0, type.Name.IndexOf("`")) + "<" + text + ">";
		}
		return type.Name;
	}

	public static AbstractShaderNode AbstractGetMember<T>(NodeSubContextFactory theSubContextFactory, ShaderNode<T> theStruct, AbstractShaderNode theMember)
	{
		Type typeFromHandle = typeof(GetMember<, >);
		Type baseType = GetBaseType(theMember);
		Type[] typeArguments = new Type[2]
		{
			typeof(T),
			baseType.GetGenericArguments()[0]
		};
		return Activator.CreateInstance(typeFromHandle.MakeGenericType(typeArguments), theSubContextFactory.NextSubContext(), theStruct, theMember.Name, null) as AbstractShaderNode;
	}

	public static AbstractShaderNode AbstractGetMember<T>(NodeSubContextFactory theSubContextFactory, ShaderNode<T> theStruct, string theMemberName, Type theMemberGenericType)
	{
		Type typeFromHandle = typeof(GetMember<, >);
		Type[] typeArguments = new Type[2]
		{
			typeof(T),
			theMemberGenericType
		};
		return Activator.CreateInstance(typeFromHandle.MakeGenericType(typeArguments), theSubContextFactory.NextSubContext(), theStruct, theMemberName, null) as AbstractShaderNode;
	}

	public static AbstractShaderNode AbstractSetMember<T>(NodeSubContextFactory theSubContextFactory, ShaderNode<T> theStruct, string theMember, AbstractShaderNode theValue)
	{
		Type typeFromHandle = typeof(SetMember<>);
		Type[] typeArguments = new Type[1] { typeof(T) };
		return Activator.CreateInstance(typeFromHandle.MakeGenericType(typeArguments), theSubContextFactory.NextSubContext(), theStruct, theMember, theValue) as AbstractShaderNode;
	}

	public static AbstractShaderNode AbstractCustomFunction(NodeContext theContext, string theFunction, string theCodeTemplate, AbstractShaderNode theDefault, IEnumerable<AbstractShaderNode> theArguments, IEnumerable<InputModifier> theModifiers = null)
	{
		Type? typeFromHandle = typeof(CustomFunction<>);
		Type baseType = GetBaseType(theDefault);
		return Activator.CreateInstance(typeFromHandle.MakeGenericType(baseType), theContext, theFunction, theCodeTemplate, theDefault, theArguments, null, null, false, theModifiers) as AbstractShaderNode;
	}

	public static AbstractShaderNode AbstractFunctionParameter(NodeSubContextFactory theSubContextFactory, Type theParameterType, InputModifier theModifier = InputModifier.In, int theId = 0, string PinName = "")
	{
		Type typeFromHandle = typeof(FunctionParameter<>);
		Type[] typeArguments = new Type[1] { theParameterType.GetGenericArguments()[0] };
		return Activator.CreateInstance(typeFromHandle.MakeGenericType(typeArguments), theSubContextFactory.NextSubContext(), null, theModifier, theId, PinName) as AbstractShaderNode;
	}

	public static AbstractShaderNode AbstractFunctionParameter(NodeSubContextFactory theSubContextFactory, AbstractShaderNode theParameter, InputModifier theModifier = InputModifier.In, int theId = 0, string PinName = "")
	{
		return AbstractFunctionParameter(theSubContextFactory, GetBaseType(theParameter), theModifier, theId, PinName);
	}

	public static AbstractShaderNode AbstractComputeTextureGet(NodeSubContextFactory theSubContextFactory, ShaderNode<Texture> theTexture, AbstractShaderNode theIndex, AbstractShaderNode theValue)
	{
		Type baseType = GetBaseType(theIndex);
		Type baseType2 = GetBaseType(theValue);
		Type typeFromHandle = typeof(ComputeTextureGet<, >);
		Type[] typeArguments = new Type[2]
		{
			baseType.GetGenericArguments()[0],
			baseType2.GetGenericArguments()[0]
		};
		return Activator.CreateInstance(typeFromHandle.MakeGenericType(typeArguments), theSubContextFactory.NextSubContext(), theTexture, theIndex, null) as AbstractShaderNode;
	}

	public static AbstractShaderNode AbstractComputeTextureSet(NodeSubContextFactory theSubContextFactory, ShaderNode<Texture> theTexture, AbstractShaderNode theIndex, AbstractShaderNode theValue)
	{
		Type baseType = GetBaseType(theIndex);
		Type baseType2 = GetBaseType(theValue);
		Type typeFromHandle = typeof(ComputeTextureSet<, >);
		Type[] typeArguments = new Type[2]
		{
			baseType.GetGenericArguments()[0],
			baseType2.GetGenericArguments()[0]
		};
		return Activator.CreateInstance(typeFromHandle.MakeGenericType(typeArguments), theSubContextFactory.NextSubContext(), theTexture, theIndex, theValue) as AbstractShaderNode;
	}

	public static AbstractShaderNode AbstractShaderNodePassThrough(NodeSubContextFactory theSubContextFactory, AbstractShaderNode theValue)
	{
		return CreateAbstract(theValue, typeof(PassThroughNode<>), new object[3]
		{
			theSubContextFactory.NextSubContext(),
			"pass",
			theValue
		});
	}

	public static AbstractShaderNode AbstractDeclareValue(NodeSubContextFactory theSubContextFactory, AbstractShaderNode theValue)
	{
		return CreateAbstract(theValue, typeof(DeclareValue<>), new object[2]
		{
			theSubContextFactory.NextSubContext(),
			null
		});
	}

	public static AbstractShaderNode AbstractDeclareValueAssigned(NodeSubContextFactory theSubContextFactory, AbstractShaderNode theValue)
	{
		return CreateAbstract(theValue, typeof(DeclareValue<>), new object[2]
		{
			theSubContextFactory.NextSubContext(),
			theValue
		});
	}

	public static AbstractShaderNode AbstractOutput(NodeSubContextFactory theSubContextFactory, AbstractShaderNode theComputation, AbstractShaderNode theOutput)
	{
		return CreateAbstract(theOutput, typeof(Output<>), new object[4]
		{
			theSubContextFactory.NextSubContext(),
			theComputation,
			theOutput,
			null
		});
	}

	public static AbstractShaderNode AbstractAssignNode(NodeSubContextFactory theSubContextFactory, AbstractShaderNode theTarget, AbstractShaderNode theSource)
	{
		return CreateAbstract(theTarget, typeof(AssignValue<>), new object[3]
		{
			theSubContextFactory.NextSubContext(),
			theTarget,
			theSource
		});
	}

	public static AbstractShaderNode AbstractDo2(NodeSubContextFactory theSubContextFactory, AbstractShaderNode theValue, IEnumerable<AbstractShaderNode> theInputs, string theId = "Do2")
	{
		return CreateAbstract(theValue, typeof(Do2<>), new object[4]
		{
			theSubContextFactory.NextSubContext(),
			theValue,
			theInputs,
			theId
		});
	}

	public static AbstractShaderNode AbstractDelegate(NodeSubContextFactory theSubContextFactory, AbstractShaderNode theValue, string theId = "Function")
	{
		return CreateAbstract(theValue, typeof(Delegate<>), new object[5]
		{
			theSubContextFactory.NextSubContext(),
			theValue,
			theValue.FunctionParameters(),
			theId,
			null
		});
	}

	public static AbstractShaderNode AbstractValueInput(NodeSubContextFactory theSubContextFactory, Type theParameterType)
	{
		Type typeFromHandle = typeof(ValueInput<>);
		Type[] typeArguments = new Type[1] { theParameterType.GetGenericArguments()[0] };
		return Activator.CreateInstance(typeFromHandle.MakeGenericType(typeArguments), theSubContextFactory.NextSubContext()) as AbstractShaderNode;
	}

	public static AbstractShaderNode AbstractValueInputGeneric(NodeSubContextFactory theSubContextFactory, Type theGenericType)
	{
		Type typeFromHandle = typeof(ValueInput<>);
		Type[] typeArguments = new Type[1] { theGenericType };
		return Activator.CreateInstance(typeFromHandle.MakeGenericType(typeArguments), theSubContextFactory.NextSubContext()) as AbstractShaderNode;
	}

	public static AbstractShaderNode AbstractValueInput(NodeSubContextFactory theSubContextFactory, object Value)
	{
		if (Value != null && TypeHelpers.IsGpuType(Value.GetType()))
		{
			Type typeFromHandle = typeof(ValueInput<>);
			Type[] typeArguments = new Type[1] { Value.GetType() };
			AbstractShaderNode obj = Activator.CreateInstance(typeFromHandle.MakeGenericType(typeArguments), theSubContextFactory.NextSubContext()) as AbstractShaderNode;
			((AbstractSetValueInput)obj).SetAbstractValue(Value);
			return obj;
		}
		return null;
	}

	public static void AbstractSetValueInput(this AbstractShaderNode ValueInput, object Value)
	{
		((AbstractSetValueInput)ValueInput).SetAbstractValue(Value);
	}

	public static AbstractShaderNode AbstractConstant(AbstractShaderNode theGpuValue, float theValue)
	{
		return AbstractConstant(theGpuValue.GetType().BaseType, theValue);
	}

	public static AbstractShaderNode AbstractConstant(Type theType, float theValue)
	{
		return ConstantHelper.AbstractFromFloat((new Type[1] { theType.GetGenericArguments()[0] })[0], theValue);
	}

	public static AbstractShaderNode AbstractSemantic(NodeSubContextFactory theSubContextFactory, AbstractShaderNode theValue, string theName, bool define = false, string theSemantic = null)
	{
		return CreateAbstract(theValue, typeof(Semantic<>), new object[4]
		{
			theSubContextFactory.NextSubContext(),
			theName,
			define,
			theSemantic
		});
	}
}
