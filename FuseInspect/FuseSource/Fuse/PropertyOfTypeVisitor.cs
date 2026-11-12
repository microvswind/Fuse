using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fuse;

public class PropertyOfTypeVisitor<TPropertyType> : IShaderNodeVisitor
{
	public readonly List<TPropertyType> Result = new List<TPropertyType>();

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		node.Property.ForEach((KeyValuePair<string, IList> kv) =>
		{
			IEnumerable<TPropertyType> enumerable = kv.Value.OfType<TPropertyType>();
			TPropertyType[] array = (enumerable as TPropertyType[]) ?? enumerable.ToArray();
			if (!EnumerableEx.IsEmpty<TPropertyType>((IEnumerable<TPropertyType>)array))
			{
				array.ForEach((TPropertyType v) =>
				{
					Result.Add(v);
				});
			}
		});
	}
}
