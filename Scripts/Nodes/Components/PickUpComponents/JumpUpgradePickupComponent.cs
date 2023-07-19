using Godot;
using System;

public partial class JumpUpgradePickupComponent : Node, IUpgradePickupComponent<JumpUpgradeFlag>
{
	[Export(PropertyHint.Enum)]
	public JumpUpgradeFlag Upgrade { get; private set; }

	public void HandlePlayerTouched(CharacterController _)
	{
		var data = ServiceProvider.Instance.GetService<PlayerData>();

		data.CollectedJumpUpgrades |= Upgrade;
	}
}
