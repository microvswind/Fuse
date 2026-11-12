using VL.Core;

namespace Fuse;

public class ConstantValue<T> : ShaderNode<T>, IConstantValue
{
	public T Value { get; }

	public override string ID => TypeHelpers.GetDefaultForType(Value);

	public ConstantValue(T theValue)
		: base(NodeContext.CurrentRoot, "constant", (ShaderNode<T>)null, false)
	{
		Value = theValue;
		HasFixedName = true;
	}

	public object GetValueAsObject()
	{
		return Value;
	}

	public override string TypeName()
	{
		return TypeHelpers.GetGpuType<T>();
	}

	protected override string SourceTemplate()
	{
		return "";
	}
}
