using System;
using System.Collections.Generic;
using Fuse.ShaderFX;
using Fuse.compute;

namespace Fuse;

public class GeometryStage : AbstractStage
{
	private const string ShaderSource = "\r\n    [maxvertexcount(${numVertex})]\r\n\tstage void GSMain(${inputType} Input input[${numElements}], inout ${outputType}<Output> outputStream)\r\n\t{\r\n        ${sourceGS}\r\n\t}";

	private readonly InputPrimitiveType _myInputPrimitiveType;

	private readonly int _myMaximumVertexCount;

	private readonly OutputPrimitiveType _myOutputPrimitiveType;

	public GeometryStage(ShaderNode<GpuVoid> theGeometryNode, int theMaximumVertexCount, InputPrimitiveType theInputPrimitiveType, OutputPrimitiveType theOutputPrimitive)
		: base("GS", theGeometryNode)
	{
		_myMaximumVertexCount = theMaximumVertexCount;
		_myInputPrimitiveType = theInputPrimitiveType;
		_myOutputPrimitiveType = theOutputPrimitive;
	}

	private string InputPrimitiveTypeString()
	{
		return _myInputPrimitiveType switch
		{
			InputPrimitiveType.Point => "point", 
			InputPrimitiveType.Line => "line", 
			InputPrimitiveType.Triangle => "triangle", 
			InputPrimitiveType.LineAdjacency => "lineadj", 
			InputPrimitiveType.TriangleAdjacency => "triangleadj", 
			_ => throw new ArgumentOutOfRangeException(), 
		};
	}

	private string InputPrimitiveTypeNumElements()
	{
		return _myInputPrimitiveType switch
		{
			InputPrimitiveType.Point => "1", 
			InputPrimitiveType.Line => "2", 
			InputPrimitiveType.Triangle => "3", 
			InputPrimitiveType.LineAdjacency => "4", 
			InputPrimitiveType.TriangleAdjacency => "6", 
			_ => throw new ArgumentOutOfRangeException(), 
		};
	}

	private string OutputPrimitiveTypeString()
	{
		return _myOutputPrimitiveType switch
		{
			OutputPrimitiveType.Point => "PointStream", 
			OutputPrimitiveType.Line => "LineStream", 
			OutputPrimitiveType.Triangle => "TriangleStream", 
			_ => throw new ArgumentOutOfRangeException(), 
		};
	}

	public override void AppendInputs(Dictionary<string, string> theTemplateMap)
	{
		theTemplateMap["numVertex"] = _myMaximumVertexCount.ToString();
		theTemplateMap["inputType"] = InputPrimitiveTypeString();
		theTemplateMap["numElements"] = InputPrimitiveTypeNumElements();
		theTemplateMap["outputType"] = OutputPrimitiveTypeString();
	}

	public override string Source()
	{
		return "\r\n    [maxvertexcount(${numVertex})]\r\n\tstage void GSMain(${inputType} Input input[${numElements}], inout ${outputType}<Output> outputStream)\r\n\t{\r\n        ${sourceGS}\r\n\t}";
	}
}
