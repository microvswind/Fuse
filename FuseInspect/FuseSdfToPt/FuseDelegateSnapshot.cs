using System.Collections;
using System.Reflection;

namespace FuseSdfToPt;

/// <summary>
/// Reads the public runtime surface of Fuse.function.Delegate&lt;T&gt; without taking a
/// compile-time dependency on Fuse/Stride. No private-field traversal is used.
/// </summary>
public sealed record FuseParameterSnapshot(string Id, string GpuType, string Modifier);

public sealed record FuseDelegateSnapshot(
    string RuntimeType,
    string FunctionName,
    string ReturnType,
    IReadOnlyList<FuseParameterSnapshot> Parameters,
    IReadOnlyDictionary<string, string> Functions)
{
    public static FuseDelegateSnapshot Capture(object liveDelegate)
    {
        ArgumentNullException.ThrowIfNull(liveDelegate);
        var type = liveDelegate.GetType();

        var functionName = ReadString(type, liveDelegate, "FunctionName")
            ?? throw new InvalidOperationException($"{type.FullName} has no readable FunctionName.");
        var functionsObject = ReadProperty(type, liveDelegate, "Functions")
            ?? throw new InvalidOperationException($"{type.FullName}.Functions is null.");
        var functions = new Dictionary<string, string>(StringComparer.Ordinal);

        if (functionsObject is not IEnumerable entries)
            throw new InvalidOperationException("Delegate.Functions is not enumerable.");

        foreach (var entry in entries)
        {
            if (entry is null) continue;
            var entryType = entry.GetType();
            var key = ReadString(entryType, entry, "Key");
            var value = ReadString(entryType, entry, "Value");
            if (key is not null && value is not null)
                functions[key] = value;
        }

        var parametersObject = ReadProperty(type, liveDelegate, "Parameters");
        var parameters = new List<FuseParameterSnapshot>();
        if (parametersObject is IEnumerable parameterItems)
        {
            foreach (var parameter in parameterItems)
            {
                if (parameter is null) continue;
                var parameterType = parameter.GetType();
                var id = ReadString(parameterType, parameter, "ID") ?? "unnamed";
                var gpuType = InvokeString(parameter, "TypeName") ?? "unknown";
                var modifier = InvokeString(parameter, "ModifierString") ?? string.Empty;
                parameters.Add(new FuseParameterSnapshot(id, gpuType, modifier));
            }
        }

        var returnType = FindGenericReturnType(type) ?? "unknown";
        return new FuseDelegateSnapshot(type.FullName ?? type.Name, functionName,
            returnType, parameters, functions);
    }

    private static string? FindGenericReturnType(Type type)
    {
        for (var current = type; current is not null; current = current.BaseType)
        {
            if (current.IsGenericType && current.GetGenericTypeDefinition().FullName == "Fuse.function.Delegate`1")
            {
                var argument = current.GetGenericArguments()[0];
                return GpuTypeName(argument);
            }
        }
        return null;
    }

    private static string GpuTypeName(Type type)
    {
        if (type == typeof(float)) return "float";
        if (type == typeof(int)) return "int";
        if (type == typeof(uint)) return "uint";
        if (type == typeof(System.Numerics.Vector2)) return "float2";
        if (type == typeof(System.Numerics.Vector3)) return "float3";
        if (type == typeof(System.Numerics.Vector4)) return "float4";
        return type.Name;
    }

    private static object? ReadProperty(Type type, object instance, string name) =>
        type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public)?.GetValue(instance);

    private static string? ReadString(Type type, object instance, string name) =>
        ReadProperty(type, instance, name)?.ToString();

    private static string? InvokeString(object instance, string methodName)
    {
        var method = instance.GetType().GetMethod(methodName,
            BindingFlags.Instance | BindingFlags.Public, binder: null, Type.EmptyTypes, modifiers: null);
        return method?.Invoke(instance, null)?.ToString();
    }
}
