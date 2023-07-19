
using Godot;
using System.Numerics;

#if !NET8_0_OR_GREATER
using System.Runtime.Intrinsics.X86;
#endif

public partial class JumpUpgradeComponent : Node
{
	[Signal]
	public delegate void OnAirJumpEventHandler();

	public int RemainingJumpCount { get; private set; }

	private PlayerData _playerData = null!;

	private int _maxAirJumpCount = default;

	public override void _Ready()
	{
		_playerData = ServiceProvider.Instance.GetService<PlayerData>();

		_playerData.Changed += UpdateMaxJumpCount;
		UpdateMaxJumpCount();
	}

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

	public void UpdateMaxJumpCount()
	{
#if !NET8_0_OR_GREATER
		if (Popcnt.IsSupported)
			_maxAirJumpCount = (int)Popcnt.PopCount((uint)_playerData.CollectedJumpUpgrades);
		else
#endif
			_maxAirJumpCount = BitOperations.PopCount((uint)_playerData.CollectedJumpUpgrades);
	}
}
