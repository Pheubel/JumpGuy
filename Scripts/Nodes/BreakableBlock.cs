using Godot;
using System;

[GlobalClass]
[Tool]
public partial class BreakableBlock : Node2D
{
	private BreakableBlockResource? _resource;

	[Export(PropertyHint.ResourceType, "BreakableBlockResource")]
	public BreakableBlockResource? Resource
	{
		get => _resource; private set
		{
#if TOOLS
			if (_resource is not null)
				_resource.Changed -= HandleResourceChanged;
#endif
			_resource = value;
#if TOOLS
			if (_resource is not null)
				_resource.Changed += HandleResourceChanged;
			else
				ResetResourceDependentData();
#endif
		}
	}

#if TOOLS
	private Sprite2D _blockSprite = null!;

	public override void _Ready()
	{
		_blockSprite = GetNode<Sprite2D>("Block");
	}

	public override void _ExitTree()
	{
		if (Resource is null)
			return;
		Resource.Changed -= HandleResourceChanged;
	}

	private void HandleResourceChanged()
	{
		_blockSprite.Texture = Resource!.Texture;
	}

	private void ResetResourceDependentData()
	{
		_blockSprite.Texture = null;
	}
#endif
}
