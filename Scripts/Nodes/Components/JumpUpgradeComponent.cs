
using Godot;
using System;
using System.Numerics;

#if !NET8_0_OR_GREATER
using System.Runtime.Intrinsics.X86;
#endif

public partial class JumpUpgradeComponent : Node
{
	[Signal]
	public delegate void OnAirJumpEventHandler();

	public int RemainingJumpCount { get; private set; }

	[Export]
	public JumpUpgradeFlag CollectedJumpUpgrades { get; private set; }

	private int _maxAirJumpCount = default;

	public void ResetJumpCount()
	{
		RemainingJumpCount = _maxAirJumpCount;
	}

	public bool TryJump()
	{
		if (RemainingJumpCount == 0)
			return false;

		RemainingJumpCount--;
		EmitSignal(SignalName.OnAirJump);
		return true;
	}

	public void AddUpgrade(JumpUpgradeFlag upgrade)
	{
		CollectedJumpUpgrades |= upgrade;

#if !NET8_0_OR_GREATER
		if (Popcnt.IsSupported)
			_maxAirJumpCount = (int)Popcnt.PopCount((uint)CollectedJumpUpgrades);
		else
#endif
			_maxAirJumpCount = BitOperations.PopCount((uint)CollectedJumpUpgrades);
	}

	public enum JumpUpgradeFlag
	{
		None = 0,
		CannonLevelOne = 1
	}
}
