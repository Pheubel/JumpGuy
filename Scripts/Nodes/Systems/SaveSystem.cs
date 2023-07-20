using System;
using System.IO;
using Godot;
using JumpGuy.Utils;

namespace JumpGuy.Scripts.Nodes.Systems
{
	public partial class SaveSystem : Node
	{
		private static readonly string saveFilePath = ProjectSettings.GlobalizePath("user://save_data.res");

		PlayerData _loadedData = LoadSave<PlayerData>();

		public void SavePlayerdata()
		{
			_loadedData = LoadSave<PlayerData>();
			ResourceSaver.Save(_loadedData, saveFilePath);
		}

		public override void _Ready()
		{
			this.GetGlobalNode<ServiceProvider>().AddService(_loadedData);
		}

		private static T LoadSave<T>() where T : class, new()
		{

			if (ResourceLoader.Exists(saveFilePath))
			{
				return GD.Load<T>(saveFilePath);
			}
			else
			{
				return new();
			}
		}
	}
}
