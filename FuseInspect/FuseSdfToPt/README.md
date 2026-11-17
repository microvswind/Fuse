# Fuse SDF → NVIDIA PT HLSL exporter (core, first slice)

This project is the first executable slice of the exporter. It defines a small SDF intermediate representation and emits HLSL for:

- Box SDF (Fuse full dimensions are converted to half extents for `sdBox`)
- Sphere SDF
- Boolean Union / Intersect / Difference
- Nested Boolean graphs and shared node instances

## Run

```powershell
dotnet run --project .\FuseSdfToPt\FuseSdfToPt.csproj
```

The console program builds the six-box Boolean topology and performs smoke assertions against the expected HLSL fragments.

## Scope and next integration step

This is **not yet a VL node package** and does not yet extract a live `Fuse.function.Delegate<T>`. The adapter still needs to bind the live Delegate's `Functions`, parameter metadata and node/input values to this IR. That is intentionally a separate layer: the emitter can be tested independently before connecting it to vvvv's runtime graph.

The PT macro is opt-in because the macro's exact invocation contract depends on the user's PT shader-file context. The exporter emits the function body by default.
