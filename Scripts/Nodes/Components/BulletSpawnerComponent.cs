using Godot;
using System;

[GlobalClass]
[Tool]
public partial class BulletSpawnerComponent : Node2D
{
	[Export]
	public PackedScene Bullet { get; private set; }

	[Export]
	public Vector2 SpawnOffset { get; set; }

	public void Shoot()
	{
		var bullet = Bullet.Instantiate<Node2D>();

		GetParent().AddSibling(bullet);

		bullet.GlobalPosition = GlobalPosition + SpawnOffset;
	}
}
