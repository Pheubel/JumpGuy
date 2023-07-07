
using Godot;
using System.Numerics;

#if !NET8_0_OR_GREATER
using System.Runtime.Intrinsics.X86;
#endif

public partial class JumpUpgradeComponent : Node
{
	public int RemainingJumpCount { get; private set; }
	[Export]
	public JumpUpgradeFlag CollectedJumpUpgrades { get; private set; }

	public void ResetJumpCount()
	{
#if !NET8_0_OR_GREATER
		if (Popcnt.IsSupported)
			RemainingJumpCount = (int)Popcnt.PopCount((uint)CollectedJumpUpgrades);
		else
#endif
			RemainingJumpCount = BitOperations.PopCount((uint)CollectedJumpUpgrades);
	}

	public bool TryJump()
	{
		if (RemainingJumpCount == 0)
			return false;

		RemainingJumpCount--;
		return true;
	}

	public void AddUpgrade(JumpUpgradeFlag upgrade)
	{
		CollectedJumpUpgrades |= upgrade;
	}

	public enum JumpUpgradeFlag
	{
		None = 0,
		CannonLevelOne = 1
	}
}
