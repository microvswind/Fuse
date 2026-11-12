using System.Collections.Generic;

namespace Fuse;

public class FieldDeclaration
{
	private const string DeclarationTemplate = "\r\n        [Link(\"${inputName}\")]\r\n        stage ${inputType} ${inputName};";

	private string _computeShaderDeclaration;

	private string _declaration;

	public string TypeName { get; private set; }

	public bool IsResource { get; }

	public FieldDeclaration(bool isResource, string computeShaderDeclaration, string declaration, string theID)
	{
		IsResource = isResource;
		Set(computeShaderDeclaration, declaration, theID);
	}

	public FieldDeclaration(string declaration)
	{
		_computeShaderDeclaration = declaration;
		_declaration = declaration;
	}

	private string BuildDeclaration(string theTypeName, string theID)
	{
		return ShaderNodesUtil.Evaluate("\r\n        [Link(\"${inputName}\")]\r\n        stage ${inputType} ${inputName};", new Dictionary<string, string>
		{
			{ "inputName", theID },
			{ "inputType", theTypeName }
		});
	}

	public void Set(string computeShaderTypeName, string typeName, string theID)
	{
		TypeName = typeName;
		_computeShaderDeclaration = BuildDeclaration(computeShaderTypeName, theID);
		_declaration = BuildDeclaration(typeName, theID);
	}

	public string GetDeclaration(bool theIsComputeShader)
	{
		if (!theIsComputeShader || _computeShaderDeclaration == null)
		{
			return _declaration;
		}
		return _computeShaderDeclaration;
	}

	public override string ToString()
	{
		return _declaration;
	}
}
