using System.Numerics;
using FuseSdfToPt;

// Smoke test matching the six-Box Boolean topology used in the Fuse/PT example.
var a = new BoxSdf("A", Vector3.Zero, new Vector3(0.80f, 0.50f, 1.00f));
var b = new BoxSdf("B", Vector3.Zero, new Vector3(0.76f, 0.46f, 0.96f));
var c = new BoxSdf("C", new Vector3(0, -0.05f, 0), new Vector3(0.15f, 0.36f, 1.50f));
var d = new BoxSdf("D", new Vector3(0, -0.05f, 0), new Vector3(0.15f, 0.36f, 1.00f));
var e = new BoxSdf("E", new Vector3(0, -0.06f, 0), new Vector3(0.13f, 0.35f, 2.00f));
var f = new BoxSdf("F", new Vector3(0, -0.10f, 0), new Vector3(0.20f, 1.00f, 0.94f));

var right = new CombineSdf("right", BooleanSdfOperation.Difference, a,
    new CombineSdf("rightCut", BooleanSdfOperation.Union, b, c));
var left = new CombineSdf("left", BooleanSdfOperation.Difference, d,
    new CombineSdf("leftCut", BooleanSdfOperation.Union, e, f));
var root = new CombineSdf("root", BooleanSdfOperation.Union, right, left);

var hlsl = new HlslEmitter().Emit(root, new PtSdfExportOptions
{
    FunctionName = "combineSDF",
    PositionParameter = "p"
});

if (!hlsl.Contains("return sdf_") ||
    !hlsl.Contains("sdBox(p - float3(0.0f, -0.05f, 0.0f), float3(0.075f, 0.18f, 0.75f))") ||
    !hlsl.Contains("max(sdf_"))
    throw new Exception("SDF emitter smoke test failed.");

Console.WriteLine(hlsl);
Console.WriteLine("// SMOKE TEST PASSED");
