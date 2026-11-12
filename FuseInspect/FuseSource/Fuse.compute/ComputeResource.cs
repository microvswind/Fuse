using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fuse.ComputeSystem;
using Stride.Core.Mathematics;

namespace Fuse.compute;

public class ComputeResource
{
	public AttributeType AttributeType { get; }

	public string Resource { get; }

	public Int3 Size
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return field;
		}
	}

	public ComputeResource(AttributeType attributeType, string resource, Int3 size)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		AttributeType = attributeType;
		Resource = resource;
		Size = size;
	}

	public override bool Equals(object obj)
	{
		if (obj is ComputeResource computeResource)
		{
			return Resource == computeResource.Resource;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (17 * 23 + AttributeType.GetHashCode()) * 23 + (Resource?.GetHashCode() ?? 0);
	}

	public static IEnumerable<ComputeResource> MergeResources(IEnumerable<ComputeResource> baseSequence, IEnumerable<ComputeResource> sequence2)
	{
		var dictionary = baseSequence.ToDictionary((ComputeResource g) => new { g.AttributeType, g.Resource }, (ComputeResource g) => g);
		foreach (ComputeResource item in sequence2)
		{
			if (item == null)
			{
				continue;
			}
			var key = new { item.AttributeType, item.Resource };
			if (dictionary.ContainsKey(key))
			{
				if (!dictionary[key].Equals(item))
				{
					dictionary[key] = item;
				}
			}
			else
			{
				dictionary[key] = item;
			}
		}
		return dictionary.Values;
	}
}
