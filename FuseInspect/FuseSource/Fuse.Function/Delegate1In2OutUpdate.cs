namespace Fuse.Function;

public delegate void Delegate1In2OutUpdate<TIn, TOut1, TOut2>(object stateInput, out object stateOutput, ShaderNode<TIn> Input, out ShaderNode<TOut1> Output1, out ShaderNode<TOut2> Output2);
