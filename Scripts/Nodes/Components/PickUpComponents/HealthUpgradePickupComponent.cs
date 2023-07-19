using Godot;
using System;

public partial class HealthUpgradePickupComponent : IUpgradePickupComponent<HealthUpgradeFlag>
{
	[Export(PropertyHint.Enum)]
	public HealthUpgradeFlag Upgrade { get; private set; }

	public void HandlePlayerTouched(CharacterController _)
	{
		var data = ServiceProvider.Instance.GetService<PlayerData>();

		data.CollectedHealthUpgrades |= Upgrade;
	}
}
