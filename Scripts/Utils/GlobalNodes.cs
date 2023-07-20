using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace JumpGuy.Utils
{
	internal static class GlobalNodes
	{
		public static T GetGlobalNode<T>(this Node node, string name) where T : Node
		{
			return node.GetNode<T>($"/root/{name}");
		}

		public static T GetGlobalNode<T>(this Node node) where T : Node
		{
			return node.GetNode<T>($"/root/{typeof(T).Name}");
		}

		public static Node GetGlobalNode(this Node node, string name)
		{
			return node.GetNode($"/root/{name}");
		}

	}
}
