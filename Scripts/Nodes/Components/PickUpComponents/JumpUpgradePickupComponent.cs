using Godot;
using JumpGuy.Utils;
using System;

public partial class JumpUpgradePickupComponent : Node, IUpgradePickupComponent<JumpUpgradeFlag>
{
	[Export(PropertyHint.Enum)]
	public JumpUpgradeFlag Upgrade { get; private set; }

	public void HandlePlayerTouched(CharacterController _)
	{
		var data = this.GetGlobalNode<ServiceProvider>().GetService<PlayerData>();

		data.CollectedJumpUpgrades |= Upgrade;
	}
}
