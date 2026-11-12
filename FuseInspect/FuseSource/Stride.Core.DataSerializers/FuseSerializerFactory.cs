using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.InteropServices;
using Fuse;
using Fuse.Calculus;
using Fuse.Color;
using Fuse.ComputeSystem;
using Fuse.Core.Base;
using Fuse.Domain;
using Fuse.Math;
using Fuse.Noise;
using Fuse.SDF;
using Fuse.Texturing;
using Fuse.compute;
using Fuse.function;
using Fuse.math;
using Stride.Core.Reflection;
using Stride.Core.Serialization;
using Stride.Core.Serialization.Serializers;
using Stride.Core.Storage;

namespace Stride.Core.DataSerializers;

[StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
[DataSerializerGlobal(/*Could not decode attribute arguments.*/)]
public static class FuseSerializerFactory
{
	internal static void Initialize()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected Obj, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected Obj, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Expected Obj, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Expected Obj, but got Unknown
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected Obj, but got Unknown
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected Obj, but got Unknown
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected Obj, but got Unknown
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Expected Obj, but got Unknown
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Expected Obj, but got Unknown
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Expected Obj, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Expected Obj, but got Unknown
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Expected Obj, but got Unknown
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Expected Obj, but got Unknown
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Expected Obj, but got Unknown
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Expected Obj, but got Unknown
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Expected Obj, but got Unknown
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Expected Obj, but got Unknown
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Expected Obj, but got Unknown
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Expected Obj, but got Unknown
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Expected Obj, but got Unknown
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Expected Obj, but got Unknown
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Expected Obj, but got Unknown
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Expected Obj, but got Unknown
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Expected Obj, but got Unknown
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Expected Obj, but got Unknown
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Expected Obj, but got Unknown
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Expected Obj, but got Unknown
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Expected Obj, but got Unknown
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b7: Expected Obj, but got Unknown
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Expected Obj, but got Unknown
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Expected Obj, but got Unknown
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0855: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Expected Obj, but got Unknown
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Expected Obj, but got Unknown
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Expected Obj, but got Unknown
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Expected Obj, but got Unknown
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Unknown result type (might be due to invalid IL or missing references)
		//IL_093f: Expected Obj, but got Unknown
		//IL_093f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_096d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Expected Obj, but got Unknown
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09af: Expected Obj, but got Unknown
		//IL_09af: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Expected Obj, but got Unknown
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1f: Expected Obj, but got Unknown
		//IL_0a1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a57: Expected Obj, but got Unknown
		//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8f: Expected Obj, but got Unknown
		//IL_0a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac7: Expected Obj, but got Unknown
		//IL_0ac7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aff: Expected Obj, but got Unknown
		//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b37: Expected Obj, but got Unknown
		//IL_0b37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6f: Expected Obj, but got Unknown
		//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba7: Expected Obj, but got Unknown
		//IL_0ba7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdf: Expected Obj, but got Unknown
		//IL_0bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c17: Expected Obj, but got Unknown
		//IL_0c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Expected Obj, but got Unknown
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c87: Expected Obj, but got Unknown
		//IL_0c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbf: Expected Obj, but got Unknown
		//IL_0cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ced: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf7: Expected Obj, but got Unknown
		//IL_0cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2f: Expected Obj, but got Unknown
		//IL_0d34: Expected Obj, but got Unknown
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d49: Expected Obj, but got Unknown
		//IL_0d49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5e: Expected Obj, but got Unknown
		//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d73: Expected Obj, but got Unknown
		//IL_0d78: Expected Obj, but got Unknown
		AssemblySerializers val = new AssemblySerializers(typeof(FuseSerializerFactory).GetTypeInfo().Assembly)
		{
			Modules = { typeof(VL_CoreSerializerFactory).GetTypeInfo().Module },
			Modules = { typeof(Stride_GraphicsSerializerFactory).GetTypeInfo().Module },
			Modules = { typeof(VL_Stride_RuntimeSerializerFactory).GetTypeInfo().Module },
			Modules = { typeof(Stride_RenderingSerializerFactory).GetTypeInfo().Module },
			Modules = { typeof(Stride_Core_MathematicsSerializerFactory).GetTypeInfo().Module },
			Modules = { typeof(StrideSerializerFactory).GetTypeInfo().Module },
			Modules = { typeof(Stride_CoreSerializerFactory).GetTypeInfo().Module },
			Modules = { typeof(Stride_ShadersSerializerFactory).GetTypeInfo().Module },
			Modules = { typeof(Stride_EngineSerializerFactory).GetTypeInfo().Module },
			Modules = { typeof(Stride_Shaders_ParserSerializerFactory).GetTypeInfo().Module }
		};
		Dictionary<string, AssemblySerializersPerProfile> profiles = val.Profiles;
		AssemblySerializersPerProfile val2 = new AssemblySerializersPerProfile();
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(31204646u, 3761078719u, 626486594u, 871154646u), typeof(Reorder3DMode), typeof(EnumSerializer<Reorder3DMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(2966736221u, 2194499692u, 4201212905u, 1739703904u), typeof(BufferType), typeof(EnumSerializer<BufferType>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(564786676u, 3018320114u, 1619815559u, 1737722658u), typeof(InputPrimitiveType), typeof(EnumSerializer<InputPrimitiveType>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3001106382u, 2585012291u, 922046714u, 3058530897u), typeof(OutputPrimitiveType), typeof(EnumSerializer<OutputPrimitiveType>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1681459799u, 1079137907u, 3360296535u, 856772297u), typeof(HandleNullInputMode), typeof(EnumSerializer<HandleNullInputMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(2818959785u, 1948718816u, 3603879860u, 1559030664u), typeof(TextureInterpolation), typeof(EnumSerializer<TextureInterpolation>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1620204260u, 4075789043u, 859861253u, 2081782987u), typeof(BoxGridMode), typeof(EnumSerializer<BoxGridMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1620680400u, 2314010571u, 1606430698u, 4140087379u), typeof(DistanceMetricsSelector), typeof(EnumSerializer<DistanceMetricsSelector>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3780884974u, 2641027561u, 1629435034u, 1177672897u), typeof(InputModifier), typeof(EnumSerializer<InputModifier>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3801311394u, 3932342238u, 2808392514u, 3636494781u), typeof(CombineSDFSimple), typeof(EnumSerializer<CombineSDFSimple>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(2735222860u, 3575491681u, 837248493u, 2738675346u), typeof(CombineSDF), typeof(EnumSerializer<CombineSDF>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(768110096u, 2075739708u, 3403059472u, 3902724595u), typeof(CombineMode), typeof(EnumSerializer<CombineMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1580558011u, 3618741100u, 1752178870u, 3132847313u), typeof(CombineSmooth), typeof(EnumSerializer<CombineSmooth>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(640018705u, 2874445416u, 3522012357u, 2755001773u), typeof(CombineBlend), typeof(EnumSerializer<CombineBlend>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3090257998u, 847318348u, 1432469173u, 4262622249u), typeof(ExtrudeSDF), typeof(EnumSerializer<ExtrudeSDF>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1851074714u, 3843700283u, 2231085300u, 2864149573u), typeof(GridSDF), typeof(EnumSerializer<GridSDF>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3306679796u, 2886831197u, 2541905542u, 4187129889u), typeof(TruchetSDF), typeof(EnumSerializer<TruchetSDF>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(2800688515u, 1022129940u, 4028477542u, 4011890886u), typeof(TPMSSDF), typeof(EnumSerializer<TPMSSDF>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3886478700u, 1587096960u, 475908248u, 2367042307u), typeof(StrutSDF), typeof(EnumSerializer<StrutSDF>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(568399847u, 3976135801u, 1240300723u, 1430990380u), typeof(RepeatSDF), typeof(EnumSerializer<RepeatSDF>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(4153019535u, 451918392u, 2865404745u, 1344188644u), typeof(SDFPolarity), typeof(EnumSerializer<SDFPolarity>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3720799793u, 3504413696u, 3177399494u, 443198003u), typeof(CombineSDFSelector), typeof(EnumSerializer<CombineSDFSelector>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(214062838u, 3620046116u, 3901782279u, 4115696511u), typeof(NoiseBasisType), typeof(EnumSerializer<NoiseBasisType>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1946229649u, 2474914302u, 3554455641u, 1122757649u), typeof(NoiseBasisInflection), typeof(EnumSerializer<NoiseBasisInflection>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(4001664403u, 3716779446u, 2943105464u, 2020825145u), typeof(FBMBasisType), typeof(EnumSerializer<FBMBasisType>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3391135727u, 1546803369u, 2285794348u, 486371718u), typeof(WorleyCellDistance), typeof(EnumSerializer<WorleyCellDistance>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3731141115u, 4046094742u, 485251382u, 1279821498u), typeof(WorleyCellFunction), typeof(EnumSerializer<WorleyCellFunction>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(440855608u, 17778121u, 1024271226u, 544845718u), typeof(Constrain), typeof(EnumSerializer<Constrain>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3915653841u, 877665581u, 3572446815u, 2294214402u), typeof(EaseMode), typeof(EnumSerializer<EaseMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(506283864u, 2249861330u, 2269880104u, 1184102690u), typeof(BlendMode), typeof(EnumSerializer<BlendMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3141886046u, 630546513u, 2931709741u, 4113605157u), typeof(InterpolationMode), typeof(EnumSerializer<InterpolationMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(2990172059u, 3778981313u, 3181777928u, 16458839u), typeof(SplineMode), typeof(EnumSerializer<SplineMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1039091016u, 4152678259u, 3346007730u, 2137772516u), typeof(NeighborSplineMode), typeof(EnumSerializer<NeighborSplineMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3858044781u, 783404980u, 2606964030u, 3106371150u), typeof(AxisMode), typeof(EnumSerializer<AxisMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(327061353u, 1321868335u, 2536512493u, 3734784344u), typeof(Space), typeof(EnumSerializer<Space>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(2115778353u, 1988793230u, 2382237273u, 1613447929u), typeof(DistanceMetric), typeof(EnumSerializer<DistanceMetric>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1393187411u, 2920079430u, 1668696071u, 1801165043u), typeof(DomainRepeat2D), typeof(EnumSerializer<DomainRepeat2D>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3652974347u, 1964958869u, 4289668844u, 2080621669u), typeof(DomainRepeat3D), typeof(EnumSerializer<DomainRepeat3D>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(550842968u, 2913611720u, 1120513011u, 983272381u), typeof(DomainAlignment), typeof(EnumSerializer<DomainAlignment>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3440089320u, 3391142055u, 81462171u, 1323001066u), typeof(SphericalMapping), typeof(EnumSerializer<SphericalMapping>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3320742u, 2254796790u, 790511829u, 2470331778u), typeof(PickOneXY), typeof(EnumSerializer<PickOneXY>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1491038673u, 3725806803u, 1756858035u, 3498845802u), typeof(PickOneXYZ), typeof(EnumSerializer<PickOneXYZ>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1522653835u, 2366325146u, 2355342671u, 2968160132u), typeof(PickOneXYZW), typeof(EnumSerializer<PickOneXYZW>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3236100104u, 3182430693u, 1261114880u, 1780779762u), typeof(PickTwoXYZ), typeof(EnumSerializer<PickTwoXYZ>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1264325276u, 1932005797u, 3900673489u, 2955288208u), typeof(ColorSpace), typeof(EnumSerializer<ColorSpace>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3816918410u, 1983041651u, 745691843u, 46425591u), typeof(Spectrum), typeof(EnumSerializer<Spectrum>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(2122997077u, 648599463u, 1553676475u, 3065905405u), typeof(Tonemap), typeof(EnumSerializer<Tonemap>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(84941450u, 79859205u, 285433067u, 3106615033u), typeof(CuttoffAttenuationMode), typeof(EnumSerializer<CuttoffAttenuationMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(433370134u, 3996271757u, 3212398661u, 3542804368u), typeof(GradientDifferentiationMode), typeof(EnumSerializer<GradientDifferentiationMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1216368933u, 2449524701u, 3681701817u, 3977880280u), typeof(IntegrationMode), typeof(EnumSerializer<IntegrationMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(2161517798u, 3452192416u, 1811228730u, 1024136989u), typeof(ComputeIntegrationMode), typeof(EnumSerializer<ComputeIntegrationMode>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(2977119984u, 1299914003u, 457679101u, 295195287u), typeof(ComputeIntegrationModeRegion), typeof(EnumSerializer<ComputeIntegrationModeRegion>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(1141987318u, 658454860u, 1751165110u, 511314841u), typeof(ParticleIntegrationMotion), typeof(EnumSerializer<ParticleIntegrationMotion>)));
		((Collection<AssemblySerializerEntry>)val2).Add(new AssemblySerializerEntry(new ObjectId(3082505950u, 118193693u, 2545989565u, 368006569u), typeof(AttributeType), typeof(EnumSerializer<AttributeType>)));
		profiles["Default"] = val2;
		val.Profiles["Hash"] = new AssemblySerializersPerProfile();
		val.Profiles["Content"] = new AssemblySerializersPerProfile();
		val.Profiles["Clone"] = new AssemblySerializersPerProfile();
		DataSerializerFactory.RegisterSerializationAssembly(val);
		AssemblyRegistry.Register(typeof(FuseSerializerFactory).GetTypeInfo().Assembly, new string[1] { "engine" });
	}
}
