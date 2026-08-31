using Godot;
using System;

public partial class PlayerMotion : RigidBody2D
{
	[Export] private int rollForce;
	[Export] private int jumpForce;
	[Export] private float maxFallSpeed = 500;
	[Export] private float speedDeadline = 600;

	public bool IsGrounded { get; private set; } = false;
	public bool IsJumping = false;
	private bool atSpeedDeadline = false;

	private Vector2 linearVelBuffer;
	
	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		
		float move = Input.GetAxis("left","right");

		ApplyCentralForce( new Vector2(move * rollForce * state.Step, 0) );

		if (IsGrounded && Input.IsActionJustPressed("jump") ) Jump();

		if (state.LinearVelocity.Y > maxFallSpeed)
			state.LinearVelocity = new Vector2(state.LinearVelocity.X, maxFallSpeed);
		
		CallDeferred(MethodName.CheckSpeedState, state);
		

		// GD.Print(LinearVelocity);

		linearVelBuffer = LinearVelocity;

	}

	public void Jump()
	{
		LinearVelocity = new Vector2(LinearVelocity.X, -jumpForce);
		IsJumping = true;
	}

	private void CheckSpeedState(PhysicsDirectBodyState2D state)
	{
		if (state.LinearVelocity.Y > speedDeadline)
			UpdateMaxSpeedState(true);
		else 
			UpdateMaxSpeedState(false);
	}

	private void UpdateMaxSpeedState(bool state)
	{
		if (atSpeedDeadline != state)
		{
			atSpeedDeadline = state;
			if (linearVelBuffer.Y > 1500)
				SignalBus.Instance.EmitSignal(SignalBus.SignalName.Died);
			else
				SignalBus.Instance.EmitSignal(SignalBus.SignalName.UpdatedMaxSpeedState);

			if (state) IsJumping = false;
		}
			
	}

	public bool IsAtSpeedDeadline()
	{
		return atSpeedDeadline;
	}

	public void _on_ground_detector_body_entered(Node2D body)
	{
		if (body.IsInGroup("Ground"))
		{
			IsGrounded = true;
			IsJumping = false;
		}
			
	}

	public void _on_ground_detector_body_exited(Node2D body)
	{
		if (body.IsInGroup("Ground"))
			IsGrounded = false;
	}
}
