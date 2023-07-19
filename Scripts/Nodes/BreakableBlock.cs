using Godot;
using System;

[GlobalClass]
public partial class BreakableBlock : StaticBody2D
{
	[Export(PropertyHint.ResourceType, "BreakableBlockResource")]
	private BreakableBlockResource? Resource { get; set; }
}
