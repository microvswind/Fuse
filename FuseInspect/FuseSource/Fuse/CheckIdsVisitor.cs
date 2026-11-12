using System.Collections.Generic;

namespace Fuse;

public class CheckIdsVisitor : IShaderNodeVisitor
{
	private readonly Dictionary<uint, uint> _hashInstances = new Dictionary<uint, uint>();

	public void Visit(AbstractShaderNode node, int recursionLevel)
	{
		if (node.HasFixedName || _hashInstances.TryAdd(node.HashCode, 1u))
		{
			return;
		}
		_hashInstances[node.HashCode]++;
		if (!node.Name.EndsWith("_" + _hashInstances[node.HashCode]))
		{
			node.Name = node.Name + "_" + _hashInstances[node.HashCode];
			if (node is IGpuInput gpuInput)
			{
				gpuInput.OnUpdateName();
			}
		}
	}
}
