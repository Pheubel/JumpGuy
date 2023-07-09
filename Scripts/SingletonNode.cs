using Godot;
using System;

public abstract partial class SingletonNode<T> : Node where T : SingletonNode<T>
{
	public static T Instance { get; private set; } = null!;

	public override void _Ready()
	{
		if (Instance is null)
			Instance = (T)this;
		else
			throw new Exception($"{nameof(T)} is already instantiated, for correct usage, be sure to add it to the auto-load and let it handle the rest.");
	}

	public override void _ExitTree()
	{
		if (Instance is not null)
			Instance = null!;
	}
}
