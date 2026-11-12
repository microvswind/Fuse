using Stride.Graphics;
using VL.Core;

namespace Fuse;

internal sealed class DelegatingTextureInput : ShaderNode<Texture>
{
	private readonly NodeContext _nodeContext;

	private readonly TextureTypeTracker _typeTracker;

	private TextureInput _textureInput;

	public DelegatingTextureInput(NodeContext nodeContext)
		: base(nodeContext, "DelegatingTextureInput", (ShaderNode<Texture>)null, false)
	{
		_nodeContext = nodeContext;
		_typeTracker = new TextureTypeTracker(theUseRw: false);
	}

	protected override ShaderNode<Texture> SetValue(Texture value)
	{
		if (_typeTracker.CheckDeclaration(value) || _textureInput == null)
		{
			_textureInput = new TextureInput(_nodeContext, _typeTracker);
		}
		_textureInput.Value = value;
		return _textureInput;
	}
}
