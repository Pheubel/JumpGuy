using Godot;
using System;

[GlobalClass]
public partial class ConstantMovementComponent : Node
{
	[Export]
	public Vector2 Velocity { get; set; }

	private CollisionObject2D _node;

	public override void _Ready()
	{
		_node = GetParent<CollisionObject2D>();
	}

	public override void _PhysicsProcess(double delta)
	{
		_node.Translate(Velocity * (float)delta);
	}
}
