using Godot;

[GlobalClass]
public partial class ReactToPlayerTouchedComponent : Node
{
	[Signal]
	public delegate void OnPlayerTouchedEventHandler(CharacterController playerCharacter);

	private void HandleBodyEntered(Node2D body)
	{
		if (body is CharacterController character)
		{
			EmitSignal(SignalName.OnPlayerTouched, character);
		}
	}
}
