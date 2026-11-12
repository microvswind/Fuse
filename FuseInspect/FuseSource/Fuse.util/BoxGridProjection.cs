using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Stride.Core.Mathematics;

namespace Fuse.util;

public static class BoxGridProjection
{
	public struct SurfaceData
	{
		public Vector4[] PositionsAndType;

		public Vector4[] NormalsAndSize;

		public Vector4[] Dimensions;

		public Vector4[] ColorsAndScale;
	}

	public struct LayoutInfo
	{
		public Vector4[] FaceLayoutInfo;

		public Vector4 CrossLayoutInfo;

		public Vector4 RoomCenter;

		public Vector4 RoomDimensions;
	}

	private const float POINT_ON_PLANE_TOLERANCE = 0.1f;

	public static SurfaceData CalculateSurfaceData(Vector3 roomCenter, Vector3 roomDimensions)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		SurfaceData result = new SurfaceData
		{
			PositionsAndType = new Vector4[6],
			NormalsAndSize = new Vector4[6],
			Dimensions = new Vector4[6],
			ColorsAndScale = new Vector4[6]
		};
		float x = roomDimensions.X;
		float y = roomDimensions.Y;
		float z = roomDimensions.Z;
		result.PositionsAndType[0] = new Vector4(roomCenter.X, roomCenter.Y - y * 0.5f, roomCenter.Z, 0f);
		result.NormalsAndSize[0] = new Vector4(0f, 1f, 0f, y);
		result.Dimensions[0] = new Vector4(x, y, z, 0f);
		result.ColorsAndScale[0] = new Vector4(0f, 1f, 0f, 1f);
		result.PositionsAndType[1] = new Vector4(roomCenter.X, roomCenter.Y + y * 0.5f, roomCenter.Z, 1f);
		result.NormalsAndSize[1] = new Vector4(0f, -1f, 0f, y);
		result.Dimensions[1] = new Vector4(x, y, z, 0f);
		result.ColorsAndScale[1] = new Vector4(1f, 1f, 0f, 1f);
		result.PositionsAndType[2] = new Vector4(roomCenter.X + x * 0.5f, roomCenter.Y, roomCenter.Z, 2f);
		result.NormalsAndSize[2] = new Vector4(-1f, 0f, 0f, x);
		result.Dimensions[2] = new Vector4(x, y, z, 0f);
		result.ColorsAndScale[2] = new Vector4(1f, 0f, 1f, 1f);
		result.PositionsAndType[3] = new Vector4(roomCenter.X, roomCenter.Y, roomCenter.Z + z * 0.5f, 3f);
		result.NormalsAndSize[3] = new Vector4(0f, 0f, -1f, z);
		result.Dimensions[3] = new Vector4(x, y, z, 0f);
		result.ColorsAndScale[3] = new Vector4(0f, 1f, 1f, 1f);
		result.PositionsAndType[4] = new Vector4(roomCenter.X - x * 0.5f, roomCenter.Y, roomCenter.Z, 4f);
		result.NormalsAndSize[4] = new Vector4(1f, 0f, 0f, x);
		result.Dimensions[4] = new Vector4(x, y, z, 0f);
		result.ColorsAndScale[4] = new Vector4(1f, 0f, 0f, 1f);
		result.PositionsAndType[5] = new Vector4(roomCenter.X, roomCenter.Y, roomCenter.Z - z * 0.5f, 5f);
		result.NormalsAndSize[5] = new Vector4(0f, 0f, 1f, z);
		result.Dimensions[5] = new Vector4(x, y, z, 0f);
		result.ColorsAndScale[5] = new Vector4(0f, 0f, 1f, 1f);
		return result;
	}

	public static LayoutInfo CalculateLayoutData(Vector3 roomCenter, Vector3 roomDimensions)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		LayoutInfo result = new LayoutInfo
		{
			FaceLayoutInfo = new Vector4[6],
			CrossLayoutInfo = Vector4.Zero,
			RoomCenter = Vector4.Zero,
			RoomDimensions = Vector4.Zero
		};
		float x = roomDimensions.X;
		float y = roomDimensions.Y;
		float z = roomDimensions.Z;
		float num = 2f * x + 2f * z;
		float num2 = 2f * z + y;
		float x2 = 4f / num;
		float y2 = 3f / num2;
		float num3 = float.Min(x2, y2);
		float num4 = z * num3;
		float num5 = x * num3;
		float num6 = z * num3;
		float num7 = x * num3;
		float num8 = z * num3;
		float num9 = y * num3;
		float num10 = z * num3;
		float num11 = 0f;
		float num12 = num4;
		float num13 = num4 + num5;
		float num14 = num4 + num5 + num6;
		float num15 = 0f;
		float num16 = num8;
		float num17 = num8 + num9;
		result.FaceLayoutInfo[1] = new Vector4(num12, num17, num5, num10);
		result.FaceLayoutInfo[0] = new Vector4(num12, num15, num5, num8);
		result.FaceLayoutInfo[2] = new Vector4(num13, num16, num6, num9);
		result.FaceLayoutInfo[3] = new Vector4(num14, num16, num7, num9);
		result.FaceLayoutInfo[4] = new Vector4(num11, num16, num4, num9);
		result.FaceLayoutInfo[5] = new Vector4(num12, num16, num5, num9);
		float num18 = num4 + num5 + num6 + num7;
		float num19 = num8 + num9 + num10;
		result.CrossLayoutInfo = new Vector4(num18, num19, 0f, 0f);
		result.RoomCenter = new Vector4(roomCenter.X, roomCenter.Y, roomCenter.Z, 0f);
		result.RoomDimensions = new Vector4(x, y, z, 0f);
		return result;
	}

	private static void ExtractPlanesFromMatrix(Matrix viewProj, out Plane left, out Plane right, out Plane top, out Plane bottom, out Plane near, out Plane far)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		left = new Plane(viewProj.M14 + viewProj.M11, viewProj.M24 + viewProj.M21, viewProj.M34 + viewProj.M31, viewProj.M44 + viewProj.M41);
		Plane.Normalize(ref left, ref left);
		right = new Plane(viewProj.M14 - viewProj.M11, viewProj.M24 - viewProj.M21, viewProj.M34 - viewProj.M31, viewProj.M44 - viewProj.M41);
		Plane.Normalize(ref right, ref right);
		top = new Plane(viewProj.M14 - viewProj.M12, viewProj.M24 - viewProj.M22, viewProj.M34 - viewProj.M32, viewProj.M44 - viewProj.M42);
		Plane.Normalize(ref top, ref top);
		bottom = new Plane(viewProj.M14 + viewProj.M12, viewProj.M24 + viewProj.M22, viewProj.M34 + viewProj.M32, viewProj.M44 + viewProj.M42);
		Plane.Normalize(ref bottom, ref bottom);
		near = new Plane(viewProj.M14 + viewProj.M13, viewProj.M24 + viewProj.M23, viewProj.M34 + viewProj.M33, viewProj.M44 + viewProj.M43);
		Plane.Normalize(ref near, ref near);
		far = new Plane(viewProj.M14 - viewProj.M13, viewProj.M24 - viewProj.M23, viewProj.M34 - viewProj.M33, viewProj.M44 - viewProj.M43);
		Plane.Normalize(ref far, ref far);
	}

	private static Vector3[] GetBoxCorners(Vector3 center, Vector3 halfDim)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		Vector3[] array = new Vector3[8];
		Vector3 val = new Vector3(0f - halfDim.X, 0f - halfDim.Y, 0f - halfDim.Z);
		array[0] = (ref center) + (ref val);
		Vector3 val2 = new Vector3(halfDim.X, 0f - halfDim.Y, 0f - halfDim.Z);
		array[1] = (ref center) + (ref val2);
		Vector3 val3 = new Vector3(halfDim.X, halfDim.Y, 0f - halfDim.Z);
		array[2] = (ref center) + (ref val3);
		Vector3 val4 = new Vector3(0f - halfDim.X, halfDim.Y, 0f - halfDim.Z);
		array[3] = (ref center) + (ref val4);
		Vector3 val5 = new Vector3(0f - halfDim.X, 0f - halfDim.Y, halfDim.Z);
		array[4] = (ref center) + (ref val5);
		Vector3 val6 = new Vector3(halfDim.X, 0f - halfDim.Y, halfDim.Z);
		array[5] = (ref center) + (ref val6);
		Vector3 val7 = new Vector3(halfDim.X, halfDim.Y, halfDim.Z);
		array[6] = (ref center) + (ref val7);
		Vector3 val8 = new Vector3(0f - halfDim.X, halfDim.Y, halfDim.Z);
		array[7] = (ref center) + (ref val8);
		return array;
	}

	private static List<Vector3>[] GetTransformedBoxFacePolygons(Vector3 center, Vector3 halfDim, ref Matrix transform)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		Vector3[] boxCorners = GetBoxCorners(center, halfDim);
		Vector3[] array = new Vector3[8];
		for (int i = 0; i < 8; i++)
		{
			array[i] = Vector3.TransformCoordinate(boxCorners[i], transform);
		}
		List<Vector3>[] array2 = new List<Vector3>[6];
		int num = 4;
		List<Vector3> list = new List<Vector3>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<Vector3> span = CollectionsMarshal.AsSpan(list);
		int num2 = 0;
		span[num2] = array[0];
		num2++;
		span[num2] = array[4];
		num2++;
		span[num2] = array[5];
		num2++;
		span[num2] = array[1];
		array2[0] = list;
		num2 = 4;
		List<Vector3> list2 = new List<Vector3>(num2);
		CollectionsMarshal.SetCount(list2, num2);
		span = CollectionsMarshal.AsSpan(list2);
		num = 0;
		span[num] = array[3];
		num++;
		span[num] = array[7];
		num++;
		span[num] = array[6];
		num++;
		span[num] = array[2];
		array2[1] = list2;
		num = 4;
		List<Vector3> list3 = new List<Vector3>(num);
		CollectionsMarshal.SetCount(list3, num);
		span = CollectionsMarshal.AsSpan(list3);
		num2 = 0;
		span[num2] = array[1];
		num2++;
		span[num2] = array[5];
		num2++;
		span[num2] = array[6];
		num2++;
		span[num2] = array[2];
		array2[2] = list3;
		num2 = 4;
		List<Vector3> list4 = new List<Vector3>(num2);
		CollectionsMarshal.SetCount(list4, num2);
		span = CollectionsMarshal.AsSpan(list4);
		num = 0;
		span[num] = array[5];
		num++;
		span[num] = array[4];
		num++;
		span[num] = array[7];
		num++;
		span[num] = array[6];
		array2[3] = list4;
		num = 4;
		List<Vector3> list5 = new List<Vector3>(num);
		CollectionsMarshal.SetCount(list5, num);
		span = CollectionsMarshal.AsSpan(list5);
		num2 = 0;
		span[num2] = array[4];
		num2++;
		span[num2] = array[0];
		num2++;
		span[num2] = array[3];
		num2++;
		span[num2] = array[7];
		array2[4] = list5;
		num2 = 4;
		List<Vector3> list6 = new List<Vector3>(num2);
		CollectionsMarshal.SetCount(list6, num2);
		span = CollectionsMarshal.AsSpan(list6);
		num = 0;
		span[num] = array[0];
		num++;
		span[num] = array[1];
		num++;
		span[num] = array[2];
		num++;
		span[num] = array[3];
		array2[5] = list6;
		return array2;
	}

	private static List<Vector3> ClipPolygonAgainstPlane(List<Vector3> polygonVertices, Plane clippingPlane)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		List<Vector3> list = new List<Vector3>();
		if (polygonVertices.Count < 3)
		{
			return list;
		}
		for (int i = 0; i < polygonVertices.Count; i++)
		{
			Vector3 val = polygonVertices[i];
			Vector3 val2 = polygonVertices[(i + 1) % polygonVertices.Count];
			float num = Plane.DotCoordinate(clippingPlane, val);
			float num2 = Plane.DotCoordinate(clippingPlane, val2);
			bool flag = num >= 0f;
			bool flag2 = num2 >= 0f;
			if (flag & flag2)
			{
				list.Add(val2);
			}
			else if (flag && !flag2)
			{
				Vector3 val3 = (ref val2) - (ref val);
				float value = (0f - num) / Vector3.Dot(val3, clippingPlane.Normal);
				Vector3 val4 = (ref val3) * System.Math.Clamp(value, 0f, 1f);
				list.Add((ref val) + (ref val4));
			}
			else if (!flag & flag2)
			{
				Vector3 val5 = (ref val2) - (ref val);
				float value2 = (0f - num) / Vector3.Dot(val5, clippingPlane.Normal);
				Vector3 val4 = (ref val5) * System.Math.Clamp(value2, 0f, 1f);
				list.Add((ref val) + (ref val4));
				list.Add(val2);
			}
		}
		return list;
	}

	public static List<List<Vector3>> GetClippedBoxFacePolygonsFromMatrix(Matrix viewProj, Vector3 boxCenter, Vector3 boxDimensions, Matrix boxWorldTransform)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		Vector3 halfDim = (ref boxDimensions) * 0.5f;
		List<Vector3>[] transformedBoxFacePolygons = GetTransformedBoxFacePolygons(boxCenter, halfDim, ref boxWorldTransform);
		ExtractPlanesFromMatrix(viewProj, out var left, out var right, out var top, out var bottom, out var near, out var far);
		Plane[] array = new Plane[6] { near, far, left, right, top, bottom };
		List<List<Vector3>> list = new List<List<Vector3>>();
		for (int i = 0; i < 6; i++)
		{
			List<Vector3> list2 = transformedBoxFacePolygons[i];
			for (int j = 0; j < 6; j++)
			{
				if (list2.Count == 0)
				{
					break;
				}
				list2 = ClipPolygonAgainstPlane(list2, array[j]);
			}
			list.Add(list2);
		}
		return list;
	}

	public static Matrix CalculateQuadTransform(Vector3[] points)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		if (points != null && points.Length < 3)
		{
			return Matrix.Identity;
		}
		Vector3 val = (ref points[1]) - (ref points[0]);
		Vector3 val2 = (ref points[2]) - (ref points[0]);
		Vector3 val3 = Vector3.Cross(ref val, ref val2);
		if (val3.LengthSquared() < 1E-06f)
		{
			return Matrix.Identity;
		}
		val3.Normalize();
		for (int i = 3; i < points.Length; i++)
		{
			if (System.Math.Abs(Vector3.Dot((ref points[i]) - (ref points[0]), val3)) >= 1E-06f)
			{
				return Matrix.Identity;
			}
		}
		Vector3 val4 = Vector3.Zero;
		Vector3[] array = points;
		for (int j = 0; j < array.Length; j++)
		{
			Vector3 val5 = array[j];
			val4 = (ref val4) + (ref val5);
		}
		val4 = (ref val4) / (float)points.Length;
		List<Vector3> list = new List<Vector3>();
		int num = points.Length;
		for (int k = 0; k < num; k++)
		{
			Vector3 item = (ref points[(k + 1) % num]) - (ref points[k]);
			if (item.LengthSquared() > 1E-06f)
			{
				item.Normalize();
				list.Add(item);
			}
		}
		array = points;
		for (int j = 0; j < array.Length; j++)
		{
			Vector3 val6 = array[j];
			Vector3 item2 = (ref val6) - (ref val4);
			if (item2.LengthSquared() > 1E-06f)
			{
				item2.Normalize();
				list.Add(item2);
			}
		}
		int num2 = MathUtil.Clamp(num * (num - 1) / 2, 0, 10);
		int num3 = 0;
		for (int l = 0; l < num; l++)
		{
			if (num3 >= num2)
			{
				break;
			}
			for (int m = l + 2; m < num; m++)
			{
				if (num3 >= num2)
				{
					break;
				}
				Vector3 item3 = (ref points[m]) - (ref points[l]);
				if (item3.LengthSquared() > 1E-06f)
				{
					item3.Normalize();
					list.Add(item3);
					num3++;
				}
			}
		}
		if (list.Count == 0)
		{
			return Matrix.Identity;
		}
		float num4 = float.MaxValue;
		Matrix val7 = Matrix.Identity;
		Vector2 val8 = Vector2.Zero;
		Vector3 val9 = Vector3.Zero;
		Matrix val16;
		foreach (Vector3 item4 in list)
		{
			Vector3 current = item4;
			Vector3 val10 = Vector3.Cross(ref val3, ref current);
			val10.Normalize();
			float num5 = float.MaxValue;
			float num6 = float.MinValue;
			float num7 = float.MaxValue;
			float num8 = float.MinValue;
			array = points;
			for (int j = 0; j < array.Length; j++)
			{
				Vector3 val11 = array[j];
				Vector3 val12 = (ref val11) - (ref val4);
				float val13 = Vector3.Dot(val12, current);
				float val14 = Vector3.Dot(val12, val10);
				num5 = System.Math.Min(num5, val13);
				num6 = System.Math.Max(num6, val13);
				num7 = System.Math.Min(num7, val14);
				num8 = System.Math.Max(num8, val14);
			}
			float num9 = num6 - num5;
			float num10 = num8 - num7;
			float num11 = num9 * num10;
			if (num11 < num4)
			{
				num4 = num11;
				val8 = new Vector2(num9, num10);
				float num12 = (num5 + num6) * 0.5f;
				float num13 = (num7 + num8) * 0.5f;
				val = num12 * (ref current);
				val2 = (ref val4) + (ref val);
				Vector3 val15 = num13 * (ref val10);
				val9 = (ref val2) + (ref val15);
				val16 = default;
				val16.Row1 = new Vector4(current, 0f);
				val16.Row2 = new Vector4(val10, 0f);
				val16.Row3 = new Vector4(val3, 0f);
				val16.Row4 = new Vector4(0f, 0f, 0f, 1f);
				val7 = val16;
			}
		}
		if (val8.X < 1E-06f || val8.Y < 1E-06f)
		{
			return Matrix.Identity;
		}
		Matrix val17 = Matrix.Scaling(val8.X, val8.Y, 1f);
		Matrix val18 = Matrix.Translation(val9);
		val16 = (ref val17) * (ref val7);
		return (ref val16) * (ref val18);
	}

	internal static bool RayPlaneIntersection(Vector3 rayOrigin, Vector3 rayDir, Vector3 planePos, Vector3 planeNormal, out Vector3 intersection)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		intersection = Vector3.Zero;
		float num = Vector3.Dot(rayDir, planeNormal);
		if (System.Math.Abs(num) < 1E-06f)
		{
			return false;
		}
		float num2 = Vector3.Dot((ref planePos) - (ref rayOrigin), planeNormal) / num;
		if (num2 < -0.1f)
		{
			return false;
		}
		Vector3 val = (ref rayDir) * num2;
		intersection = (ref rayOrigin) + (ref val);
		return true;
	}

	internal static bool IsPointInRectangle(Vector3 position, Vector3 rectCenter, Vector3 rectDimensions, Vector3 normalAxis)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = (ref position) - (ref rectCenter);
		float num = rectDimensions.X * 0.5f;
		float num2 = rectDimensions.Y * 0.5f;
		float num3 = rectDimensions.Z * 0.5f;
		if (System.Math.Abs(normalAxis.Y) >= 0.99f)
		{
			Vector3 unitZ = Vector3.UnitZ;
			Vector3 unitX = Vector3.UnitX;
			if (System.Math.Abs(Vector3.Dot(val, unitX)) <= num + 0.1f)
			{
				return System.Math.Abs(Vector3.Dot(val, unitZ)) <= num3 + 0.1f;
			}
			return false;
		}
		if (System.Math.Abs(normalAxis.X) >= 0.99f)
		{
			Vector3 unitZ = Vector3.UnitY;
			Vector3 unitX = Vector3.UnitZ;
			if (System.Math.Abs(Vector3.Dot(val, unitX)) <= num3 + 0.1f)
			{
				return System.Math.Abs(Vector3.Dot(val, unitZ)) <= num2 + 0.1f;
			}
			return false;
		}
		if (System.Math.Abs(normalAxis.Z) >= 0.99f)
		{
			Vector3 unitZ = Vector3.UnitY;
			Vector3 unitX = Vector3.UnitX;
			if (System.Math.Abs(Vector3.Dot(val, unitX)) <= num + 0.1f)
			{
				return System.Math.Abs(Vector3.Dot(val, unitZ)) <= num2 + 0.1f;
			}
			return false;
		}
		return false;
	}

	public static List<Vector2> ProjectWorldPointsByFaceThenParticle(IReadOnlyList<Vector3> worldPoints, Vector3 viewerPosition, SurfaceData surfaceData, LayoutInfo layoutInfo, bool[] includeFaces = null)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		List<Vector2> list = new List<Vector2>();
		if (worldPoints == null || worldPoints.Count == 0)
		{
			return list;
		}
		if (includeFaces == null)
		{
			includeFaces = new bool[6] { true, true, true, true, true, true };
		}
		else if (includeFaces.Length != 6)
		{
			includeFaces = new bool[6] { true, true, true, true, true, true };
		}
		Vector3 rectDimensions = new Vector3(layoutInfo.RoomDimensions.X, layoutInfo.RoomDimensions.Y, layoutInfo.RoomDimensions.Z);
		HashSet<(int, int)> hashSet = new HashSet<(int, int)>();
		for (int i = 0; i < worldPoints.Count; i++)
		{
			Vector3 val = worldPoints[i];
			Vector3 val2 = (ref val) - (ref viewerPosition);
			if (val2.LengthSquared() < 1E-08f)
			{
				continue;
			}
			val2 = Vector3.Normalize(val2);
			float num = float.MaxValue;
			Vector3 val3 = Vector3.Zero;
			int num2 = -1;
			for (int j = 0; j < 6; j++)
			{
				if (!includeFaces[j])
				{
					continue;
				}
				Vector3 val4 = new Vector3(surfaceData.PositionsAndType[j].X, surfaceData.PositionsAndType[j].Y, surfaceData.PositionsAndType[j].Z);
				Vector3 val5 = new Vector3(surfaceData.NormalsAndSize[j].X, surfaceData.NormalsAndSize[j].Y, surfaceData.NormalsAndSize[j].Z);
				if (RayPlaneIntersection(viewerPosition, val2, val4, val5, out var intersection) && IsPointInRectangle(intersection, val4, rectDimensions, val5))
				{
					Vector3 val6 = (ref intersection) - (ref viewerPosition);
					float num3 = val6.LengthSquared();
					if (num3 < num)
					{
						num = num3;
						val3 = intersection;
						num2 = j;
					}
				}
			}
			if (num2 == -1)
			{
				continue;
			}
			for (int k = 0; k < 6; k++)
			{
				if (includeFaces[k] && !hashSet.Contains((i, k)))
				{
					Vector3 rectCenter = new Vector3(surfaceData.PositionsAndType[k].X, surfaceData.PositionsAndType[k].Y, surfaceData.PositionsAndType[k].Z);
					Vector3 val7 = new Vector3(surfaceData.NormalsAndSize[k].X, surfaceData.NormalsAndSize[k].Y, surfaceData.NormalsAndSize[k].Z);
					val7.Normalize();
					if (System.Math.Abs(Vector3.Dot((ref val3) - (ref rectCenter), val7)) <= 0.1f && IsPointInRectangle(val3, rectCenter, rectDimensions, val7))
					{
						Vector2 item = MapToTexture(val3, k, layoutInfo);
						list.Add(item);
						hashSet.Add((i, k));
					}
				}
			}
		}
		return list;
	}

	internal static Vector2 MapToTexture(Vector3 pointOnSurface, int surfaceIndex, LayoutInfo layoutInfo)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = new Vector3(layoutInfo.RoomCenter.X, layoutInfo.RoomCenter.Y, layoutInfo.RoomCenter.Z);
		Vector3 val2 = new Vector3(layoutInfo.RoomDimensions.X, layoutInfo.RoomDimensions.Y, layoutInfo.RoomDimensions.Z);
		Vector3 val3 = (ref val2) * 0.5f;
		Vector3 val4 = (ref val) - (ref val3);
		Vector3 val5 = (ref pointOnSurface) - (ref val4);
		float num = 0f;
		float num2 = 0f;
		switch (surfaceIndex)
		{
		case 0:
			num = val5.X / val2.X;
			num2 = 1f - val5.Z / val2.Z;
			break;
		case 1:
			num = val5.X / val2.X;
			num2 = val5.Z / val2.Z;
			break;
		case 2:
			num = val5.Z / val2.Z;
			num2 = val5.Y / val2.Y;
			break;
		case 3:
			num = 1f - val5.X / val2.X;
			num2 = val5.Y / val2.Y;
			break;
		case 4:
			num = 1f - val5.Z / val2.Z;
			num2 = val5.Y / val2.Y;
			break;
		case 5:
			num = val5.X / val2.X;
			num2 = val5.Y / val2.Y;
			break;
		default:
			num = (num2 = 0f);
			break;
		}
		num = System.Math.Clamp(num, 0f, 1f);
		num2 = System.Math.Clamp(num2, 0f, 1f);
		Vector4 val6 = layoutInfo.FaceLayoutInfo[surfaceIndex];
		float x = val6.X;
		float y = val6.Y;
		float z = val6.Z;
		float w = val6.W;
		Vector2 val7 = default;
		val7.X = x + num * z;
		val7.Y = y + num2 * w;
		return new Vector2(val7.X / 4f, val7.Y / 3f);
	}
}
