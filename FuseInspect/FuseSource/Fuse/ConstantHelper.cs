using System;
using Stride.Core.Mathematics;

namespace Fuse;

public static class ConstantHelper
{
	public static ConstantValue<T> FromFloat<T>(float theValue)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		if (typeof(T) == typeof(float))
		{
			return new ConstantValue<T>((T)Convert.ChangeType(theValue, typeof(float)));
		}
		if (typeof(T) == typeof(Vector2))
		{
			return new ConstantValue<T>((T)Convert.ChangeType((object)new Vector2(theValue, theValue), typeof(Vector2)));
		}
		if (typeof(T) == typeof(Vector3))
		{
			return new ConstantValue<T>((T)Convert.ChangeType((object)new Vector3(theValue, theValue, theValue), typeof(Vector3)));
		}
		if (typeof(T) == typeof(Vector4))
		{
			return new ConstantValue<T>((T)Convert.ChangeType((object)new Vector4(theValue, theValue, theValue, theValue), typeof(Vector4)));
		}
		if (typeof(T) == typeof(Color4))
		{
			return new ConstantValue<T>((T)Convert.ChangeType((object)new Color4(theValue, theValue, theValue, theValue), typeof(Color4)));
		}
		int num = (int)theValue;
		if (typeof(T) == typeof(int))
		{
			return new ConstantValue<T>((T)Convert.ChangeType(num, typeof(int)));
		}
		if (typeof(T) == typeof(Int2))
		{
			return new ConstantValue<T>((T)Convert.ChangeType((object)new Int2(num, num), typeof(Int2)));
		}
		if (typeof(T) == typeof(Int3))
		{
			return new ConstantValue<T>((T)Convert.ChangeType((object)new Int3(num, num, num), typeof(Int3)));
		}
		if (typeof(T) == typeof(Int4))
		{
			return new ConstantValue<T>((T)Convert.ChangeType((object)new Int4(num, num, num, num), typeof(Int4)));
		}
		bool flag = theValue > 0f;
		if (typeof(T) == typeof(bool))
		{
			return new ConstantValue<T>((T)Convert.ChangeType(flag, typeof(bool)));
		}
		return null;
	}

	public static AbstractShaderNode AbstractFromFloat(Type theType, float theValue)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		if (theType == typeof(float))
		{
			return new ConstantValue<float>(theValue);
		}
		if (theType == typeof(Vector2))
		{
			return new ConstantValue<Vector2>(new Vector2(theValue, theValue));
		}
		if (theType == typeof(Vector3))
		{
			return new ConstantValue<Vector3>(new Vector3(theValue, theValue, theValue));
		}
		if (theType == typeof(Vector4))
		{
			return new ConstantValue<Vector4>(new Vector4(theValue, theValue, theValue, theValue));
		}
		int num = (int)theValue;
		if (theType == typeof(int))
		{
			return new ConstantValue<int>(num);
		}
		if (theType == typeof(Int2))
		{
			return new ConstantValue<Int2>(new Int2(num, num));
		}
		if (theType == typeof(Int3))
		{
			return new ConstantValue<Int3>(new Int3(num, num, num));
		}
		if (theType == typeof(Int4))
		{
			return new ConstantValue<Int4>(new Int4(num, num, num, num));
		}
		return null;
	}
}
