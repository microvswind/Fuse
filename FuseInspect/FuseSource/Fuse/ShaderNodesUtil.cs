using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Fuse.ShaderFX;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Games;
using Stride.Rendering;
using Stride.Rendering.Materials;
using Stride.Shaders.Compiler;
using Stride.Shaders.Parser;
using VL.Core;
using VL.Stride;
using VL.Stride.Rendering;
using VL.Stride.Rendering.ComputeEffect;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public static class ShaderNodesUtil
{
	private class DynamicDrawEffectInstance(string effectName, ParameterCollection parameters = null) : DynamicEffectInstance(effectName, parameters)
	{
		public readonly CompositeDisposable Subscriptions = new CompositeDisposable();

		protected override void Destroy()
		{
			Subscriptions.Dispose();
			((EffectInstance)this).Destroy();
		}
	}

	public readonly struct NodeContextOverride : IDisposable
	{
		private readonly NodeContext _previous;

		internal NodeContextOverride(NodeContext nodeContext)
		{
			_previous = Interlocked.Exchange(ref _sCurrentParentContext, nodeContext);
		}

		public void Dispose()
		{
			Interlocked.Exchange(ref _sCurrentParentContext, _previous);
		}
	}

	public static bool DebugShaderGeneration = false;

	public static bool DebugVisit = false;

	public static bool TimeShaderGeneration = false;

	public static string DebugIdent = ". ";

	private static readonly PropertyKey<int> VarIDCounterKey = new PropertyKey<int>("Fuse.FuseIDCounter", typeof(int), new PropertyKeyMetadata[1] { (PropertyKeyMetadata)DefaultValueMetadata.Static<int>(0, true) });

	public static int Id2;

	[ThreadStatic]
	private static NodeContext _sCurrentParentContext;

	public static void SetConsoleOut(string path)
	{
		StreamWriter streamWriter = new StreamWriter(new FileStream(path, FileMode.Create))
		{
			AutoFlush = true
		};
		Console.SetOut(streamWriter);
		Console.SetError(streamWriter);
	}

	public static string BuildArguments(IEnumerable<AbstractShaderNode> inputs)
	{
		StringBuilder stringBuilder = new StringBuilder();
		inputs.ForEach((AbstractShaderNode input) =>
		{
			if (input != null)
			{
				stringBuilder.Append(input.ID);
				stringBuilder.Append(", ");
			}
		});
		if (stringBuilder.Length > 2)
		{
			stringBuilder.Remove(stringBuilder.Length - 2, 2);
		}
		return stringBuilder.ToString();
	}

	public static bool HasNullValue<T>(IEnumerable<T> theSequence)
	{
		return theSequence.Any((T value) => value == null);
	}

	public static string IndentCode(string theCode)
	{
		string[] list = theCode.Split(new string[1] { Environment.NewLine }, StringSplitOptions.None);
		StringBuilder myStringBuilder = new StringBuilder();
		list.ForEach((string line) =>
		{
			myStringBuilder.AppendLine("    " + line);
		});
		return myStringBuilder.ToString();
	}

	public static string FirstLetterToUpper(string str)
	{
		if (str == null)
		{
			return null;
		}
		if (str.Length > 1)
		{
			return char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);
		}
		return str.ToUpper();
	}

	public static string FirstLetterToLower(string str)
	{
		if (str == null)
		{
			return null;
		}
		if (str.Length > 1)
		{
			return char.ToLower(str[0]) + str.Substring(1, str.Length - 1);
		}
		return str.ToLower();
	}

	public static string Evaluate(string theShaderTemplate, IDictionary<string, string> theKeys)
	{
		return Regex.Replace(theShaderTemplate, "\\$\\{(?<key>[^}]+)\\}", (Match m) => (!theKeys.ContainsKey(m.Groups["key"].Value)) ? m.Value : theKeys[m.Groups["key"].Value]);
	}

	public static string Evaluate(string theShaderTemplate, MatchEvaluator theEvaluator)
	{
		return Regex.Replace(theShaderTemplate, "\\$\\{(?<key>[^}]+)\\}", theEvaluator);
	}

	public static string EvaluateIDs(string theShaderTemplate)
	{
		int id = 0;
		Dictionary<string, string> idMap = new Dictionary<string, string>();
		return Regex.Replace(theShaderTemplate, "\\$\\{#(?<key>[^}]+)\\}", (Match m) =>
		{
			string value = m.Groups["key"].Value;
			if (!idMap.ContainsKey(value))
			{
				idMap[value] = id.ToString() ?? "";
				id++;
			}
			return idMap[value];
		});
	}

	public static string CleanVlClassName(string theVlClassName)
	{
		return Regex.Replace(theVlClassName, "_.*", "");
	}

	public static string FixName(string theName)
	{
		return theName.Replace(".", "").Replace(" ", "");
	}

	public static string FormatShaderCode(string shaderCode)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		bool flag = false;
		using (StringReader stringReader = new StringReader(shaderCode))
		{
			while (true)
			{
				string text = stringReader.ReadLine();
				if (text == null)
				{
					break;
				}
				text = text.Trim();
				if (text.Length == 0)
				{
					if (!flag)
					{
						stringBuilder.AppendLine();
						flag = true;
					}
					continue;
				}
				flag = false;
				if (text.Contains('}'))
				{
					num--;
					num = System.Math.Max(num, 0);
				}
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(0, 2, stringBuilder2);
				handler.AppendFormatted(new string("    "[0], num * "    ".Length));
				handler.AppendFormatted(text);
				stringBuilder2.AppendLine(ref handler);
				if (text.Contains('{'))
				{
					num++;
				}
			}
		}
		return stringBuilder.ToString();
	}

	public static void AddShaderSource(string type, string sourceCode, string sourcePath)
	{
		try
		{
			Game resource = Resources.GetGameProvider((IServiceProvider)AppHost.Current.Services).GetHandle().Resource;
			if (resource == null)
			{
				return;
			}
			EffectSystem effectSystem = resource.EffectSystem;
			IEffectCompiler compiler = effectSystem.Compiler;
			EffectCompiler val = (EffectCompiler)(object)((compiler is EffectCompiler) ? compiler : null);
			if (val == null)
			{
				IEffectCompiler compiler2 = effectSystem.Compiler;
				EffectCompilerCache val2 = (EffectCompilerCache)(object)((compiler2 is EffectCompilerCache) ? compiler2 : null);
				if (val2 != null)
				{
					object? obj = typeof(EffectCompilerChain).GetProperty("Compiler", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(val2);
					val = (EffectCompiler)((obj is EffectCompiler) ? obj : null);
				}
			}
			if (val == null)
			{
				return;
			}
			MethodInfo method = typeof(EffectCompiler).GetMethod("GetMixinParser", BindingFlags.Instance | BindingFlags.NonPublic);
			if (!(method == null))
			{
				object? obj2 = method.Invoke(val, null);
				ShaderMixinParser val3 = (ShaderMixinParser)((obj2 is ShaderMixinParser) ? obj2 : null);
				if (val3 != null)
				{
					val3.SourceManager.AddShaderSource(type, sourceCode, sourcePath);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public static VLComputeEffectShader RegisterComputeShader<T>(ToComputeFx<T> theComputeFx, string effectName = "FuseComputeGraph")
	{
		Game resource = Resources.GetGameProvider((IServiceProvider)AppHost.Current.Services).GetHandle().Resource;
		if (resource == null)
		{
			return null;
		}
		IComputeVoid val = ShaderGraph.BuildFinalShaderGraph((IComputeNode)(object)theComputeFx, (IEnumerable<IComputeNode>)null);
		VLComputeEffectShader val2 = ShaderGraph.ComposeComputeShader(((GameBase)resource).GraphicsDevice, (IServiceRegistry)(object)((GameBase)resource).Services, val);
		((ComponentBase)val2).Name = effectName;
		return val2;
	}

	public static DynamicEffectInstance RegisterDrawShader(ToDrawFX theDrawShader)
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected Obj, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected Obj, but got Unknown
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		Game resource = Resources.GetGameProvider((IServiceProvider)AppHost.Current.Services).GetHandle().Resource;
		if (resource == null)
		{
			return null;
		}
		DynamicDrawEffectInstance dynamicDrawEffectInstance = new DynamicDrawEffectInstance("ShaderFXGraphEffect");
		object obj = typeof(ShaderGraph).GetMethod("NewShaderGeneratorContext", BindingFlags.Static | BindingFlags.NonPublic)?.Invoke(null, new object[3]
		{
			((GameBase)resource).GraphicsDevice,
			((EffectInstance)dynamicDrawEffectInstance).Parameters,
			dynamicDrawEffectInstance.Subscriptions
		});
		MaterialComputeColorKeys baseKeys = new MaterialComputeColorKeys(MaterialKeys.DiffuseMap, (ParameterKey)(object)MaterialKeys.DiffuseValue, (Nullable)Color.White, true);
		theDrawShader.GenerateShaderSource((ShaderGeneratorContext)obj, baseKeys);
		((DynamicEffectInstance)dynamicDrawEffectInstance).EffectName = theDrawShader.ShaderName;
		if (DebugShaderGeneration)
		{
			Console.WriteLine($"Register Time: {stopwatch.ElapsedMilliseconds} ms for Shader {theDrawShader.ShaderName}");
		}
		return (DynamicEffectInstance)(object)dynamicDrawEffectInstance;
	}

	public static ToShaderFX<T> RegisterShaderFX<T>(ShaderNode<T> theGpuValue, bool isCompute = true) where T : struct
	{
		return new ToShaderFX<T>(theGpuValue);
	}

	public static void ListInputs(List<IGpuInput> theInputs, AbstractShaderNode theGpuValue)
	{
		if (theGpuValue != null)
		{
			theInputs.AddRange(theGpuValue.InputList());
		}
	}

	public static Dictionary<string, List<TProperty>> PropertiesForTree<TProperty>(AbstractShaderNode theNode)
	{
		return theNode.PropertiesForTree<TProperty>();
	}

	public static List<TProperty> PropertiesForTreeList<TProperty>(AbstractShaderNode theNode, string theThePropertyId)
	{
		return theNode.PropertyForTree<TProperty>(theThePropertyId);
	}

	public static List<TProperty> PropertiesForTreeList<TProperty>(AbstractShaderNode theNode)
	{
		return theNode.PropertiesForTreeList<TProperty>();
	}

	internal static int GetAndIncIDCount(this ShaderGeneratorContext context)
	{
		int num = ((ComponentBase)context).Tags.Get<int>(VarIDCounterKey);
		((ComponentBase)context).Tags.Set<int>(VarIDCounterKey, num + 1);
		return num;
	}

	public static int GetAndIncIDCount3()
	{
		return Id2++;
	}

	public static int GetStableHashCode(this string str)
	{
		int num = 5381;
		int num2 = num;
		for (int i = 0; i < str.Length && str[i] != '\0'; i += 2)
		{
			num = ((num << 5) + num) ^ str[i];
			if (i == str.Length - 1 || str[i + 1] == '\0')
			{
				break;
			}
			num2 = ((num2 << 5) + num2) ^ str[i + 1];
		}
		return num + num2 * 1566083941;
	}

	public static Matrix GetParentTransform(RenderDrawContext renderDrawContext)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return ((ComponentBase)renderDrawContext.RenderContext).Tags.Get<Matrix>(EntityRendererRenderFeature.CurrentParentTransformation);
	}

	public static NodeContextOverride SetParentContext(NodeContext nodeContext)
	{
		return new NodeContextOverride(nodeContext);
	}

	public static uint GetHashCode(NodeContext nodeContext)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		NodePath path = nodeContext.Path;
		return (uint)((object)path/*cast due to constrained. prefix*/).ToString().GetStableHashCode();
	}
}
