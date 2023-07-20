using Godot;
using System;
using JumpGuy.Utils;

public partial class PlayerData : Resource
{
	const int StartingHealth = 3;

	#region Signals
	[Signal]
	public delegate void OnJumpUpgradeChangedEventHandler(JumpUpgradeFlag newFlags);

	[Signal]
	public delegate void OnMaxHealthChangedEventHandler(int newMaxHealth);

	[Signal]
	public delegate void OnHealthUpgradeChangedEventHandler(HealthUpgradeFlag newFlags);

	[Signal]
	public delegate void OnCurrentHealthChangedEventHandler(int currentHealth);

	[Signal]
	public delegate void OnCurrentHealthReachedZeroEventHandler(int currentHealth);
	#endregion

	#region BackingFields
	JumpUpgradeFlag _collectedJumpUpgrades;
	HealthUpgradeFlag _collectedHealthUpgrades;
	int _maxHealth = StartingHealth;
	int _currentHealth = StartingHealth;
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

	// MaxHealth is set programatically through changing CollectedHealthUpgrades, so no export attribute
	public int MaxHealth
	{
		get => _maxHealth;
		private set
		{
			_maxHealth = value;
			EmitSignal(SignalName.OnMaxHealthChanged, _maxHealth);
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

			// changing max health will call "EmitChanged()", so no need to do it here
			MaxHealth = BitOps.PopCount((int)value) + StartingHealth;
		}
	}

	[Export]
	public int CurrentHealth
	{
		get => _currentHealth;
		set
		{
			if (_currentHealth > 0 && value <= 0)
				EmitSignal(SignalName.OnCurrentHealthReachedZero);

			_currentHealth = Math.Clamp(value, 0, MaxHealth);
			EmitSignal(SignalName.OnCurrentHealthChanged, _currentHealth);
			EmitChanged();
		}
	}
}
