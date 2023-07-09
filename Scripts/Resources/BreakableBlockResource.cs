using System;
using Godot;


[GlobalClass]
[Tool]
public partial class BreakableBlockResource : Resource
{
	private Texture2D _texture;

	[Export]
	public Texture2D Texture
	{
		get => _texture; private set
		{
			_texture = value;
			EmitChanged();
		}
	}
}

