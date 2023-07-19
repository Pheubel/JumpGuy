using Godot;
using System;

public partial class SceneChanger : Node
{
	[Export(PropertyHint.ResourceType)]
	PackedScene _newScene;

	public void SwapScene()
	{
		var sceneHost = ServiceProvider.Instance.GetService<SceneHost>();

		sceneHost.SwapSceneDeferred(_newScene);
	}
}
