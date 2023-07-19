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
	public float TimeToPeak { get; private set; } = 0.4f;

	[Export]
	public float TimeToFall { get; private set; } = 0.3f;

	[Export]
	public bool BlockInput { get; set; } = false;

	public JumpUpgradeComponent JumpUpgradeComponent { get; private set; } = null!;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private float _jumpVelocity;
	private float _jumpGravity;
	private float _variableJumpGravity;
	private float _fallGravity;

	private AnimationTree _animationTree;

	[MemberNotNull(nameof(JumpUpgradeComponent))]
	public override void _Ready()
	{
		JumpUpgradeComponent = GetNode<JumpUpgradeComponent>("JumpUpgradeComponent") ?? throw new Exception();
		_animationTree = GetNode<AnimationTree>("AnimationTree");

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
			JumpUpgradeComponent.ResetJumpCount();

		// Handle Jump.
		if (!BlockInput && Input.IsActionJustPressed(Constants.Action.game_jump) && (IsOnFloor() || JumpUpgradeComponent.TryJump()))
			velocity.Y = _jumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = BlockInput ? 0 : Input.GetAxis(Constants.Action.game_move_left, Constants.Action.game_move_right);
		if (direction != 0)
		{
			_animationTree.Set("parameters/conditions/idle", false);
			_animationTree.Set("parameters/conditions/walking", true);

			_animationTree.Set("parameters/walk/blend_position", direction);

			velocity.X = direction * Speed;
		}
		else
		{
			_animationTree.Set("parameters/conditions/idle", true);
			_animationTree.Set("parameters/conditions/walking", false);

			_animationTree.Set("parameters/walk/blend_position", 0);

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
