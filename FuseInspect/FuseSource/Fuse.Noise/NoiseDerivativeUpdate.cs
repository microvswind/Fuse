namespace Fuse.Noise;

public delegate void NoiseDerivativeUpdate<T>(object stateInput, out object stateOutput, ShaderNode<T> Domain, out ShaderNode<float> Value, out ShaderNode<T> Derivative);
