using Godot;
using System;
using JumpGuy.Utils;

public partial class SceneChanger : Node
{
	[Export(PropertyHint.ResourceType)]
	PackedScene _newScene;

	public void SwapScene()
	{
		var sceneHost = this.GetGlobalNode<ServiceProvider>().GetService<SceneHost>();

		sceneHost.SwapSceneDeferred(_newScene);
	}
}
