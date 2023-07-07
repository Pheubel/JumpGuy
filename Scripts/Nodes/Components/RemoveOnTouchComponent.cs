using Godot;
using System;

[GlobalClass]
public partial class RemoveOnTouchComponent : Node
{
	public override void _Ready()
	{
		GetNode<Area2D>("../PickupBounds").BodyEntered += HandleBodyEntered;
	}

	private void HandleBodyEntered(Node2D body)
	{
		if (body is CharacterController character)
		{
			GetParent().QueueFree();
		}
	}
}
