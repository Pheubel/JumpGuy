using Constants;
using Godot;
using System;

public partial class PlayerJumpBullet : Area2D
{
	[Export]
	string ExcludeGroup { get; set; } = string.Empty;

	public override void _Ready()
	{
		this.BodyShapeEntered += (bodyRid, body, bodyShapeIndex, localShapeIndex) =>
		{
			if (body.IsInGroup(ExcludeGroup))
				return;

			if (body.TryGetChildOfType<PlayerBulletTargetComponent>(out var target))
			{
				//GD.Print("hit a node with a target component child.");
				target.Hit();
			}
			else
			{
				//GD.Print($"something else hit: {body.Name}");
			}

			QueueFree();
		};
	}
}
