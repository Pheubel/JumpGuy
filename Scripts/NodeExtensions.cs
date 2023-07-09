using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Godot;

public static class NodeExtensions
{
	/// <summary>
	/// Itterates over the direct children of the node to find all nodes that match the given type.
	/// </summary>
	/// <typeparam name="T"> The type to be queried.</typeparam>
	/// <param name="parent"> The parent node whose direct children get scanned.</param>
	/// <returns> Array of direct children that match the given type.</returns>
	public static IReadOnlyList<T> GetChildrenOfType<T>(this Node parent) where T : class
	{
		List<T> matches = new();

		foreach (var childNode in parent.GetChildren())
		{
			if (childNode is T matchedNode)
				matches.Add(matchedNode);
		}

		return matches;
	}

	/// <summary>
	/// Itterates over the direct children of the node to find the first node that match the given type.
	/// </summary>
	/// <typeparam name="T"> The type to be queried.</typeparam>
	/// <param name="parent"> The parent node whose direct children get scanned.</param>
	/// <returns> The first child that matches the given type or null if it is not found.</returns>
	public static T? GetChildOfTypeOrNull<T>(this Node parent) where T : class
	{
		foreach (var childNode in parent.GetChildren())
		{
			if (childNode is T matchedNode)
				return matchedNode;
		}

		return null;
	}

	/// <summary>
	/// Itterates over the direct children of the node to find the first node that match the given type.
	/// </summary>
	/// <typeparam name="T"> The type to be queried.</typeparam>
	/// <param name="parent"> The parent node whose direct children get scanned.</param>
	/// <param name="node"> The first child that matches the given type or null if it is not found.</param>
	/// <returns> true if a ndoe with the given type was found, false otherwise.</returns>
	public static bool TryGetChildOfType<T>(this Node parent, [NotNullWhen(true)] out T? node) where T : class
	{
		foreach (var childNode in parent.GetChildren())
		{
			if (childNode is T matchedNode)
			{
				node = matchedNode;
				return true;
			}
		}

		node = null;
		return false;
	}
}