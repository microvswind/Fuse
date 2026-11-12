using System;
using System.Collections.Generic;
using Fuse.compute;
using Fuse.function;
using Stride.Core.Mathematics;
using Stride.Graphics;

namespace Fuse;

public static class TypeHelpers
{
	private static readonly Dictionary<Type, string> StructTypes = new Dictionary<Type, string>();

	private static readonly Dictionary<Type, string> TypeDefaults = new Dictionary<Type, string>
	{
		{
			typeof(float),
			"0.0"
		},
		{
			typeof(Vector2),
			"float2(0.0, 0.0)"
		},
		{
			typeof(Vector3),
			"float3(0.0, 0.0, 0.0)"
		},
		{
			typeof(Vector4),
			"float4(0.0, 0.0, 0.0, 0.0)"
		},
		{
			typeof(Color4),
			"float4(0.0, 0.0, 0.0, 0.0)"
		},
		{
			typeof(Matrix),
			"float4x4()"
		},
		{
			typeof(Matrix3),
			"float3x3()"
		},
		{
			typeof(Matrix2),
			"float2x2()"
		},
		{
			typeof(int),
			"0"
		},
		{
			typeof(Int2),
			"int2(0, 0)"
		},
		{
			typeof(Int3),
			"int3(0, 0, 0)"
		},
		{
			typeof(Int4),
			"int4(0, 0, 0, 0)"
		},
		{
			typeof(uint),
			"0"
		},
		{
			typeof(ushort),
			"0"
		},
		{
			typeof(bool),
			"true"
		}
	};

	private static readonly Dictionary<Type, string> KnownSignatureTypes = new Dictionary<Type, string>
	{
		{
			typeof(float),
			"Float"
		},
		{
			typeof(Vector2),
			"Float2"
		},
		{
			typeof(Vector3),
			"Float3"
		},
		{
			typeof(Vector4),
			"Float4"
		},
		{
			typeof(Color4),
			"Float4"
		},
		{
			typeof(Matrix),
			"Matrix"
		},
		{
			typeof(Matrix3),
			"Matrix3"
		},
		{
			typeof(Matrix2),
			"Matrix2"
		},
		{
			typeof(int),
			"Int"
		},
		{
			typeof(Int2),
			"Int2"
		},
		{
			typeof(Int3),
			"Int3"
		},
		{
			typeof(Int4),
			"Int4"
		},
		{
			typeof(uint),
			"UInt"
		},
		{
			typeof(ushort),
			"UShort"
		},
		{
			typeof(bool),
			"Bool"
		},
		{
			typeof(GpuStruct),
			"Struct"
		},
		{
			typeof(Texture),
			"Texture"
		},
		{
			typeof(SamplerState),
			"Sampler"
		},
		{
			typeof(GpuVoid),
			"Void"
		}
	};

	private static readonly Dictionary<Type, string> KnownGpuTypes = new Dictionary<Type, string>
	{
		{
			typeof(float),
			"float"
		},
		{
			typeof(Vector2),
			"float2"
		},
		{
			typeof(Vector3),
			"float3"
		},
		{
			typeof(Vector4),
			"float4"
		},
		{
			typeof(Color4),
			"float4"
		},
		{
			typeof(Matrix),
			"float4x4"
		},
		{
			typeof(Matrix3),
			"float3x3"
		},
		{
			typeof(Matrix2),
			"float2x2"
		},
		{
			typeof(int),
			"int"
		},
		{
			typeof(Int2),
			"int2"
		},
		{
			typeof(Int3),
			"int3"
		},
		{
			typeof(Int4),
			"int4"
		},
		{
			typeof(uint),
			"uint"
		},
		{
			typeof(ushort),
			"ushort"
		},
		{
			typeof(bool),
			"bool"
		},
		{
			typeof(GpuStruct),
			"struct"
		},
		{
			typeof(Texture),
			"Texture"
		},
		{
			typeof(SamplerState),
			"Sampler"
		},
		{
			typeof(GpuVoid),
			"void"
		}
	};

	private static readonly Dictionary<Type, string> KnownCompositionTypes = new Dictionary<Type, string>
	{
		{
			typeof(float),
			"ComputeFloat"
		},
		{
			typeof(Vector2),
			"ComputeFloat2"
		},
		{
			typeof(Vector3),
			"ComputeFloat3"
		},
		{
			typeof(Vector4),
			"ComputeFloat4"
		},
		{
			typeof(Color4),
			"ComputeColor"
		},
		{
			typeof(Matrix),
			"ComputeMatrix"
		},
		{
			typeof(int),
			"ComputeInt"
		},
		{
			typeof(Int2),
			"ComputeInt2"
		},
		{
			typeof(Int3),
			"ComputeInt3"
		},
		{
			typeof(Int4),
			"ComputeInt4"
		},
		{
			typeof(uint),
			"ComputeUint"
		},
		{
			typeof(ushort),
			"ComputeUShort"
		},
		{
			typeof(bool),
			"ComputeBool"
		},
		{
			typeof(GpuStruct),
			"struct"
		},
		{
			typeof(Texture),
			"Texture"
		},
		{
			typeof(SamplerState),
			"Sampler"
		},
		{
			typeof(GpuVoid),
			"ComputeVoid"
		}
	};

	private static readonly Dictionary<Type, int> KnownDimensions = new Dictionary<Type, int>
	{
		{
			typeof(float),
			1
		},
		{
			typeof(Vector2),
			2
		},
		{
			typeof(Vector3),
			3
		},
		{
			typeof(Vector4),
			4
		},
		{
			typeof(Color4),
			4
		},
		{
			typeof(Matrix),
			16
		},
		{
			typeof(Matrix3),
			9
		},
		{
			typeof(Matrix2),
			4
		},
		{
			typeof(int),
			1
		},
		{
			typeof(Int2),
			2
		},
		{
			typeof(Int3),
			3
		},
		{
			typeof(Int4),
			4
		},
		{
			typeof(uint),
			1
		},
		{
			typeof(ushort),
			1
		},
		{
			typeof(bool),
			1
		},
		{
			typeof(GpuStruct),
			1
		},
		{
			typeof(Texture),
			1
		},
		{
			typeof(SamplerState),
			1
		},
		{
			typeof(GpuVoid),
			0
		}
	};

	private static readonly Dictionary<Type, int> KnownByteSizes = new Dictionary<Type, int>
	{
		{
			typeof(float),
			4
		},
		{
			typeof(Vector2),
			8
		},
		{
			typeof(Vector3),
			12
		},
		{
			typeof(Vector4),
			16
		},
		{
			typeof(Color4),
			16
		},
		{
			typeof(Matrix),
			64
		},
		{
			typeof(Matrix3),
			36
		},
		{
			typeof(Matrix2),
			16
		},
		{
			typeof(int),
			4
		},
		{
			typeof(Int2),
			8
		},
		{
			typeof(Int3),
			12
		},
		{
			typeof(Int4),
			16
		},
		{
			typeof(uint),
			4
		},
		{
			typeof(ushort),
			2
		},
		{
			typeof(bool),
			1
		},
		{
			typeof(GpuVoid),
			0
		}
	};

	private static readonly Dictionary<Type, PixelFormat> KnownPixelFormats = new Dictionary<Type, PixelFormat>
	{
		{
			typeof(float),
			(PixelFormat)41
		},
		{
			typeof(Vector2),
			(PixelFormat)16
		},
		{
			typeof(Vector3),
			(PixelFormat)6
		},
		{
			typeof(Vector4),
			(PixelFormat)2
		},
		{
			typeof(Color4),
			(PixelFormat)2
		},
		{
			typeof(int),
			(PixelFormat)43
		},
		{
			typeof(Int2),
			(PixelFormat)18
		},
		{
			typeof(Int3),
			(PixelFormat)8
		},
		{
			typeof(Int4),
			(PixelFormat)4
		},
		{
			typeof(uint),
			(PixelFormat)42
		},
		{
			typeof(ushort),
			(PixelFormat)57
		},
		{
			typeof(bool),
			(PixelFormat)66
		},
		{
			typeof(GpuVoid),
			(PixelFormat)0
		}
	};

	private static readonly Dictionary<PixelFormat, Type> PixelFormatTypes = new Dictionary<PixelFormat, Type>
	{
		{
			(PixelFormat)41,
			typeof(float)
		},
		{
			(PixelFormat)16,
			typeof(Vector2)
		},
		{
			(PixelFormat)6,
			typeof(Vector3)
		},
		{
			(PixelFormat)2,
			typeof(Vector4)
		},
		{
			(PixelFormat)43,
			typeof(int)
		},
		{
			(PixelFormat)18,
			typeof(Int2)
		},
		{
			(PixelFormat)8,
			typeof(Int3)
		},
		{
			(PixelFormat)4,
			typeof(Int4)
		},
		{
			(PixelFormat)42,
			typeof(uint)
		},
		{
			(PixelFormat)57,
			typeof(ushort)
		},
		{
			(PixelFormat)66,
			typeof(bool)
		}
	};

	public static void ConstrainTypes<T>(T input, T input2, T input3, T input4)
	{
	}

	public static void ConstrainTypesByEnumerable<T>(T input, IEnumerable<T> input2)
	{
	}

	public static void ConstrainTypesByDictionary<T>(T input, IDictionary<string, T> input2)
	{
	}

	public static void ConstrainTypeByShaderNode<T>(ShaderNode<T> input, T input2) where T : struct
	{
	}

	public static void ConstrainTypesByBuffer<T>(ShaderNode<T> input, ShaderNode<Buffer<T>> input2) where T : unmanaged
	{
	}

	public static void ConstrainType<T>(T value, ShaderNode<T> shaderNode, Buffer<T> buffer, ShaderNode<Buffer<T>> gpuBuffer) where T : unmanaged
	{
	}

	public static void ConstrainBuffer<T>(T value, ShaderNode<T> shaderNode, Buffer<T> buffer, ShaderNode<Buffer<T>> gpuBuffer) where T : unmanaged
	{
	}

	public static bool IsStructType(Type t)
	{
		return StructTypes.ContainsKey(t);
	}

	public static bool IsStructType<T>()
	{
		return IsStructType(typeof(T));
	}

	public static bool IsGpuArray(Type type)
	{
		if (type == null)
		{
			return false;
		}
		if (type.IsGenericType)
		{
			return type.GetGenericTypeDefinition() == typeof(GpuArray<>);
		}
		return false;
	}

	public static bool IsGpuArray<T>()
	{
		return IsStructType(typeof(T));
	}

	public static bool IsGpuArray<T>(ShaderNode<T> shaderNode)
	{
		return IsGpuArray(typeof(T));
	}

	public static bool IsGpuArray(AbstractShaderNode theValue)
	{
		if (theValue == null || !theValue.GetType().IsGenericType)
		{
			return false;
		}
		Type type = theValue.GetType().GetGenericArguments()[0];
		if (type.IsGenericType)
		{
			return type.GetGenericTypeDefinition() == typeof(GpuArray<>);
		}
		return false;
	}

	public static bool IsDelegate(AbstractShaderNode theValue)
	{
		if (theValue != null && theValue.GetType().IsGenericType)
		{
			return theValue.GetType().GetGenericTypeDefinition() == typeof(Delegate<>);
		}
		return false;
	}

	public static bool IsVoidNode(AbstractShaderNode theValue)
	{
		if (theValue is ShaderNode<GpuVoid>)
		{
			return true;
		}
		return false;
	}

	public static bool IsVoid(Type t)
	{
		return t == typeof(GpuVoid);
	}

	public static List<string> GetDescription(AbstractShaderNode theValue)
	{
		List<string> list = new List<string> { "x", "y", "z", "w" };
		int dimension = GetDimension(theValue);
		List<string> list2 = new List<string>();
		if (dimension == 1)
		{
			list2.Add(theValue.Name);
		}
		else
		{
			for (int i = 0; i < dimension; i++)
			{
				list2.Add(theValue.Name + "." + list[i]);
			}
		}
		return list2;
	}

	public static ShaderNode<TOut> AdaptiveSignature<TIn, TOut>(string thePrepend, ShaderNode<TIn> theValue, out string theSignature)
	{
		theSignature = thePrepend + GetDimension(typeof(TOut)) + GetDimension(typeof(TIn));
		return null;
	}

	public static void LimitToStruct<T>(ShaderNode<T> theValue) where T : struct
	{
	}

	public static string TextureTypeName(Texture theTexture, bool theUseRW)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected I4, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Invalid comparison between Unknown and I4
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Invalid comparison between Unknown and I4
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected I4, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected I4, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Invalid comparison between Unknown and I4
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Invalid comparison between Unknown and I4
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Invalid comparison between Unknown and I4
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected I4, but got Unknown
		if (theTexture == null)
		{
			return "Texture2D<float4>";
		}
		TextureDimension dimension = theTexture.Dimension;
		string text = (int)dimension switch
		{
			0 => "Texture1D", 
			1 => (theTexture.ArraySize == 1) ? "Texture2D" : "Texture2DArray", 
			2 => "Texture3D", 
			3 => "TextureCube", 
			_ => "Texture2D", 
		};
		PixelFormat format = theTexture.Format;
		string text2;
		if ((int)format <= 18)
		{
			switch (format - 2)
			{
			case 1:
				goto IL_00e8;
			case 2:
				goto IL_00f0;
			case 0:
				goto IL_0120;
			}
			if ((int)format != 10)
			{
				switch (format - 16)
				{
				case 1:
					break;
				case 2:
					goto IL_00e0;
				case 0:
					goto IL_0118;
				default:
					goto IL_0128;
				}
				text2 = "uint2";
			}
			else
			{
				text2 = "half4";
			}
		}
		else if ((int)format != 34)
		{
			switch (format - 41)
			{
			case 1:
				goto IL_00c8;
			case 2:
				goto IL_00d0;
			case 0:
				goto IL_0110;
			}
			if ((int)format != 54)
			{
				goto IL_0128;
			}
			text2 = "half";
		}
		else
		{
			text2 = "half2";
		}
		goto IL_012e;
		IL_00e8:
		text2 = "uint4";
		goto IL_012e;
		IL_0118:
		text2 = "float2";
		goto IL_012e;
		IL_012e:
		string text3 = text2;
		if (((theTexture.Flags & 4) == 4) & theUseRW)
		{
			return "RW" + text + "<" + text3 + ">";
		}
		return text + "<" + text3 + ">";
		IL_00e0:
		text2 = "int2";
		goto IL_012e;
		IL_0128:
		text2 = "float4";
		goto IL_012e;
		IL_0110:
		text2 = "float";
		goto IL_012e;
		IL_00f0:
		text2 = "int4";
		goto IL_012e;
		IL_00d0:
		text2 = "int";
		goto IL_012e;
		IL_00c8:
		text2 = "uint";
		goto IL_012e;
		IL_0120:
		text2 = "float4";
		goto IL_012e;
	}

	public static string BufferTypeName(Buffer theBuffer, string theType, BufferType theBufferType)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Invalid comparison between Unknown and I4
		switch (theBufferType)
		{
		case BufferType.Append:
			return "AppendStructuredBuffer<" + theType + ">";
		case BufferType.Consume:
			return "ConsumeStructuredBuffer<" + theType + ">";
		default:
			if (theBuffer == null)
			{
				if (theBufferType == BufferType.RW)
				{
					return "RWStructuredBuffer<" + theType + ">";
				}
				return "StructuredBuffer<" + theType + ">";
			}
			if ((theBuffer.Flags & 0x20) == 32)
			{
				return "RWStructuredBuffer<" + theType + ">";
			}
			return "StructuredBuffer<" + theType + ">";
		}
	}

	public static string GetGenericTypeName(AbstractShaderNode node)
	{
		if (node == null)
		{
			return "";
		}
		Type[] genericArguments = node.GetType().GetGenericArguments();
		if (genericArguments.Length == 0)
		{
			return null;
		}
		Type type = genericArguments[0];
		if (type == typeof(float))
		{
			return "Float32";
		}
		if (type == typeof(double))
		{
			return "Float64";
		}
		if (type == typeof(int))
		{
			return "Integer32";
		}
		if (type == typeof(long))
		{
			return "Integer64";
		}
		if (type == typeof(Color4))
		{
			return "RGBA";
		}
		return type.Name;
	}

	public static string ValueToString(object theObject)
	{
		if ((!(theObject is byte) && !(theObject is sbyte) && !(theObject is short) && !(theObject is ushort) && !(theObject is int) && !(theObject is uint) && !(theObject is long) && !(theObject is ulong)) || 1 == 0)
		{
			return theObject.ToString();
		}
		return Convert.ToInt64(theObject).ToString("#,##0").Replace(",", ".");
	}

	public static string GetDefaultForType<T>(T theValue)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		if (typeof(T) == typeof(float))
		{
			return theValue.ToString();
		}
		if (typeof(T) == typeof(Vector2))
		{
			Vector2 val = (Vector2)Convert.ChangeType(theValue, typeof(Vector2));
			return $"float2({val.X},{val.Y})";
		}
		if (typeof(T) == typeof(Vector3))
		{
			Vector3 val2 = (Vector3)Convert.ChangeType(theValue, typeof(Vector3));
			return $"float3({val2.X},{val2.Y},{val2.Z})";
		}
		if (typeof(T) == typeof(Vector4))
		{
			Vector4 val3 = (Vector4)Convert.ChangeType(theValue, typeof(Vector4));
			return $"float4({val3.X},{val3.Y},{val3.Z},{val3.W})";
		}
		if (typeof(T) == typeof(Color4))
		{
			Color4 val4 = (Color4)Convert.ChangeType(theValue, typeof(Color4));
			return $"float4({val4.R},{val4.G},{val4.B},{val4.A})";
		}
		if (typeof(T) == typeof(int))
		{
			return theValue.ToString();
		}
		if (typeof(T) == typeof(ushort))
		{
			return theValue.ToString();
		}
		if (typeof(T) == typeof(Int2))
		{
			Int2 val5 = (Int2)Convert.ChangeType(theValue, typeof(Int2));
			return $"int2({val5.X},{val5.Y})";
		}
		if (typeof(T) == typeof(Int3))
		{
			Int3 val6 = (Int3)Convert.ChangeType(theValue, typeof(Int3));
			return $"int3({val6.X},{val6.Y},{val6.Z})";
		}
		if (typeof(T) == typeof(Int4))
		{
			Int4 val7 = (Int4)Convert.ChangeType(theValue, typeof(Int4));
			return $"int4({val7.X},{val7.Y},{val7.Z},{val7.W})";
		}
		if (typeof(T) == typeof(bool))
		{
			return theValue.ToString()?.ToLower();
		}
		if (typeof(T) == typeof(SamplerState))
		{
			return "Sampler";
		}
		if (typeof(T) == typeof(Texture))
		{
			return "Texture0";
		}
		return GetDefaultForType(typeof(T));
	}

	public static string GetDefaultForType(Type t)
	{
		if (TypeDefaults.TryGetValue(t, out var value))
		{
			return value;
		}
		throw new NotImplementedException("No name defined for type: " + t.Name);
	}

	public static string GetDefaultForType<T>()
	{
		if (TypeDefaults.TryGetValue(typeof(T), out var value))
		{
			return value;
		}
		throw new NotImplementedException("No name defined for type: " + typeof(T).Name);
	}

	public static string GetSignature(AbstractShaderNode theValue)
	{
		if (!(theValue is ShaderNode<float>))
		{
			if (!(theValue is ShaderNode<Vector2>))
			{
				if (!(theValue is ShaderNode<Vector3>))
				{
					if (!(theValue is ShaderNode<Vector4>))
					{
						if (!(theValue is ShaderNode<Color4>))
						{
							if (!(theValue is ShaderNode<bool>))
							{
								if (!(theValue is ShaderNode<int>))
								{
									if (!(theValue is ShaderNode<uint>))
									{
										if (!(theValue is ShaderNode<ushort>))
										{
											if (!(theValue is ShaderNode<Int2>))
											{
												if (!(theValue is ShaderNode<Int3>))
												{
													if (!(theValue is ShaderNode<Int4>))
													{
														if (!(theValue is ShaderNode<GpuVoid>))
														{
															if (!(theValue is ShaderNode<Matrix>))
															{
																if (!(theValue is ShaderNode<Matrix3>))
																{
																	if (theValue is ShaderNode<Matrix2>)
																	{
																		return "Matrix2";
																	}
																	throw new NotImplementedException("No name defined for type: " + theValue.GetType().FullName);
																}
																return "Matrix3";
															}
															return "Matrix";
														}
														return "Void";
													}
													return "Int4";
												}
												return "Int3";
											}
											return "Int2";
										}
										return "UShort";
									}
									return "UInt";
								}
								return "Int";
							}
							return "Bool";
						}
						return "Float4";
					}
					return "Float4";
				}
				return "Float3";
			}
			return "Float2";
		}
		return "Float";
	}

	public static string GetSignature(Type type)
	{
		if (KnownSignatureTypes.TryGetValue(type, out var value))
		{
			return value;
		}
		throw new NotImplementedException("No name defined for type: " + type.Name + " : " + type.FullName);
	}

	public static string GetSignature<T>()
	{
		return GetSignature(typeof(T));
	}

	public static string GetSignature<T>(ShaderNode<T> shaderNode)
	{
		return GetSignature(typeof(T));
	}

	public static string GetGpuType(AbstractShaderNode abstractShaderNode)
	{
		if (!(abstractShaderNode is ShaderNode<float>))
		{
			if (!(abstractShaderNode is ShaderNode<Vector2>))
			{
				if (!(abstractShaderNode is ShaderNode<Vector3>))
				{
					if (!(abstractShaderNode is ShaderNode<Vector4>))
					{
						if (!(abstractShaderNode is ShaderNode<Color4>))
						{
							if (!(abstractShaderNode is ShaderNode<Matrix>))
							{
								if (!(abstractShaderNode is ShaderNode<Matrix3>))
								{
									if (!(abstractShaderNode is ShaderNode<Matrix2>))
									{
										if (!(abstractShaderNode is ShaderNode<bool>))
										{
											if (!(abstractShaderNode is ShaderNode<int>))
											{
												if (!(abstractShaderNode is ShaderNode<uint>))
												{
													if (!(abstractShaderNode is ShaderNode<ushort>))
													{
														if (!(abstractShaderNode is ShaderNode<Int2>))
														{
															if (!(abstractShaderNode is ShaderNode<Int3>))
															{
																if (!(abstractShaderNode is ShaderNode<Int4>))
																{
																	if (!(abstractShaderNode is ShaderNode<GpuVoid>))
																	{
																		if (!(abstractShaderNode is ShaderNode<GpuArray<float>>))
																		{
																			if (!(abstractShaderNode is ShaderNode<GpuArray<Vector2>>))
																			{
																				if (!(abstractShaderNode is ShaderNode<GpuArray<Vector3>>))
																				{
																					if (!(abstractShaderNode is ShaderNode<GpuArray<Vector4>>))
																					{
																						if (!(abstractShaderNode is ShaderNode<GpuArray<Color4>>))
																						{
																							if (!(abstractShaderNode is ShaderNode<GpuArray<Matrix>>))
																							{
																								if (!(abstractShaderNode is ShaderNode<GpuArray<Matrix3>>))
																								{
																									if (!(abstractShaderNode is ShaderNode<GpuArray<Matrix2>>))
																									{
																										if (!(abstractShaderNode is ShaderNode<GpuArray<bool>>))
																										{
																											if (!(abstractShaderNode is ShaderNode<GpuArray<int>>))
																											{
																												if (!(abstractShaderNode is ShaderNode<GpuArray<uint>>))
																												{
																													if (!(abstractShaderNode is ShaderNode<GpuArray<ushort>>))
																													{
																														if (!(abstractShaderNode is ShaderNode<GpuArray<Int2>>))
																														{
																															if (!(abstractShaderNode is ShaderNode<GpuArray<Int3>>))
																															{
																																if (abstractShaderNode is ShaderNode<GpuArray<Int4>>)
																																{
																																	return "int4";
																																}
																																throw new NotImplementedException("No name defined for type: " + abstractShaderNode.GetType().FullName);
																															}
																															return "int3";
																														}
																														return "int2";
																													}
																													return "ushort";
																												}
																												return "uint";
																											}
																											return "int";
																										}
																										return "bool";
																									}
																									return "float2x2";
																								}
																								return "float3x3";
																							}
																							return "float4x4";
																						}
																						return "float4";
																					}
																					return "float4";
																				}
																				return "float3";
																			}
																			return "float2";
																		}
																		return "float";
																	}
																	return "void";
																}
																return "int4";
															}
															return "int3";
														}
														return "int2";
													}
													return "ushort";
												}
												return "uint";
											}
											return "int";
										}
										return "bool";
									}
									return "float2x2";
								}
								return "float3x3";
							}
							return "float4x4";
						}
						return "float4";
					}
					return "float4";
				}
				return "float3";
			}
			return "float2";
		}
		return "float";
	}

	public static bool IsGpuType(Type type)
	{
		return KnownGpuTypes.ContainsKey(type);
	}

	public static string GetGpuType(Type type)
	{
		if (!KnownGpuTypes.TryGetValue(type, out var value))
		{
			return ShaderNodesUtil.CleanVlClassName(type.Name);
		}
		return value;
	}

	public static string GetGpuType<T>()
	{
		return GetGpuType(typeof(T));
	}

	public static string GetGpuType<T>(ShaderNode<T> shaderNode)
	{
		return GetGpuType<T>();
	}

	public static string GetCompositionType(AbstractShaderNode abstractShaderNode)
	{
		if (!(abstractShaderNode is ShaderNode<float>))
		{
			if (!(abstractShaderNode is ShaderNode<Vector2>))
			{
				if (!(abstractShaderNode is ShaderNode<Vector3>))
				{
					if (!(abstractShaderNode is ShaderNode<Vector4>))
					{
						if (!(abstractShaderNode is ShaderNode<Color4>))
						{
							if (!(abstractShaderNode is ShaderNode<Matrix>))
							{
								if (!(abstractShaderNode is ShaderNode<bool>))
								{
									if (!(abstractShaderNode is ShaderNode<int>))
									{
										if (!(abstractShaderNode is ShaderNode<uint>))
										{
											if (!(abstractShaderNode is ShaderNode<ushort>))
											{
												if (!(abstractShaderNode is ShaderNode<Int2>))
												{
													if (!(abstractShaderNode is ShaderNode<Int3>))
													{
														if (!(abstractShaderNode is ShaderNode<Int4>))
														{
															if (abstractShaderNode is ShaderNode<GpuVoid>)
															{
																return "ComputeVoid";
															}
															throw new NotImplementedException("No name defined for type: " + abstractShaderNode.GetType().FullName);
														}
														return "ComputeUint";
													}
													return "ComputeInt3";
												}
												return "ComputeInt2";
											}
											return "ComputeUShort";
										}
										return "ComputeUInt";
									}
									return "ComputeInt";
								}
								return "ComputeBool";
							}
							return "ComputeMatrix";
						}
						return "ComputeColor";
					}
					return "ComputeFloat4";
				}
				return "ComputeFloat3";
			}
			return "ComputeFloat2";
		}
		return "ComputeFloat";
	}

	public static string GetCompositionType(Type type)
	{
		if (!KnownCompositionTypes.TryGetValue(type, out var value))
		{
			return ShaderNodesUtil.CleanVlClassName(type.Name);
		}
		return value;
	}

	public static string GetCompositionType<T>()
	{
		return GetCompositionType(typeof(T));
	}

	public static string GetCompositionType<T>(ShaderNode<T> shaderNode)
	{
		return GetCompositionType<T>();
	}

	public static int GetDimension(AbstractShaderNode abstractShaderNode)
	{
		if (!(abstractShaderNode is ShaderNode<float>))
		{
			if (!(abstractShaderNode is ShaderNode<Vector2>))
			{
				if (!(abstractShaderNode is ShaderNode<Vector3>))
				{
					if (!(abstractShaderNode is ShaderNode<Vector4>))
					{
						if (!(abstractShaderNode is ShaderNode<Color4>))
						{
							if (!(abstractShaderNode is ShaderNode<bool>))
							{
								if (!(abstractShaderNode is ShaderNode<int>))
								{
									if (!(abstractShaderNode is ShaderNode<uint>))
									{
										if (!(abstractShaderNode is ShaderNode<ushort>))
										{
											if (!(abstractShaderNode is ShaderNode<Int2>))
											{
												if (!(abstractShaderNode is ShaderNode<Int3>))
												{
													if (!(abstractShaderNode is ShaderNode<Int4>))
													{
														if (!(abstractShaderNode is ShaderNode<GpuVoid>))
														{
															if (!(abstractShaderNode is ShaderNode<Matrix>))
															{
																if (!(abstractShaderNode is ShaderNode<Matrix3>))
																{
																	if (abstractShaderNode is ShaderNode<Matrix2>)
																	{
																		return 4;
																	}
																	return 0;
																}
																return 9;
															}
															return 16;
														}
														return 0;
													}
													return 4;
												}
												return 3;
											}
											return 2;
										}
										return 1;
									}
									return 1;
								}
								return 1;
							}
							return 1;
						}
						return 4;
					}
					return 4;
				}
				return 3;
			}
			return 2;
		}
		return 1;
	}

	public static int GetDimension(Type type)
	{
		if (!KnownDimensions.TryGetValue(type, out var value))
		{
			return 0;
		}
		return value;
	}

	public static int GetDimension<T>(ShaderNode<T> shaderNode)
	{
		return GetDimension(typeof(T));
	}

	public static int GetDimension<T>()
	{
		return GetDimension(typeof(T));
	}

	public static int GetDimensionFromInt3(Int3 dimension)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if (dimension.Y == 1 && dimension.Z == 1)
		{
			return 1;
		}
		if (dimension.Z == 1)
		{
			return 2;
		}
		return 3;
	}

	public static int GetSizeInBytes(AbstractShaderNode abstractShaderNode)
	{
		if (!(abstractShaderNode is ShaderNode<float>))
		{
			if (!(abstractShaderNode is ShaderNode<Vector2>))
			{
				if (!(abstractShaderNode is ShaderNode<Vector3>))
				{
					if (!(abstractShaderNode is ShaderNode<Vector4>))
					{
						if (!(abstractShaderNode is ShaderNode<Color4>))
						{
							if (!(abstractShaderNode is ShaderNode<Matrix>))
							{
								if (!(abstractShaderNode is ShaderNode<Matrix3>))
								{
									if (!(abstractShaderNode is ShaderNode<Matrix2>))
									{
										if (!(abstractShaderNode is ShaderNode<bool>))
										{
											if (!(abstractShaderNode is ShaderNode<int>))
											{
												if (!(abstractShaderNode is ShaderNode<uint>))
												{
													if (!(abstractShaderNode is ShaderNode<ushort>))
													{
														if (!(abstractShaderNode is ShaderNode<Int2>))
														{
															if (!(abstractShaderNode is ShaderNode<Int3>))
															{
																if (!(abstractShaderNode is ShaderNode<Int4>))
																{
																	if (!(abstractShaderNode is ShaderNode<GpuArray<float>>))
																	{
																		if (!(abstractShaderNode is ShaderNode<GpuArray<Vector2>>))
																		{
																			if (!(abstractShaderNode is ShaderNode<GpuArray<Vector3>>))
																			{
																				if (!(abstractShaderNode is ShaderNode<GpuArray<Vector4>>))
																				{
																					if (!(abstractShaderNode is ShaderNode<GpuArray<Color4>>))
																					{
																						if (!(abstractShaderNode is ShaderNode<GpuArray<Matrix>>))
																						{
																							if (!(abstractShaderNode is ShaderNode<GpuArray<Matrix3>>))
																							{
																								if (!(abstractShaderNode is ShaderNode<GpuArray<Matrix2>>))
																								{
																									if (!(abstractShaderNode is ShaderNode<GpuArray<bool>>))
																									{
																										if (!(abstractShaderNode is ShaderNode<GpuArray<int>>))
																										{
																											if (!(abstractShaderNode is ShaderNode<GpuArray<uint>>))
																											{
																												if (!(abstractShaderNode is ShaderNode<GpuArray<ushort>>))
																												{
																													if (!(abstractShaderNode is ShaderNode<GpuArray<Int2>>))
																													{
																														if (!(abstractShaderNode is ShaderNode<GpuArray<Int3>>))
																														{
																															if (!(abstractShaderNode is ShaderNode<GpuArray<Int4>>))
																															{
																																if (abstractShaderNode is ShaderNode<GpuVoid>)
																																{
																																	return 0;
																																}
																																throw new NotImplementedException("No name defined for type: " + abstractShaderNode.GetType().FullName);
																															}
																															return 16;
																														}
																														return 12;
																													}
																													return 8;
																												}
																												return 4;
																											}
																											return 4;
																										}
																										return 4;
																									}
																									return 1;
																								}
																								return 16;
																							}
																							return 36;
																						}
																						return 64;
																					}
																					return 16;
																				}
																				return 16;
																			}
																			return 12;
																		}
																		return 8;
																	}
																	return 4;
																}
																return 16;
															}
															return 12;
														}
														return 8;
													}
													return 4;
												}
												return 4;
											}
											return 4;
										}
										return 1;
									}
									return 16;
								}
								return 36;
							}
							return 64;
						}
						return 16;
					}
					return 16;
				}
				return 12;
			}
			return 8;
		}
		return 4;
	}

	public static int GetSizeInBytes(Type type)
	{
		if (!KnownByteSizes.TryGetValue(type, out var value))
		{
			return 0;
		}
		return value;
	}

	public static int GetSizeInBytes<T>(ShaderNode<T> shaderNode)
	{
		return GetSizeInBytes(typeof(T));
	}

	public static int GetSizeInBytes<T>()
	{
		return GetSizeInBytes(typeof(T));
	}

	public static PixelFormat GetPixelFormat(AbstractShaderNode abstractShaderNode)
	{
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		if (!(abstractShaderNode is ShaderNode<float>))
		{
			if (!(abstractShaderNode is ShaderNode<Vector2>))
			{
				if (!(abstractShaderNode is ShaderNode<Vector3>))
				{
					if (!(abstractShaderNode is ShaderNode<Vector4>))
					{
						if (!(abstractShaderNode is ShaderNode<Color4>))
						{
							if (!(abstractShaderNode is ShaderNode<bool>))
							{
								if (!(abstractShaderNode is ShaderNode<int>))
								{
									if (!(abstractShaderNode is ShaderNode<Int2>))
									{
										if (!(abstractShaderNode is ShaderNode<Int3>))
										{
											if (!(abstractShaderNode is ShaderNode<Int4>))
											{
												if (!(abstractShaderNode is ShaderNode<uint>))
												{
													if (!(abstractShaderNode is ShaderNode<ushort>))
													{
														if (!(abstractShaderNode is ShaderNode<GpuArray<float>>))
														{
															if (!(abstractShaderNode is ShaderNode<GpuArray<Vector2>>))
															{
																if (!(abstractShaderNode is ShaderNode<GpuArray<Vector3>>))
																{
																	if (!(abstractShaderNode is ShaderNode<GpuArray<Vector4>>))
																	{
																		if (!(abstractShaderNode is ShaderNode<GpuArray<Color4>>))
																		{
																			if (!(abstractShaderNode is ShaderNode<GpuArray<bool>>))
																			{
																				if (!(abstractShaderNode is ShaderNode<GpuArray<int>>))
																				{
																					if (!(abstractShaderNode is ShaderNode<GpuArray<Int2>>))
																					{
																						if (!(abstractShaderNode is ShaderNode<GpuArray<Int3>>))
																						{
																							if (!(abstractShaderNode is ShaderNode<GpuArray<Int4>>))
																							{
																								if (!(abstractShaderNode is ShaderNode<GpuArray<uint>>))
																								{
																									if (abstractShaderNode is ShaderNode<GpuArray<ushort>>)
																									{
																										return (PixelFormat)57;
																									}
																									throw new NotImplementedException("No name defined for type: " + abstractShaderNode.GetType().FullName);
																								}
																								return (PixelFormat)42;
																							}
																							return (PixelFormat)4;
																						}
																						return (PixelFormat)8;
																					}
																					return (PixelFormat)18;
																				}
																				return (PixelFormat)43;
																			}
																			return (PixelFormat)66;
																		}
																		return (PixelFormat)2;
																	}
																	return (PixelFormat)2;
																}
																return (PixelFormat)6;
															}
															return (PixelFormat)16;
														}
														return (PixelFormat)41;
													}
													return (PixelFormat)57;
												}
												return (PixelFormat)42;
											}
											return (PixelFormat)4;
										}
										return (PixelFormat)8;
									}
									return (PixelFormat)18;
								}
								return (PixelFormat)43;
							}
							return (PixelFormat)66;
						}
						return (PixelFormat)2;
					}
					return (PixelFormat)2;
				}
				return (PixelFormat)6;
			}
			return (PixelFormat)16;
		}
		return (PixelFormat)41;
	}

	public static PixelFormat GetPixelFormat(Type type)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (!KnownPixelFormats.TryGetValue(type, out var value))
		{
			return (PixelFormat)0;
		}
		return value;
	}

	public static PixelFormat GetPixelFormat<T>(ShaderNode<T> shaderNode)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return GetPixelFormat(typeof(T));
	}

	public static PixelFormat GetPixelFormat<T>()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return GetPixelFormat(typeof(T));
	}

	public static Type GetType(PixelFormat format)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		if (!PixelFormatTypes.TryGetValue(format, out var value))
		{
			return typeof(float);
		}
		return value;
	}

	public static bool IsIntType(Type type)
	{
		if (!(type == typeof(int)) && !(type == typeof(uint)) && !(type == typeof(ushort)) && !(type == typeof(Int2)) && !(type == typeof(Int3)))
		{
			return type == typeof(Int4);
		}
		return true;
	}

	public static bool IsIntType<T>()
	{
		return IsIntType(typeof(T));
	}

	public static bool IsIntType<T>(ShaderNode<T> shaderNode)
	{
		return IsIntType<T>();
	}
}
