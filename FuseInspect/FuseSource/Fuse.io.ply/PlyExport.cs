using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Fuse.util;
using Stride.Core.Extensions;
using Stride.Core.Mathematics;

namespace Fuse.io.ply;

public static class PlyExport
{
	public static void ExportBinaryPlyWithSphereFrustumCullingAndTransforms(List<float[]> vertexDataList, List<Matrix>? transforms, IReadOnlyList<string> elementNames, BoundingFrustum frustum, bool checkFrustum, float radius, string outputPlyPath)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		if (vertexDataList == null || vertexDataList.Count == 0)
		{
			throw new ArgumentException("vertexDataList is null or empty.");
		}
		bool flag = transforms != null && transforms.Count == vertexDataList.Count;
		if (elementNames == null || elementNames.Count < 4)
		{
			throw new ArgumentException("elementNames must contain at least x, y, z, and radius attributes.");
		}
		int num = CollectionExtensions.IndexOf<string>(elementNames, "x");
		int num2 = CollectionExtensions.IndexOf<string>(elementNames, "y");
		int num3 = CollectionExtensions.IndexOf<string>(elementNames, "z");
		if (num < 0 || num2 < 0 || num3 < 0)
		{
			throw new ArgumentException("elementNames must include 'x', 'y', 'z', attributes.");
		}
		int count = elementNames.Count;
		Plane[] planes = (checkFrustum ? FrustumUtil.GetFrustumPlanes(frustum) : null);
		BoundingSphere sphere = new BoundingSphere(default(Vector3), radius);
		Vector3 center = default;
		List<float> list = new List<float>();
		for (int i = 0; i < vertexDataList.Count; i++)
		{
			float[] array = vertexDataList[i];
			Matrix val = ((flag && transforms != null) ? transforms[i] : Matrix.Identity);
			if (array == null || array.Length == 0)
			{
				continue;
			}
			if (array.Length % count != 0)
			{
				throw new ArgumentException($"vertexData at index {i} length ({array.Length}) is not divisible by stride ({count}).");
			}
			int num4 = array.Length / count;
			for (int j = 0; j < num4; j++)
			{
				int num5 = j * count;
				float num6 = array[num5 + num];
				float num7 = array[num5 + num2];
				float num8 = array[num5 + num3];
				Vector3 val2 = new Vector3(num6, num7, num8);
				Vector4 val3 = (Vector4)(flag ? Vector4.Transform(new Vector4(val2, 1f), val) : new Vector4(val2, 1f));
				if (radius < 0f)
				{
					Console.WriteLine($"Warning: negative radius {radius} at vertex {j} (arrayIdx={i}).");
				}
				center.X = val3.X;
				center.Y = val3.Y;
				center.Z = val3.Z;
				sphere.Center = center;
				if (!checkFrustum || FrustumUtil.IsSphereInsideFrustum(planes, sphere))
				{
					int count2 = list.Count;
					list.AddRange(new float[count]);
					for (int k = 0; k < count; k++)
					{
						list[count2 + k] = array[num5 + k];
					}
					list[count2 + num] = val3.X;
					list[count2 + num2] = val3.Y;
					list[count2 + num3] = val3.Z;
				}
			}
		}
		int value = list.Count / count;
		using (FileStream output = new FileStream(outputPlyPath, FileMode.Create, FileAccess.Write))
		{
			using BinaryWriter binaryWriter = new BinaryWriter(output);
			binaryWriter.Write(Encoding.ASCII.GetBytes("ply\n"));
			binaryWriter.Write(Encoding.ASCII.GetBytes("format binary_little_endian 1.0\n"));
			binaryWriter.Write(Encoding.ASCII.GetBytes($"element vertex {value}\n"));
			foreach (string elementName in elementNames)
			{
				binaryWriter.Write(Encoding.ASCII.GetBytes("property float " + elementName + "\n"));
			}
			binaryWriter.Write(Encoding.ASCII.GetBytes("end_header\n"));
			for (int l = 0; l < list.Count; l++)
			{
				float value2 = list[l];
				binaryWriter.Write(value2);
			}
		}
		Console.WriteLine($"Merged PLY export complete. Wrote {value} vertices to '{outputPlyPath}'.");
	}
}
