using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Stride.Core.Extensions;

namespace Fuse;

public class PropertyOfTypeAndIdVisitor<TPropertyType> : IShaderNodeVisitor
{
	private readonly string _propertyId;

	public readonly HashSet<TPropertyType> Result = new HashSet<TPropertyType>();

	public PropertyOfTypeAndIdVisitor(string theThePropertyId)
	{
		_propertyId = theThePropertyId;
	}

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		new Stopwatch().Start();
		if (node.Property.ContainsKey(_propertyId))
		{
			EnumerableExtensions.ForEach<TPropertyType>((IEnumerable)node.Property[_propertyId], (Action<TPropertyType>)((TPropertyType i) =>
			{
				Result.Add(i);
			}));
		}
	}
}
