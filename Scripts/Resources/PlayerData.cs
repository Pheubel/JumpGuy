using Godot;
using System;

public partial class PlayerData : Resource
{
	[Export]
	public JumpUpgradeFlag CollectedJumpupgrades { get; set; }

}
