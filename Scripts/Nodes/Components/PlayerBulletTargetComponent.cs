using Godot;
using System;

[GlobalClass]
public partial class PlayerBulletTargetComponent : Node
{
	// TODO: transer bullet stats?

	[Signal]
	public delegate void OnBulletHitEventHandler();

	public void Hit()
	{
		EmitSignal(SignalName.OnBulletHit);
	}
}
