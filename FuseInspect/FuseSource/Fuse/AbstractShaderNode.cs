using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fuse.function;
using Stride.Core.Extensions;
using Stride.Rendering.Materials;
using Stride.Rendering.Materials.ComputeColors;
using Stride.Shaders;
using VL.Core;

namespace Fuse;

public abstract class AbstractShaderNode : IComputeNode
{
	private const string DefaultShaderCode = "${resultType} ${resultName};";

	protected const string Mixins = "Mixins";

	protected const string Inputs = "Inputs";

	protected const string Compositions = "Compositions";

	protected const string Declarations = "Declarations";

	protected const string Structs = "Structs";

	protected const string ConstantArrays = "ConstantArrays";

	protected const string Streams = "Streams";

	private readonly HashSet<ShaderGeneratorContext> _contexts = new HashSet<ShaderGeneratorContext>();

	public readonly NodeContext NodeContext;

	public readonly List<IPrepareGraph> PrepareGraphListener = new List<IPrepareGraph>();

	private bool _hasNullInput;

	public List<AbstractShaderNode> Ins = new List<AbstractShaderNode>();

	protected HandleNullInputMode NullInputMode = HandleNullInputMode.SetOff;

	public bool HasFixedName { get; set; }

	public int WriteCounter { get; set; }

	public virtual string Name { get; set; }

	public abstract AbstractShaderNode AbstractDefault { get; }

	public uint HashCode { get; set; }

	public abstract string ID { get; }

	public virtual string DelegateID => ID;

	public virtual IDictionary<string, string> Functions
	{
		get
		{
			return new Dictionary<string, string>();
		}
		protected set
		{
			throw new NotImplementedException();
		}
	}

	public string SourceCode => GenerateSource(Ins);

	public string ShaderCode { get; set; }

	public Dictionary<string, IList> Property { get; } = new Dictionary<string, IList>();

	protected AbstractShaderNode(NodeContext nodeContext, string theId)
	{
		NodeContext = nodeContext;
		Name = theId;
		HashCode = ShaderNodesUtil.GetHashCode(nodeContext);
		HasFixedName = false;
	}

	public virtual IEnumerable<IComputeNode> GetChildren(object context = null)
	{
		return (IEnumerable<IComputeNode>)Enumerable.Empty<ComputeNode>();
	}

	public virtual ShaderSource GenerateShaderSource(ShaderGeneratorContext context, MaterialComputeColorKeys baseKeys)
	{
		return null;
	}

	public void AddPrepareGraph(IPrepareGraph theDelegate)
	{
		PrepareGraphListener.Add(theDelegate);
	}

	public void RemovePrepareGraph(IPrepareGraph theDelegate)
	{
		PrepareGraphListener.Remove(theDelegate);
	}

	public abstract string TypeName();

	protected abstract string SourceTemplate();

	public abstract int Dimension();

	public void SetViewerID(string theID)
	{
		SetProperty("ViewerID", new ViewerID(theID));
	}

	public void CallPrepareGraph()
	{
		foreach (IPrepareGraph item in PrepareGraphListener)
		{
			item.PrepareGraph(this);
		}
		foreach (AbstractShaderNode @in in Ins)
		{
			@in.CallPrepareGraph();
		}
	}

	public void SetInputs(IEnumerable<AbstractShaderNode> theIns)
	{
		if (Ins.SequenceEqual(theIns))
		{
			return;
		}
		_hasNullInput = false;
		Ins = new List<AbstractShaderNode>();
		theIns.ForEach((AbstractShaderNode input) =>
		{
			switch (NullInputMode)
			{
			case HandleNullInputMode.SetOff:
				if (input != null)
				{
					Ins.Add(input);
				}
				else
				{
					_hasNullInput = true;
				}
				break;
			case HandleNullInputMode.ReplaceWithDefault:
				if (input == null)
				{
					Ins.Add(AbstractDefault);
				}
				break;
			case HandleNullInputMode.Remove:
				if (input != null)
				{
					Ins.Add(input);
				}
				break;
			}
		});
	}

	public void AddInput(AbstractShaderNode theInput)
	{
		if (theInput != null)
		{
			Ins.Add(theInput);
		}
	}

	protected virtual Dictionary<string, string> CreateTemplateMap()
	{
		return new Dictionary<string, string>();
	}

	protected virtual string GenerateDefaultSource()
	{
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName};", new Dictionary<string, string>
		{
			{ "resultName", ID },
			{
				"resultType",
				TypeName()
			},
			{ "default", "" }
		});
	}

	private string GenerateSource(IEnumerable<AbstractShaderNode> theIns)
	{
		if (_hasNullInput)
		{
			return GenerateDefaultSource();
		}
		string text = SourceTemplate();
		if (!(text.Trim() == ""))
		{
			return ShaderNodesUtil.Evaluate(text, CreateTemplateMap());
		}
		return "";
	}

	public virtual void OnPassContext(ShaderGeneratorContext nodeContext)
	{
	}

	public virtual void BuildChildrenSource(StringBuilder theSourceBuilder, HashSet<AbstractShaderNode> theHashes, string thePrepend)
	{
		foreach (AbstractShaderNode @in in Ins)
		{
			@in.BuildSource(theSourceBuilder, theHashes, thePrepend + ShaderNodesUtil.DebugIdent);
		}
	}

	protected virtual void BuildSource(StringBuilder theSourceBuilder, HashSet<AbstractShaderNode> theHashes, string thePrepend)
	{
		if (ShaderNodesUtil.DebugShaderGeneration)
		{
			Console.WriteLine(thePrepend + ID);
		}
		if (theHashes.Add(this))
		{
			BuildChildrenSource(theSourceBuilder, theHashes, thePrepend);
			string sourceCode = SourceCode;
			if (!string.IsNullOrWhiteSpace(sourceCode))
			{
				theSourceBuilder.Append("        ");
				theSourceBuilder.Append(sourceCode);
				theSourceBuilder.Append(Environment.NewLine);
			}
		}
	}

	public string BuildSourceCode()
	{
		StringBuilder stringBuilder = new StringBuilder();
		HashSet<AbstractShaderNode> theHashes = new HashSet<AbstractShaderNode>();
		BuildSource(stringBuilder, theHashes, "");
		return stringBuilder.ToString();
	}

	public void PreOrderVisit(IShaderNodeVisitor theVisitor, HashSet<AbstractShaderNode> theVisitedNodes, int recursionLevel = 0)
	{
		if (!theVisitedNodes.Add(this))
		{
			return;
		}
		theVisitor.Visit(this, recursionLevel);
		foreach (AbstractShaderNode @in in Ins)
		{
			@in?.PreOrderVisit(theVisitor, theVisitedNodes, recursionLevel + 1);
		}
	}

	public void PreOrderVisit(IShaderNodeVisitor theVisitor)
	{
		PreOrderVisit(theVisitor, new HashSet<AbstractShaderNode>());
	}

	public void PostOrderVisit(IShaderNodeVisitor theVisitor, HashSet<AbstractShaderNode> theVisitedNodes, int recursionLevel = 0)
	{
		if (!theVisitedNodes.Add(this))
		{
			return;
		}
		foreach (AbstractShaderNode @in in Ins)
		{
			@in?.PostOrderVisit(theVisitor, theVisitedNodes);
		}
		theVisitor.Visit(this, recursionLevel);
	}

	public void PrintVisitTree()
	{
		PreOrderVisit(new PrintShaderNodeVisitor(), new HashSet<AbstractShaderNode>());
	}

	public void CheckHashCodes()
	{
		PreOrderVisit(new CheckIdsVisitor(), new HashSet<AbstractShaderNode>());
	}

	public void CheckContext(ShaderGeneratorContext theContext)
	{
		if (_contexts.Add(theContext))
		{
			CheckContextVisitor theVisitor = new CheckContextVisitor(theContext);
			PreOrderVisit(theVisitor);
		}
	}

	public List<TNode> ChildrenOfType<TNode>() where TNode : class
	{
		ChildrenOfTypeVisitor<TNode> childrenOfTypeVisitor = new ChildrenOfTypeVisitor<TNode>();
		PreOrderVisit(childrenOfTypeVisitor);
		return childrenOfTypeVisitor.Result.ToList();
	}

	public List<IFunctionParameter> FunctionParameters()
	{
		return ChildrenOfType<IFunctionParameter>();
	}

	public List<TPropertyType> PropertyForTree<TPropertyType>(string theThePropertyId)
	{
		PropertyOfTypeAndIdVisitor<TPropertyType> propertyOfTypeAndIdVisitor = new PropertyOfTypeAndIdVisitor<TPropertyType>(theThePropertyId);
		PreOrderVisit(propertyOfTypeAndIdVisitor);
		return propertyOfTypeAndIdVisitor.Result.ToList();
	}

	public List<TPropertyType> PropertiesForTreeList<TPropertyType>()
	{
		PropertyOfTypeVisitor<TPropertyType> propertyOfTypeVisitor = new PropertyOfTypeVisitor<TPropertyType>();
		PreOrderVisit(propertyOfTypeVisitor);
		return propertyOfTypeVisitor.Result.ToList();
	}

	public List<string> PropertyIdsForTree()
	{
		PropertyIdsVisitor propertyIdsVisitor = new PropertyIdsVisitor();
		PreOrderVisit(propertyIdsVisitor);
		return propertyIdsVisitor.Result.ToList();
	}

	public Dictionary<string, IList> PropertiesForTree()
	{
		PropertiesVisitor propertiesVisitor = new PropertiesVisitor();
		PreOrderVisit(propertiesVisitor);
		return propertiesVisitor.Result;
	}

	public Dictionary<string, List<TProperty>> PropertiesForTree<TProperty>(Dictionary<string, List<TProperty>> theProperties = null)
	{
		PropertiesTypedVisitor<TProperty> propertiesTypedVisitor = new PropertiesTypedVisitor<TProperty>();
		PreOrderVisit(propertiesTypedVisitor);
		return propertiesTypedVisitor.Result;
	}

	public void AddProperty(string thePropertyId, object theProperty)
	{
		if (!Property.ContainsKey(thePropertyId))
		{
			Property[thePropertyId] = new ArrayList();
		}
		Property[thePropertyId].Add(theProperty);
	}

	public void SetProperty(string thePropertyId, object theProperty)
	{
		Property[thePropertyId] = new ArrayList { theProperty };
	}

	public void RemoveProperty(string thePropertyId)
	{
		Property.Remove(thePropertyId);
	}

	public void AddProperties(string thePropertyId, IList theProperties)
	{
		if (!Property.ContainsKey(thePropertyId))
		{
			Property[thePropertyId] = new ArrayList();
		}
		EnumerableExtensions.ForEach<object>((IEnumerable)theProperties, (Action<object>)((object r) =>
		{
			Property[thePropertyId].Add(r);
		}));
	}

	public List<TPropertyType> GetProperties<TPropertyType>()
	{
		List<TPropertyType> result = new List<TPropertyType>();
		Property.ForEach((KeyValuePair<string, IList> kv) =>
		{
			IEnumerable<TPropertyType> enumerable = kv.Value.OfType<TPropertyType>();
			TPropertyType[] array = (enumerable as TPropertyType[]) ?? enumerable.ToArray();
			if (!EnumerableEx.IsEmpty<TPropertyType>((IEnumerable<TPropertyType>)array))
			{
				array.ForEach((TPropertyType v) =>
				{
					result.Add(v);
				});
			}
		});
		return result;
	}

	public List<string> MixinList()
	{
		return PropertyForTree<string>("Mixins");
	}

	public List<IGpuInput> InputList()
	{
		return PropertyForTree<IGpuInput>("Inputs");
	}

	public List<string> CompositionList()
	{
		return PropertyForTree<string>("Compositions");
	}

	public List<FieldDeclaration> DeclarationList()
	{
		return PropertyForTree<FieldDeclaration>("Declarations");
	}

	public List<string> StructList()
	{
		return PropertyForTree<string>("Structs");
	}

	public List<string> ConstantArrayList()
	{
		return PropertyForTree<string>("ConstantArrays");
	}

	public List<string> StreamList()
	{
		return PropertyForTree<string>("Streams");
	}

	public Dictionary<string, string> FunctionMap()
	{
		FunctionMapVisitor functionMapVisitor = new FunctionMapVisitor();
		PreOrderVisit(functionMapVisitor);
		return functionMapVisitor.Result;
	}
}
