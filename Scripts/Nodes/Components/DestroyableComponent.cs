using Godot;
using System;

[GlobalClass]
public partial class DestroyableComponent : Node
{
	[Signal]
	public delegate void OnDestroyedEventHandler();

	[Export(PropertyHint.NodePathToEditedNode)]
	private NodePath _target = new NodePath("../");

	public void Destroy()
	{
		GetNode(_target).QueueFree();
		EmitSignal(SignalName.OnDestroyed);
	}
}
