using Godot;
using System;

public partial class PlayerData : Resource
{
	#region Signals
	[Signal]
	public delegate void OnJumpUpgradeChangedEventHandler(JumpUpgradeFlag newFlags);

	[Signal]
	public delegate void OnHealthUpgradeChangedEventHandler(HealthUpgradeFlag newFlags);
	#endregion

	#region BackingFields
	JumpUpgradeFlag _collectedJumpUpgrades;
	HealthUpgradeFlag _collectedHealthUpgrades;
	#endregion

	[Export]
	public JumpUpgradeFlag CollectedJumpUpgrades
	{
		get => _collectedJumpUpgrades;
		set
		{
			_collectedJumpUpgrades = value;
			EmitSignal(SignalName.OnJumpUpgradeChanged, (int)_collectedJumpUpgrades);
			EmitChanged();
		}
	}

	[Export]
	public HealthUpgradeFlag CollectedHealthUpgrades
	{
		get => _collectedHealthUpgrades;
		set
		{
			_collectedHealthUpgrades = value;
			EmitSignal(SignalName.OnHealthUpgradeChanged, (int)_collectedHealthUpgrades);
			EmitChanged();
		}
	}
}
