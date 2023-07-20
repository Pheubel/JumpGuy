using System;
using Godot;
using JumpGuy.Utils;

public partial class HealthSystem : Node
{
	[Export(PropertyHint.Range, "0,1000000,1,hide_slider")]
	public int HeartWidth { get; private set; }

	[Export]
	private TextureRect _healthImage;

	[Export]
	private TextureRect _containerImage;

	public override void _Ready()
	{
		var playerData = this.GetGlobalNode<ServiceProvider>().GetService<PlayerData>();

		playerData.OnMaxHealthChanged += HandleMaxHealthChanged;
		playerData.OnCurrentHealthChanged += HandleCurrentHealthChanged;

		HandleCurrentHealthChanged(playerData.MaxHealth);
		HandleMaxHealthChanged(playerData.CurrentHealth);
	}

	public override void _ExitTree()
	{
		var playerData = this.GetGlobalNode<ServiceProvider>().GetService<PlayerData>();

		playerData.OnMaxHealthChanged -= HandleMaxHealthChanged;
		playerData.OnCurrentHealthChanged -= HandleCurrentHealthChanged;
	}

	private void HandleMaxHealthChanged(int newMaxHealth)
	{
		Vector2 size = _containerImage.Size;
		size.X = HeartWidth * newMaxHealth;
		_containerImage.SetSize(size);
	}

	private void HandleCurrentHealthChanged(int currentHealth)
	{
		Vector2 size = _healthImage.Size;
		size.X = HeartWidth * currentHealth;
		_healthImage.SetSize(size);
	}
}
