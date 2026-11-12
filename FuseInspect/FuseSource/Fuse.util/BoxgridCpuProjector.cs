using System;
using Stride.Core.Mathematics;

namespace Fuse.util;

public static class BoxgridCpuProjector
{
	public struct BoxgridProjector
	{
		public Vector3 RoomCenter;

		public Vector3 RoomSize;

		public Vector4 ZoomRegion;

		public Vector3 ViewerPosition;

		public Vector4[] FaceLayoutInfo;

		public BoundingBox RoomBounds
		{
			get
			{
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				//IL_0019: Unknown result type (might be due to invalid IL or missing references)
				//IL_0026: Unknown result type (might be due to invalid IL or missing references)
				//IL_002b: Unknown result type (might be due to invalid IL or missing references)
				Vector3 val = (ref RoomSize) * 0.5f;
				return new BoundingBox((ref RoomCenter) - (ref val), (ref RoomCenter) + (ref val));
			}
		}
	}

	public static bool TryProjectPointToAtlas(in BoxgridProjector proj, in Vector3 particlePos, out int faceIndex, out Vector2 atlasPos4x3, out float offsetDist)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		faceIndex = -1;
		atlasPos4x3 = Vector2.Zero;
		offsetDist = 0f;
		Vector3 viewerPosition = proj.ViewerPosition;
		Vector3 val = (ref particlePos) - (ref viewerPosition);
		float num = val.LengthSquared();
		if (num <= float.Epsilon)
		{
			return false;
		}
		Vector3 val2 = (ref val) / (float)System.Math.Sqrt(num);
		BoundingBox roomBounds = proj.RoomBounds;
		Vector3 minimum = roomBounds.Minimum;
		Vector3 maximum = roomBounds.Maximum;
		Vector3 val3 = new Vector3(1f / val2.X, 1f / val2.Y, 1f / val2.Z);
		Vector3 val4 = (ref minimum) - (ref viewerPosition);
		Vector3 val5 = (ref val4) * (ref val3);
		val4 = (ref maximum) - (ref viewerPosition);
		Vector3 val6 = (ref val4) * (ref val3);
		Vector3 val7 = Vector3.Min(val5, val6);
		Vector3 val8 = Vector3.Max(val5, val6);
		float num2 = System.Math.Max(System.Math.Max(val7.X, val7.Y), val7.Z);
		float num3 = System.Math.Min(System.Math.Min(val8.X, val8.Y), val8.Z);
		if (num3 < 0f || num2 > num3)
		{
			return false;
		}
		float num4 = ((num2 > 0f) ? num2 : num3);
		if (num4 <= 0f)
		{
			return false;
		}
		val4 = (ref val2) * num4;
		Vector3 val9 = (ref viewerPosition) + (ref val4);
		if (System.Math.Abs(val9.Y - minimum.Y) < 0.0001f)
		{
			faceIndex = 0;
		}
		else if (System.Math.Abs(val9.Y - maximum.Y) < 0.0001f)
		{
			faceIndex = 1;
		}
		else if (System.Math.Abs(val9.X - maximum.X) < 0.0001f)
		{
			faceIndex = 2;
		}
		else if (System.Math.Abs(val9.Z - maximum.Z) < 0.0001f)
		{
			faceIndex = 3;
		}
		else if (System.Math.Abs(val9.X - minimum.X) < 0.0001f)
		{
			faceIndex = 4;
		}
		else
		{
			if (!(System.Math.Abs(val9.Z - minimum.Z) < 0.0001f))
			{
				return false;
			}
			faceIndex = 5;
		}
		Vector3 roomSize = proj.RoomSize;
		float num5 = 0f;
		float num6 = 0f;
		switch (faceIndex)
		{
		case 0:
			num5 = (val9.X - minimum.X) / roomSize.X;
			num6 = 1f - (val9.Z - minimum.Z) / roomSize.Z;
			break;
		case 1:
			num5 = (val9.X - minimum.X) / roomSize.X;
			num6 = (val9.Z - minimum.Z) / roomSize.Z;
			break;
		case 2:
			num5 = (val9.Z - minimum.Z) / roomSize.Z;
			num6 = (val9.Y - minimum.Y) / roomSize.Y;
			break;
		case 3:
			num5 = 1f - (val9.X - minimum.X) / roomSize.X;
			num6 = (val9.Y - minimum.Y) / roomSize.Y;
			break;
		case 4:
			num5 = 1f - (val9.Z - minimum.Z) / roomSize.Z;
			num6 = (val9.Y - minimum.Y) / roomSize.Y;
			break;
		case 5:
			num5 = (val9.X - minimum.X) / roomSize.X;
			num6 = (val9.Y - minimum.Y) / roomSize.Y;
			break;
		default:
			return false;
		}
		if (num5 < 0f || num5 > 1f || num6 < 0f || num6 > 1f)
		{
			return false;
		}
		Vector4 val10 = proj.FaceLayoutInfo[faceIndex];
		float num7 = val10.X + num5 * val10.Z;
		float num8 = val10.Y + num6 * val10.W;
		Vector2 val11 = new Vector2(num7 / 4f, num8 / 3f);
		Vector2 val12 = new Vector2(proj.ZoomRegion.X, proj.ZoomRegion.Y);
		Vector2 val13 = new Vector2(proj.ZoomRegion.Z, proj.ZoomRegion.W) - val12;
		if (val13.X <= 0f || val13.Y <= 0f)
		{
			return false;
		}
		Vector2 val14 = (val11 - val12) / val13;
		atlasPos4x3 = new Vector2(val14.X * 4f, val14.Y * 3f);
		offsetDist = Vector3.Distance(val9, particlePos);
		if (val14.X < 0f || val14.X > 1f || val14.Y < 0f || val14.Y > 1f)
		{
			return false;
		}
		return true;
	}

	public static bool CullBoundingBox(in BoxgridProjector proj, in BoundingBox box)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		BoundingBox val = box;
		BoundingBox roomBounds = proj.RoomBounds;
		if (!val.Intersects(ref roomBounds))
		{
			return true;
		}
		Vector3 minimum = box.Minimum;
		Vector3 maximum = box.Maximum;
		for (int i = 0; i < 8; i++)
		{
			Vector3 particlePos = new Vector3(((i & 1) != 0) ? maximum.X : minimum.X, ((i & 2) != 0) ? maximum.Y : minimum.Y, ((i & 4) != 0) ? maximum.Z : minimum.Z);
			if (TryProjectPointToAtlas(in proj, in particlePos, out var _, out var _, out var _))
			{
				return false;
			}
		}
		return true;
	}

	public static bool CullBoundingSphere(in BoxgridProjector proj, in BoundingSphere sphere)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (!SphereIntersectsAabb(in sphere, proj.RoomBounds))
		{
			return true;
		}
		TryProjectPointToAtlas(in proj, in sphere.Center, out var _, out var _, out var _);
		return false;
	}

	private static bool SphereIntersectsAabb(in BoundingSphere s, in BoundingBox b)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 center = s.Center;
		float num = MathF.Max(b.Minimum.X, MathF.Min(center.X, b.Maximum.X));
		float num2 = MathF.Max(b.Minimum.Y, MathF.Min(center.Y, b.Maximum.Y));
		float num3 = MathF.Max(b.Minimum.Z, MathF.Min(center.Z, b.Maximum.Z));
		float num4 = num - center.X;
		float num5 = num2 - center.Y;
		float num6 = num3 - center.Z;
		return num4 * num4 + num5 * num5 + num6 * num6 <= s.Radius * s.Radius;
	}

	public static BoxgridProjector CreateBoxgridProjector(in BoundingBox roomBounds, in Vector3 viewerPosition, in Vector4[] faceLayoutInfo)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		BoxgridProjector result = default;
		BoundingBox val = roomBounds;
		result.RoomCenter = val.Center;
		result.RoomSize = (ref roomBounds.Maximum) - (ref roomBounds.Minimum);
		result.ViewerPosition = viewerPosition;
		result.FaceLayoutInfo = faceLayoutInfo;
		return result;
	}
}
