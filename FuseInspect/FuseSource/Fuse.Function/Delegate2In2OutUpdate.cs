namespace Fuse.Function;

public delegate void Delegate2In2OutUpdate<TIn1, TIn2, TOut1, TOut2>(object stateInput, out object stateOutput, ShaderNode<TIn1> input1, ShaderNode<TIn2> input2, out ShaderNode<TOut1> output1, out ShaderNode<TOut2> output2);
