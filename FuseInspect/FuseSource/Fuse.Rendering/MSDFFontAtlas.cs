using System.Collections.Generic;
using System.Text.Json;
using Stride.Core.Mathematics;

namespace Fuse.Rendering;

public class MSDFFontAtlas
{
	public class AtlasInfo
	{
		public string type { get; set; }

		public float distanceRange { get; set; }

		public float distanceRangeMiddle { get; set; }

		public float size { get; set; }

		public int width { get; set; }

		public int height { get; set; }

		public string yOrigin { get; set; }

		public GridInfo grid { get; set; }
	}

	public class GridInfo
	{
		public float cellWidth { get; set; }

		public float cellHeight { get; set; }

		public int columns { get; set; }

		public int rows { get; set; }

		public float originY { get; set; }
	}

	public class MetricsInfo
	{
		public float emSize { get; set; }

		public float lineHeight { get; set; }

		public float ascender { get; set; }

		public float descender { get; set; }

		public float underlineY { get; set; }

		public float underlineThickness { get; set; }
	}

	public class PlaneBounds
	{
		public float left { get; set; }

		public float bottom { get; set; }

		public float right { get; set; }

		public float top { get; set; }
	}

	public class AtlasBounds
	{
		public float left { get; set; }

		public float bottom { get; set; }

		public float right { get; set; }

		public float top { get; set; }
	}

	public class GlyphInfo
	{
		public int unicode { get; set; }

		public float advance { get; set; }

		public PlaneBounds planeBounds { get; set; }

		public AtlasBounds atlasBounds { get; set; }
	}

	public class MSDFFont
	{
		public AtlasInfo atlas { get; set; }

		public MetricsInfo metrics { get; set; }

		public List<GlyphInfo> glyphs { get; set; }

		public List<object> kerning { get; set; }
	}

	private readonly MSDFFont fontData;

	private Dictionary<char, GlyphInfo> glyphMap;

	public MSDFFontAtlas(string jsonData)
	{
		fontData = JsonSerializer.Deserialize<MSDFFont>(jsonData);
		InitializeGlyphMap();
	}

	private void InitializeGlyphMap()
	{
		glyphMap = new Dictionary<char, GlyphInfo>();
		foreach (GlyphInfo glyph in fontData.glyphs)
		{
			if (glyph.unicode > 0)
			{
				glyphMap[(char)glyph.unicode] = glyph;
			}
		}
	}

	public (Vector2 position, Vector2 uvMin, Vector2 uvMax) GetGlyphQuad(char character, Vector2 position, float scale)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if (!glyphMap.TryGetValue(character, out var value))
		{
			return default;
		}
		if (value.planeBounds == null || value.atlasBounds == null)
		{
			return (position: new Vector2(position.X + value.advance * scale, position.Y), uvMin: Vector2.Zero, uvMax: Vector2.Zero);
		}
		float num = fontData.atlas.width;
		float num2 = fontData.atlas.height;
		Vector2 item = new Vector2(value.atlasBounds.left / num, 1f - value.atlasBounds.bottom / num2);
		Vector2 item2 = new Vector2(value.atlasBounds.right / num, 1f - value.atlasBounds.top / num2);
		_ = value.planeBounds.left;
		_ = value.planeBounds.bottom;
		float num3 = position.X + value.planeBounds.right * scale;
		float num4 = position.Y + value.planeBounds.top * scale;
		return (position: new Vector2(num3, num4), uvMin: item, uvMax: item2);
	}

	public float GetAdvance(char character, float scale)
	{
		if (!glyphMap.TryGetValue(character, out var value))
		{
			return 0f;
		}
		return value.advance * scale;
	}

	public float GetLineHeight(float scale)
	{
		return fontData.metrics.lineHeight * scale;
	}
}
