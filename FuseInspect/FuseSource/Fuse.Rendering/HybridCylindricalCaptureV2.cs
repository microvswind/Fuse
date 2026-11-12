using System;
using Stride.Core.Mathematics;

namespace Fuse.Rendering;

public static class HybridCylindricalCaptureV2
{
	public static void CreateCylindricalSliceMatrix2(int numberOfCameras, int cameraIndex, float cylinderHeight, float nearPlane, float farPlane, out Matrix view, out Matrix projection)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)System.Math.PI * 2f / (float)numberOfCameras;
		float x = num * 0.5f;
		float num2 = (float)cameraIndex * num;
		float num3 = (0f - nearPlane) * MathF.Tan(x);
		float num4 = nearPlane * MathF.Tan(x);
		float num5 = cylinderHeight * 0.5f;
		float num6 = 0f - num5;
		projection = new Matrix(2f * nearPlane / (num4 - num3), 0f, 0f, 0f, 0f, 2f / (num5 - num6), 0f, 0f, (num4 + num3) / (num4 - num3), (num5 + num6) / (num5 - num6), farPlane / (farPlane - nearPlane), 1f, 0f, 0f, (0f - nearPlane * farPlane) / (farPlane - nearPlane), 0f);
		view = Matrix.RotationY(0f - num2);
	}

	public static void CreateCylindricalSliceMatrix(int numberOfCameras, int cameraIndex, float cylinderHeight, float nearPlaneDistance, float farPlaneDistance, out Matrix view, out Matrix projection)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)System.Math.PI * 2f / (float)numberOfCameras;
		float num2 = num * 0.5f;
		float num3 = (float)cameraIndex * num;
		view = Matrix.RotationY(0f - num3);
		float x = num3 - num2;
		float x2 = num3 + num2;
		Vector3 val = new Vector3(MathF.Sin(x), 0f, MathF.Cos(x));
		Vector3 val2 = new Vector3(MathF.Sin(x2), 0f, MathF.Cos(x2));
		Plane plane = new Plane(val, 0f);
		Plane plane2 = new Plane(val2, 0f);
		float num4 = cylinderHeight * 0.5f;
		new Plane(new Vector3(0f, 1f, 0f), num4);
		new Plane(new Vector3(0f, -1f, 0f), num4);
		Vector3 val3 = new Vector3(MathF.Sin(num3), 0f, MathF.Cos(num3));
		Plane plane3 = new Plane(val3, nearPlaneDistance);
		Plane plane4 = new Plane(val3, farPlaneDistance);
		Matrix val4 = Matrix.Invert(view);
		Matrix invT = Matrix.Transpose(ref val4);
		Plane planeCam = TransformPlane(plane3, invT);
		Plane planeCam2 = TransformPlane(plane4, invT);
		Plane planeCam3 = TransformPlane(plane, invT);
		Plane planeCam4 = TransformPlane(plane2, invT);
		float num5 = IntersectPlaneAtCameraZ(planeCam);
		float num6 = IntersectPlaneAtCameraZ(planeCam2);
		if (num5 > num6)
		{
			float num7 = num5;
			num5 = num6;
			num6 = num7;
		}
		float left = IntersectPlaneXatZ(planeCam3, num5);
		float right = IntersectPlaneXatZ(planeCam4, num5);
		float bottom = 0f - num4;
		float top = num4;
		projection = CreateOffAxisPerspectiveRH(left, right, bottom, top, num5, num6);
	}

	private static Plane TransformPlane(Plane plane, Matrix invT)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Vector4 val = Vector4.Transform(new Vector4(plane.Normal, 0f - plane.D), invT);
		Plane result = new Plane(new Vector3(val.X, val.Y, val.Z), 0f - val.W);
		float num = result.Normal.Length();
		if (MathF.Abs(num) > 1E-07f)
		{
			ref Vector3 normal = ref result.Normal;
			normal = (ref normal) / num;
			result.D /= num;
		}
		return result;
	}

	private static float IntersectPlaneAtCameraZ(Plane planeCam)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		float z = planeCam.Normal.Z;
		float d = planeCam.D;
		if (MathF.Abs(z) < 1E-07f)
		{
			return 100000f;
		}
		return d / z;
	}

	private static float IntersectPlaneXatZ(Plane planeCam, float fixedZ)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		float x = planeCam.Normal.X;
		float z = planeCam.Normal.Z;
		return (planeCam.D - z * fixedZ) / x;
	}

	private static Matrix CreateOffAxisPerspectiveRH(float left, float right, float bottom, float top, float nearZ, float farZ)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		float num = 2f * nearZ;
		return new Matrix
		{
			M11 = num / (right - left),
			M12 = 0f,
			M13 = 0f,
			M14 = 0f,
			M21 = 0f,
			M22 = num / (top - bottom),
			M23 = 0f,
			M24 = 0f,
			M31 = (right + left) / (right - left),
			M32 = (top + bottom) / (top - bottom),
			M33 = farZ / (farZ - nearZ),
			M34 = 1f,
			M41 = 0f,
			M42 = 0f,
			M43 = (0f - nearZ) * (farZ / (farZ - nearZ)),
			M44 = 0f
		};
	}
}
