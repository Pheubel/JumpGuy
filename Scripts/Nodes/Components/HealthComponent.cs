using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node
{
	[Signal]
	public delegate void OnHealthDepletedEventHandler();

	[Export]
	public int Health { get; set; }

	public void TakeDamage(int damage)
	{
		Health -= damage;

		if (Health <= 0)
		{
			Health = 0;
			EmitSignal(SignalName.OnHealthDepleted);
		}
	}
}
