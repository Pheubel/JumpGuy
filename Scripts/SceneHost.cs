using Godot;
using System;
using JumpGuy.Utils;

public partial class SceneHost : Node
{
	[Export(PropertyHint.ResourceType)]
	PackedScene _defaultScene;

	Node _currentScene;

	public override void _Ready()
	{
		this.GetGlobalNode<ServiceProvider>().AddService(this);

		_currentScene = _defaultScene.Instantiate();
		AddChild(_currentScene);
	}

	public override void _ExitTree()
	{
		this.GetGlobalNode<ServiceProvider>().TryRemoveService<SceneHost>();
	}

	public void SwapScene(PackedScene newScene)
	{
		RemoveChild(_currentScene);
		_currentScene.QueueFree();

		_currentScene = newScene.Instantiate();
		AddChild(_currentScene);
	}

	public void SwapSceneDeferred(PackedScene newScene)
	{
		CallDeferred(MethodName.SwapScene, newScene);
	}
}
