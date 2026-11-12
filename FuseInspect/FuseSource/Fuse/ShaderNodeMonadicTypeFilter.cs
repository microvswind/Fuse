using System;
using Fuse.compute;
using Stride.Graphics;
using VL.Core;

namespace Fuse;

internal sealed class ShaderNodeMonadicTypeFilter : IMonadicTypeFilter
{
	public bool Accepts(TypeDescriptor typeDescriptor)
	{
		if (typeDescriptor.IsUnmanaged)
		{
			return true;
		}
		Type clrType = typeDescriptor.ClrType;
		if ((object)clrType == null)
		{
			return false;
		}
		if (typeof(Buffer).IsAssignableFrom(clrType))
		{
			return true;
		}
		if (clrType == typeof(Texture))
		{
			return true;
		}
		if (clrType == typeof(SamplerState))
		{
			return true;
		}
		if (clrType == typeof(GpuStruct))
		{
			return true;
		}
		if (clrType == typeof(GpuVoid))
		{
			return true;
		}
		return false;
	}
}
