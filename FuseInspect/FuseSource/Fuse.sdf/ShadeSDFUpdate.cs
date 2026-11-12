using Stride.Core.Mathematics;

namespace Fuse.sdf;

public delegate void ShadeSDFUpdate(object stateInput, out object stateOutput, ShaderNode<MarchSurface> Surface, ShaderNode<MarchRay> Ray, out ShaderNode<Vector4> Color);
