using Godot;
using System;

[GlobalClass]
public partial class DestroyableComponent : Node
{
	[Signal]
	public delegate void OnDestroyedEventHandler();

	public void Destroy()
	{
		this.GetParent().QueueFree();
		EmitSignal(SignalName.OnDestroyed);
	}
}
