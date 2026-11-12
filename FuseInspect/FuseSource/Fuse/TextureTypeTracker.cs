using Stride.Graphics;

namespace Fuse;

public class TextureTypeTracker : GpuTypeTracker<Texture>
{
	private bool _useRw;

	public TextureTypeTracker(bool theUseRw)
	{
		_useRw = theUseRw;
	}

	public void SetUseRw(bool theUseRw)
	{
		_useRw = theUseRw;
	}

	protected override string DefineGpuType(Texture value)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected I4, but got Unknown
		if (value == null)
		{
			return "Texture1D";
		}
		TextureDimension dimension = value.Dimension;
		return (int)dimension switch
		{
			0 => "Texture1D", 
			1 => (value.ArraySize == 1) ? "Texture2D" : "Texture2DArray", 
			2 => "Texture3D", 
			3 => "TextureCube", 
			_ => "Texture2D", 
		};
	}

	protected override string DefineComputeGpuType(Texture value)
	{
		if (value != null)
		{
			return TypeHelpers.TextureTypeName(value, _useRw);
		}
		return "Texture1D";
	}
}
