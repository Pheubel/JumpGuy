using Godot;
using JumpGuy.Utils;

public partial class JumpUpgradeComponent : Node
{
	[Signal]
	public delegate void OnAirJumpEventHandler();

	public int RemainingJumpCount { get; private set; }

	private int _maxAirJumpCount = default;

	public override void _Ready()
	{
		var playerData = this.GetGlobalNode<ServiceProvider>().GetService<PlayerData>();

		playerData.OnJumpUpgradeChanged += UpdateMaxJumpCount;
		UpdateMaxJumpCount(playerData.CollectedJumpUpgrades);
	}

	public override void _ExitTree()
	{
		var playerData = this.GetGlobalNode<ServiceProvider>().GetService<PlayerData>();

		playerData.OnJumpUpgradeChanged -= UpdateMaxJumpCount;
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

	public void UpdateMaxJumpCount(JumpUpgradeFlag activeJumpUpgrades)
	{
		_maxAirJumpCount = BitOps.PopCount((int)activeJumpUpgrades);
	}
}
