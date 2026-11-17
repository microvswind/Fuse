namespace FuseSdfToPt;

/// <summary>Small, explicit SDF IR. Fuse runtime graphs are adapted into this model before emission.</summary>
public abstract record SdfNode;

public sealed record BoxSdf(
    string Name,
    System.Numerics.Vector3 Position,
    System.Numerics.Vector3 Size) : SdfNode;

public sealed record SphereSdf(
    string Name,
    System.Numerics.Vector3 Position,
    float Radius) : SdfNode;

public enum BooleanSdfOperation
{
    Union,
    Intersect,
    Difference
}

public sealed record CombineSdf(
    string Name,
    BooleanSdfOperation Operation,
    SdfNode A,
    SdfNode B) : SdfNode;

public sealed record PtSdfExportOptions
{
    public string FunctionName { get; init; } = "mapSdf";
    public string PositionParameter { get; init; } = "p";
    public bool IncludePtMacro { get; init; } = false;
    public float? PtMaxDistance { get; init; }
}
