using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class CharacterController : CharacterBody2D
{
	[Export]
	public float Speed { get; private set; } = 150.0f;

	[Export]
	public float JumpHeight { get; private set; } = 40;

	[Export]
	public float VariableJumpHeight { get; private set; } = 40;

	[Export]
	public float TimeToPeak { get; private set; } = 1;

	[Export]
	public float TimeToFall { get; private set; } = 0.8f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private JumpUpgradeComponent? _jumpUpgradeComponent;

	private float _jumpVelocity;
	private float _jumpGravity;
	private float _variableJumpGravity;
	private float _fallGravity;

	[MemberNotNull(nameof(_jumpUpgradeComponent))]
	public override void _Ready()
	{
		_jumpUpgradeComponent = GetNode<JumpUpgradeComponent>("JumpUpgradeComponent") ?? throw new Exception();

		_jumpVelocity = ((2 * JumpHeight) / TimeToPeak) * -1;
		_jumpGravity = ((-2 * JumpHeight) / (TimeToPeak * TimeToPeak)) * -1;
		_fallGravity = ((-2 * JumpHeight) / (TimeToFall * TimeToFall)) * -1;
		_variableJumpGravity = (_jumpVelocity * _jumpVelocity) / (2 * VariableJumpHeight);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += GetGravity() * (float)delta;
		else
			_jumpUpgradeComponent!.ResetJumpCount();

		// Handle Jump.
		if (Input.IsActionJustPressed(Constants.Action.game_jump) && (IsOnFloor() || _jumpUpgradeComponent!.TryJump()))
			velocity.Y = _jumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Input.GetAxis(Constants.Action.game_move_left, Constants.Action.game_move_right);
		if (direction != 0)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	//public void Jump()
	//{

	//}

	private float GetGravity()
	{
		return Velocity.Y < 0
			? (Input.IsActionPressed(Constants.Action.game_jump)
				? _variableJumpGravity
				: _jumpGravity)
			: _fallGravity;
	}
}