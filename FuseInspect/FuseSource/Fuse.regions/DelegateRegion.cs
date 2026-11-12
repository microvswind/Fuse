using System.Collections.Generic;
using System.Linq;
using Fuse.compute;
using Fuse.function;
using VL.Core;
using VL.Core.PublicAPI;

namespace Fuse.regions;

public class DelegateRegion : Delegate<GpuVoid>
{
	public DelegateRegion(NodeContext nodeContext, IEnumerable<AbstractShaderNode> theInputs, IEnumerable<AbstractShaderNode> theOutputs, IEnumerable<AbstractShaderNode> theCrossLinks, IEnumerable<BorderControlPointDescription> theInputDescriptions, IEnumerable<BorderControlPointDescription> theOutputDescriptions)
		: base(nodeContext, (AbstractShaderNode)null, (List<IFunctionParameter>)null, "delegateRegion", (ShaderNode<GpuVoid>)null)
	{
		OptionalOutputs = new List<AbstractShaderNode>();
		Parameters = Delegate<GpuVoid>.GetUniqueParameters(FunctionParameters());
		BuildFunction(SetupRegion(theInputs, theOutputs, theCrossLinks, theInputDescriptions, theOutputDescriptions));
	}

	private AbstractShaderNode SetupRegion(IEnumerable<AbstractShaderNode> theInputs, IEnumerable<AbstractShaderNode> theOutputs, IEnumerable<AbstractShaderNode> theCrossLinks, IEnumerable<BorderControlPointDescription> theInputDescriptions, IEnumerable<BorderControlPointDescription> theOutputDescriptions)
	{
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(NodeContext);
		OptionalOutputs.Clear();
		List<AbstractShaderNode> list = theInputs.ToList();
		List<AbstractShaderNode> list2 = theOutputs.ToList();
		List<AbstractShaderNode> list3 = theCrossLinks.ToList();
		theInputDescriptions.ToList();
		List<BorderControlPointDescription> list4 = theOutputDescriptions.ToList();
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
		foreach (AbstractShaderNode item7 in list)
		{
			if (item7 is ShaderNode<GpuVoid>)
			{
				list5.Add(item7);
			}
			else
			{
				list5.Add(item7);
			}
		}
		for (int num = 0; num < list2.Count; num++)
		{
			if (list2[num] is ShaderNode<GpuVoid> item)
			{
				list6.Add(item);
				continue;
			}
			AbstractShaderNode theTarget = AbstractCreation.AbstractFunctionParameter(nodeSubContextFactory, list4[num].TypeInfo, InputModifier.Out, num + list.Count);
			AbstractShaderNode item2 = AbstractCreation.AbstractAssignNode(nodeSubContextFactory, theTarget, list2[num]);
			list6.Add(item2);
		}
		Group item3 = new Group(nodeSubContextFactory.NextSubContext(), list5);
		Group item4 = new Group(nodeSubContextFactory.NextSubContext(), myCrossLinks);
		for (int num2 = 0; num2 < list2.Count; num2++)
		{
			if (list2[num2] is ShaderNode<GpuVoid>)
			{
				AbstractShaderNode theOutput = list5[num2] ?? new EmptyVoid(nodeSubContextFactory.NextSubContext());
				AbstractShaderNode item5 = AbstractCreation.AbstractOutput(nodeSubContextFactory, this, theOutput);
				OptionalOutputs.Add(item5);
			}
			else
			{
				AbstractShaderNode theOutput2 = list5[num2] ?? AbstractCreation.AbstractConstant(list2[num2], 0f);
				AbstractShaderNode item6 = AbstractCreation.AbstractOutput(nodeSubContextFactory, this, theOutput2);
				OptionalOutputs.Add(item6);
			}
		}
		List<AbstractShaderNode> list7 = new List<AbstractShaderNode> { item4 };
		list7.AddRange(list5);
		list7.Add(item3);
		list7.AddRange(list6);
		return new Group(nodeSubContextFactory.NextSubContext(), list7);
	}
}
