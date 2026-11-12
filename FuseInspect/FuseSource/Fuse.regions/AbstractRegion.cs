using System.Collections.Generic;
using System.Linq;
using Fuse.compute;
using VL.Core;
using VL.Core.PublicAPI;

namespace Fuse.regions;

public abstract class AbstractRegion : ShaderNode<GpuVoid>
{
	public delegate void AddRegionNodes(List<AbstractShaderNode> theNodes);

	public delegate Group CreateRegionGroup(NodeSubContextFactory subContextFactory, List<AbstractShaderNode> outputs);

	protected AbstractRegion(NodeContext nodeContext, string theName)
		: base(nodeContext, theName, (ShaderNode<GpuVoid>)null, true)
	{
		OptionalOutputs = new List<AbstractShaderNode>();
	}

	protected virtual void SetupRegion(CreateRegionGroup theCreateRegion, AddRegionNodes theAddRegionNodes, IEnumerable<AbstractShaderNode> theInputs, IEnumerable<AbstractShaderNode> theOutputs, IEnumerable<AbstractShaderNode> theCrossLinks, IEnumerable<BorderControlPointDescription> theDescriptions)
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(NodeContext);
		OptionalOutputs.Clear();
		List<AbstractShaderNode> list = theInputs.ToList();
		List<AbstractShaderNode> list2 = theOutputs.ToList();
		List<AbstractShaderNode> list3 = theCrossLinks.ToList();
		List<BorderControlPointDescription> list4 = theDescriptions.ToList();
		List<AbstractShaderNode> myCrossLinks = new List<AbstractShaderNode>();
		list3.ForEach((AbstractShaderNode c) =>
		{
			bool flag = ((c == null || c is IInjectToRegion) ? true : false);
			if (!flag && !TypeHelpers.IsDelegate(c))
			{
				myCrossLinks.Add(c);
			}
		});
		List<AbstractShaderNode> list5 = new List<AbstractShaderNode>();
		List<AbstractShaderNode> list6 = new List<AbstractShaderNode>();
		for (int num = 0; num < list2.Count; num++)
		{
			if (list2[num] is ShaderNode<GpuVoid> item)
			{
				AbstractShaderNode item2 = list[num];
				list5.Add(item2);
				list6.Add(item);
			}
			else
			{
				AbstractShaderNode item3 = ((list[num] == null) ? AbstractCreation.AbstractConstant(list4[num].TypeInfo, 0f) : list[num]);
				list5.Add(item3);
				AbstractShaderNode item4 = ((num >= list2.Count) ? AbstractCreation.AbstractConstant(list4[num].TypeInfo, 0f) : list2[num]);
				list6.Add(item4);
			}
		}
		for (int num2 = 0; num2 < list2.Count; num2++)
		{
			if (!(list2[num2] is ShaderNode<GpuVoid>))
			{
				AbstractShaderNode item5 = AbstractCreation.AbstractAssignNode(nodeSubContextFactory, list5[num2], list6[num2]);
				list6.Add(item5);
			}
		}
		Group item6 = new Group(nodeSubContextFactory.NextSubContext(), list5);
		Group item7 = new Group(nodeSubContextFactory.NextSubContext(), myCrossLinks);
		for (int num3 = 0; num3 < list2.Count; num3++)
		{
			if (list2[num3] is ShaderNode<GpuVoid>)
			{
				AbstractShaderNode theOutput = list5[num3] ?? new EmptyVoid(nodeSubContextFactory.NextSubContext());
				AbstractShaderNode item8 = AbstractCreation.AbstractOutput(nodeSubContextFactory, this, theOutput);
				OptionalOutputs.Add(item8);
			}
			else
			{
				AbstractShaderNode theOutput2 = list5[num3] ?? AbstractCreation.AbstractConstant(list2[num3], 0f);
				AbstractShaderNode item9 = AbstractCreation.AbstractOutput(nodeSubContextFactory, this, theOutput2);
				OptionalOutputs.Add(item9);
			}
		}
		List<AbstractShaderNode> list7 = new List<AbstractShaderNode> { item7 };
		theAddRegionNodes(list7);
		list7.Add(item6);
		list7.Add(theCreateRegion(nodeSubContextFactory, list6));
		SetInputs(list7);
	}
}
