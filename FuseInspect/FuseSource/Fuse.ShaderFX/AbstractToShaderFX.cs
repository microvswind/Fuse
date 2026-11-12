using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Stride.Rendering.Materials;
using Stride.Rendering.Materials.ComputeColors;
using Stride.Shaders;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse.ShaderFX;

public abstract class AbstractToShaderFX<T> : IComputeValue<T>, IComputeNode
{
	private readonly HashSet<string> _compositions = new HashSet<string>();

	private readonly HashSet<string> _constantArrays = new HashSet<string>();

	private readonly Dictionary<string, string> _customTemplate;

	private readonly HashSet<string> _declarations = new HashSet<string>();

	private readonly Dictionary<string, string> _functionMap = new Dictionary<string, string>();

	private readonly HashSet<string> _groupDeclarations = new HashSet<string>();

	private readonly bool _isCompute;

	private readonly HashSet<string> _mixins = new HashSet<string>();

	private readonly string _sourceTemplate;

	private readonly List<AbstractStage> _stages;

	private readonly Stopwatch _stopwatch = new Stopwatch();

	private readonly HashSet<string> _streams = new HashSet<string>();

	private readonly HashSet<string> _structs = new HashSet<string>();

	public string ShaderCode { get; private set; }

	public string ShaderName { get; private set; }

	public Dictionary<string, AbstractShaderNode> Inputs { get; }

	protected AbstractToShaderFX(List<AbstractStage> theStages, Dictionary<string, string> theCustomTemplate, bool theIsCompute, string theSource)
	{
		_customTemplate = theCustomTemplate;
		_isCompute = theIsCompute;
		_stages = theStages;
		_sourceTemplate = theSource;
		_stages = theStages;
		Inputs = new Dictionary<string, AbstractShaderNode>();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (AbstractStage stage in _stages)
		{
			if (stage?.StageNode != null)
			{
				Inputs.Add(stage.Key, stage.StageNode);
				dictionary.Add("stage" + stage.Key, stage.Source());
			}
		}
		_sourceTemplate = ShaderNodesUtil.Evaluate(_sourceTemplate, dictionary);
	}

	public virtual IEnumerable<IComputeNode> GetChildren(object context = null)
	{
		return (IEnumerable<IComputeNode>)Enumerable.Empty<ComputeNode>();
	}

	public ShaderSource GenerateShaderSource(ShaderGeneratorContext theContext, MaterialComputeColorKeys baseKeys)
	{
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Expected Obj, but got Unknown
		_stopwatch.Reset();
		_stopwatch.Start();
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine("-> Start Generating Shader " + ShaderName);
		}
		Dictionary<string, (string, string)> dictionary = new Dictionary<string, (string, string)>();
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<string, AbstractShaderNode> input in Inputs)
		{
			AbstractShaderNode value = input.Value;
			value.CheckHashCodes();
			value.CheckContext(theContext);
			HandleShader(_isCompute, value, input.Key, out var theSource, out var theStreams, out var theDefinedStreams);
			dictionary.Add(input.Key, (theSource, theStreams));
			stringBuilder.AppendLine(theDefinedStreams);
		}
		_stopwatch.Restart();
		Dictionary<string, string> templateMap = BuildTemplateMap();
		templateMap["streamDeclaration"] = stringBuilder.ToString();
		CustomTemplates().ForEach((KeyValuePair<string, string> kv) =>
		{
			templateMap.Add(kv.Key, kv.Value);
		});
		dictionary.ForEach((KeyValuePair<string, (string source, string stream)> kv) =>
		{
			templateMap.Add("source" + kv.Key, kv.Value.source);
			templateMap.Add("streams" + kv.Key, kv.Value.stream);
		});
		ShaderCode = ShaderNodesUtil.Evaluate(_sourceTemplate, templateMap);
		ShaderCode = CheckCode(ShaderCode);
		ShaderCode = ShaderNodesUtil.FormatShaderCode(ShaderCode);
		ShaderName = "Shader_" + System.Math.Abs(ShaderCode.GetStableHashCode());
		ShaderCode = ShaderNodesUtil.Evaluate(ShaderCode, new Dictionary<string, string> { { "shaderID", ShaderName } });
		ShaderCode = ShaderNodesUtil.Evaluate(ShaderCode, (Match m) => (!m.Groups["key"].Value.StartsWith("stage")) ? m.Value : "");
		foreach (KeyValuePair<string, AbstractShaderNode> input2 in Inputs)
		{
			input2.Value.ShaderCode = ShaderCode;
		}
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"-> Evaluate: {_stopwatch.ElapsedMilliseconds} ms");
		}
		_stopwatch.Restart();
		ShaderNodesUtil.AddShaderSource(ShaderName, ShaderCode, "shaders\\" + ShaderName + ".sdsl");
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"-> AddShaderSource: {_stopwatch.ElapsedMilliseconds} ms");
		}
		ShaderClassSource val = new ShaderClassSource(ShaderName);
		_stopwatch.Stop();
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"-> Execution Time: {stopwatch.ElapsedMilliseconds} ms for Shader {ShaderName}");
		}
		return (ShaderSource)val;
	}

	public void AppendInputs(Dictionary<string, string> theTemplateMap)
	{
		foreach (AbstractStage stage in _stages)
		{
			stage?.AppendInputs(theTemplateMap);
		}
	}

	public Dictionary<string, string> AppendTemplateValues(Dictionary<string, string> theTemplateMap)
	{
		theTemplateMap["shaderType"] = TypeHelpers.GetSignature<T>();
		theTemplateMap["resultType"] = TypeHelpers.GetGpuType<T>();
		AppendInputs(theTemplateMap);
		return theTemplateMap;
	}

	private Dictionary<string, string> CustomTemplates()
	{
		return AppendTemplateValues(_customTemplate);
	}

	private Dictionary<string, string> BuildTemplateMap()
	{
		StringBuilder declarationBuilder = new StringBuilder();
		_declarations.ForEach((string declaration) =>
		{
			declarationBuilder.AppendLine(declaration);
		});
		StringBuilder groupDeclarationBuilder = new StringBuilder();
		_groupDeclarations.ForEach((string declaration) =>
		{
			groupDeclarationBuilder.AppendLine(declaration);
		});
		StringBuilder structBuilder = new StringBuilder();
		_structs.ForEach((string gpuStruct) =>
		{
			structBuilder.AppendLine(gpuStruct);
		});
		StringBuilder constantArrayBuilder = new StringBuilder();
		_constantArrays.ForEach((string array) =>
		{
			constantArrayBuilder.AppendLine(array);
		});
		StringBuilder streamBuilder = new StringBuilder();
		_streams.ForEach((string stream) =>
		{
			streamBuilder.AppendLine(stream);
		});
		StringBuilder mixinBuilder = new StringBuilder();
		_mixins.ForEach((string mixin) =>
		{
			mixinBuilder.Append(", " + mixin);
		});
		StringBuilder compositionBuilder = new StringBuilder();
		_compositions.ForEach((string composition) =>
		{
			compositionBuilder.AppendLine(composition);
		});
		StringBuilder functionBuilder = new StringBuilder();
		_functionMap?.ForEach((KeyValuePair<string, string> kv) =>
		{
			functionBuilder.AppendLine(kv.Value);
		});
		return new Dictionary<string, string>
		{
			{
				"mixins",
				mixinBuilder.ToString()
			},
			{
				"declarations",
				declarationBuilder.ToString()
			},
			{
				"compositions",
				compositionBuilder.ToString()
			},
			{
				"structs",
				structBuilder.ToString()
			},
			{
				"constantArrays",
				constantArrayBuilder.ToString()
			},
			{
				"streams",
				streamBuilder.ToString()
			},
			{
				"functions",
				functionBuilder.ToString()
			},
			{
				"groupDeclarations",
				groupDeclarationBuilder.ToString()
			}
		};
	}

	private void HandleDeclaration(FieldDeclaration theDeclaration, bool theIsComputeShader)
	{
		if (theDeclaration.IsResource)
		{
			_groupDeclarations.Add(theDeclaration.GetDeclaration(theIsComputeShader));
		}
		else
		{
			_declarations.Add(theDeclaration.GetDeclaration(theIsComputeShader));
		}
	}

	private void HandleFunction(KeyValuePair<string, string> theKeyFunction)
	{
		if (!_functionMap.ContainsKey(theKeyFunction.Key))
		{
			_functionMap.Add(theKeyFunction.Key, theKeyFunction.Value);
		}
	}

	private void HandleShader(bool theIsComputeShader, AbstractShaderNode theShaderInput, string theKey, out string theSource, out string theStreams, out string theDefinedStreams)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine("  -> HandleShader: " + theKey);
		}
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		_stopwatch.Restart();
		theShaderInput.DeclarationList().ForEach((FieldDeclaration declaration) =>
		{
			HandleDeclaration(declaration, theIsComputeShader);
		});
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"     Declaration: {_stopwatch.ElapsedMilliseconds} ms");
		}
		_stopwatch.Restart();
		theShaderInput.StructList().ForEach((string value) =>
		{
			_structs.Add(value);
		});
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"     Structs: {_stopwatch.ElapsedMilliseconds} ms");
		}
		_stopwatch.Restart();
		theShaderInput.ConstantArrayList().ForEach((string value) =>
		{
			_constantArrays.Add(value);
		});
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"     Arrays: {_stopwatch.ElapsedMilliseconds} ms");
		}
		_stopwatch.Restart();
		theShaderInput.StreamList().ForEach((string value) =>
		{
			_streams.Add(value);
		});
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"     streams: {_stopwatch.ElapsedMilliseconds} ms");
		}
		_stopwatch.Restart();
		theShaderInput.MixinList().ForEach((string value) =>
		{
			_mixins.Add(value);
		});
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"     Mixins: {_stopwatch.ElapsedMilliseconds} ms");
		}
		_stopwatch.Restart();
		theShaderInput.FunctionMap().ForEach(HandleFunction);
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"     Functions: {_stopwatch.ElapsedMilliseconds} ms");
		}
		_stopwatch.Restart();
		stringBuilder.AppendLine("        streams. = " + theKey + theShaderInput.ID + ";");
		stringBuilder2.AppendLine("    stream " + TypeHelpers.GetGpuType(theShaderInput) + " " + theKey + ";");
		theSource = theShaderInput.BuildSourceCode();
		theStreams = stringBuilder.ToString();
		theDefinedStreams = stringBuilder2.ToString();
		if (ShaderNodesUtil.TimeShaderGeneration)
		{
			Console.WriteLine($"     Finish Stage: {stopwatch.ElapsedMilliseconds} ms");
		}
	}

	private string CheckCode(string theCode)
	{
		if (!_isCompute)
		{
			return theCode.Replace("RWStructuredBuffer", "StructuredBuffer");
		}
		return theCode;
	}
}
