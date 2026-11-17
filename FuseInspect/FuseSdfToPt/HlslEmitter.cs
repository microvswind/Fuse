using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace FuseSdfToPt;

/// <summary>
/// Emits a dependency-first HLSL expression graph. Shared SdfNode instances are emitted once.
/// Fuse graph extraction is deliberately separate: this class consumes a normalized SDF IR.
/// </summary>
public sealed class HlslEmitter
{
    public string Emit(SdfNode root, PtSdfExportOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(root);
        options ??= new PtSdfExportOptions();
        ValidateIdentifier(options.FunctionName, nameof(options.FunctionName));
        ValidateIdentifier(options.PositionParameter, nameof(options.PositionParameter));

        var sb = new StringBuilder();
        var names = new Dictionary<SdfNode, string>(ReferenceComparer.Instance);
        var emitted = new HashSet<SdfNode>(ReferenceComparer.Instance);
        var counter = 0;

        string Visit(SdfNode node)
        {
            if (names.TryGetValue(node, out var existing))
                return existing;

            string expression;
            switch (node)
            {
                case BoxSdf box:
                    ValidateIdentifier(box.Name, nameof(box.Name));
                    if (box.Size.X <= 0 || box.Size.Y <= 0 || box.Size.Z <= 0)
                        throw new ArgumentOutOfRangeException(nameof(box.Size), "Box dimensions must be positive.");
                    expression = $"sdBox({options.PositionParameter} - {V3(box.Position)}, {V3(box.Size * 0.5f)})";
                    break;

                case SphereSdf sphere:
                    ValidateIdentifier(sphere.Name, nameof(sphere.Name));
                    if (sphere.Radius <= 0)
                        throw new ArgumentOutOfRangeException(nameof(sphere.Radius), "Sphere radius must be positive.");
                    expression = $"sdSphere({options.PositionParameter} - {V3(sphere.Position)}, {F(sphere.Radius)})";
                    break;

                case CombineSdf combine:
                    ValidateIdentifier(combine.Name, nameof(combine.Name));
                    var a = Visit(combine.A);
                    var b = Visit(combine.B);
                    expression = combine.Operation switch
                    {
                        BooleanSdfOperation.Union => $"min({a}, {b})",
                        BooleanSdfOperation.Intersect => $"max({a}, {b})",
                        BooleanSdfOperation.Difference => $"max({a}, -{b})",
                        _ => throw new NotSupportedException($"Unsupported Boolean SDF operation: {combine.Operation}")
                    };
                    break;

                default:
                    throw new NotSupportedException($"Unsupported SDF node type: {node.GetType().FullName}");
            }

            var variable = $"sdf_{++counter}";
            names.Add(node, variable);
            if (emitted.Add(node))
                sb.Append("    float ").Append(variable).Append(" = ").Append(expression).AppendLine(";");
            return variable;
        }

        var result = Visit(root);
        sb.Insert(0, "#include \"PT_BoxSDF.hlsli\"\n#include \"PT_SphereSDF.hlsli\"\n\n");
        sb.AppendLine();
        sb.Append("float ").Append(options.FunctionName).Append("(float3 ")
          .Append(options.PositionParameter).AppendLine(")")
          .AppendLine("{")
          .Append("    return ").Append(result).AppendLine(";")
          .AppendLine("}");

        if (options.IncludePtMacro)
        {
            if (options.PtMaxDistance is null || options.PtMaxDistance <= 0)
                throw new ArgumentOutOfRangeException(nameof(options.PtMaxDistance),
                    "A positive PtMaxDistance is required when IncludePtMacro is enabled.");
            sb.AppendLine()
              .AppendLine("PT_SDF_IMPLEMENT_FULL(")
              .Append("    ").Append(F(options.PtMaxDistance.Value)).AppendLine(",")
              .Append("    ").Append(options.FunctionName).AppendLine(",")
              .AppendLine("    PT_SDF_NORMAL_NUMERIC,")
              .AppendLine("    PT_SDF_UV_PLANAR")
              .AppendLine(")");
        }

        return sb.ToString();
    }

    private static string V3(Vector3 v) => $"float3({F(v.X)}, {F(v.Y)}, {F(v.Z)})";
    private static string F(float value) => value.ToString("0.0######", CultureInfo.InvariantCulture) + "f";

    private static void ValidateIdentifier(string value, string parameter)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            !(char.IsLetter(value[0]) || value[0] == '_') ||
            value.Any(c => !(char.IsLetterOrDigit(c) || c == '_')))
            throw new ArgumentException($"'{value}' is not a valid HLSL identifier.", parameter);
    }

    private sealed class ReferenceComparer : IEqualityComparer<SdfNode>
    {
        public static readonly ReferenceComparer Instance = new();
        public bool Equals(SdfNode? x, SdfNode? y) => ReferenceEquals(x, y);
        public int GetHashCode(SdfNode obj) => RuntimeHelpers.GetHashCode(obj);
    }
}
