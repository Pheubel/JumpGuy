using Godot;
using System;

public partial class JumpUpgradePickupComponent : Node, IUpgradePickupComponent<JumpUpgradeComponent.JumpUpgradeFlag>
{
	[Export(PropertyHint.Enum)]
	public JumpUpgradeComponent.JumpUpgradeFlag Upgrade { get; private set; }

	public override void _Ready()
	{
		GetNode<Area2D>("../PickupBounds").BodyEntered += HandleBodyEntered;
	}

	private void HandleBodyEntered(Node2D body)
	{
		if (body is CharacterController character)
		{
			character.JumpUpgradeComponent.AddUpgrade(Upgrade);
		}
	}
}
