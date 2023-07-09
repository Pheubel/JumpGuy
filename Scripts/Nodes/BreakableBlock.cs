using Godot;
using System;

[GlobalClass]
[Tool]
public partial class BreakableBlock : StaticBody2D
{
	[Export(PropertyHint.ResourceType, "BreakableBlockResource")]
	private BreakableBlockResource? Resource { get; set; }

#if TOOLS
	private Sprite2D _blockSprite = null!;

	public override void _Ready()
	{
		_blockSprite = GetNode<Sprite2D>("Block");
	}

	public override void _Process(double delta)
	{
		if (Resource != null)
		{
			_blockSprite.Texture = Resource.Texture;
		}
		else
		{
			_blockSprite.Texture = null;
		}
	}
#endif
}
