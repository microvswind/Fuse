using System;
using Stride.Core.Mathematics;

namespace Fuse.Rendering;

public static class MathUtil
{
	public static Matrix CreateCustomProjectionMatrix(float horizontalFovDegrees, float orthographicHeight, float nearPlane, float farPlane)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		float num = MathF.Tan(horizontalFovDegrees / 2f * (float)System.Math.PI / 180f) * nearPlane;
		float num2 = 0f - num;
		float num3 = orthographicHeight / 2f;
		float num4 = 0f - num3;
		float num5 = 2f * nearPlane / (num - num2);
		float num6 = 2f / (num3 - num4);
		float num7 = farPlane / (farPlane - nearPlane);
		float num8 = (0f - nearPlane) * num7;
		return new Matrix(num5, 0f, 0f, 0f, 0f, num6, 0f, 0f, (num + num2) / (num - num2), (num3 + num4) / (num3 - num4), num7, 1f, 0f, 0f, num8, 0f);
	}

	public static Matrix CreateHybridProjectionMatrix(float horizontalFovDegrees, float orthographicHeight, float nearPlane, float farPlane)
	{
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		if (nearPlane <= 0f || farPlane <= nearPlane)
		{
			throw new ArgumentException("Invalid near/far plane values");
		}
		if (horizontalFovDegrees <= 0f || horizontalFovDegrees >= 180f)
		{
			throw new ArgumentException("FOV must be between 0 and 180 degrees");
		}
		float num = MathF.Tan(horizontalFovDegrees * 0.5f * (float)System.Math.PI / 180f) * nearPlane;
		float num2 = 0f - num;
		float num3 = orthographicHeight * 0.5f;
		float num4 = 0f - num3;
		float num5 = 2f * nearPlane / (num - num2);
		float num6 = 2f / (num3 - num4);
		float num7 = (num + num2) / (num - num2);
		float num8 = (num3 + num4) / (num3 - num4);
		float num9 = (0f - (farPlane + nearPlane)) / (farPlane - nearPlane);
		float num10 = (0f - 2f * farPlane * nearPlane) / (farPlane - nearPlane);
		return new Matrix(num5, 0f, 0f, 0f, 0f, num6, 0f, 0f, num7, num8, num9, -1f, 0f, 0f, num10, 0f);
	}
}
