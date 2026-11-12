namespace Fuse;

public abstract class GpuTypeTracker<T>
{
	public string ComputeGpuType { get; private set; }

	public string GpuType { get; private set; }

	protected GpuTypeTracker()
	{
		ComputeGpuType = "";
		GpuType = "";
	}

	protected abstract string DefineGpuType(T value);

	protected abstract string DefineComputeGpuType(T value);

	public bool CheckDeclaration(T value)
	{
		string text = DefineGpuType(value);
		string text2 = DefineComputeGpuType(value);
		if (text2.Equals(ComputeGpuType) && text.Equals(GpuType))
		{
			return false;
		}
		ComputeGpuType = text2;
		GpuType = text;
		return true;
	}
}
