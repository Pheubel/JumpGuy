using Godot;
using System;
using JumpGuy.Utils;

public partial class HealthUpgradePickupComponent : Node, IUpgradePickupComponent<HealthUpgradeFlag>
{
	[Export(PropertyHint.Enum)]
	public HealthUpgradeFlag Upgrade { get; private set; }

	public void HandlePlayerTouched(CharacterController _)
	{
		var data = this.GetGlobalNode<ServiceProvider>().GetService<PlayerData>();

		data.CollectedHealthUpgrades |= Upgrade;
	}
}
