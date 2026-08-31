using Godot;
using System;

public partial class DoubleJump : PowerUp
{
	private bool doubleJumped = false;

	public override void Effect(Player player)
	{
		PlayerMotion playerBody = player.Body;

		if (playerBody.IsGrounded)
			doubleJumped = false;
		
		if (!doubleJumped && !playerBody.IsGrounded && Input.IsActionJustPressed("jump"))
		{
			playerBody.Jump();
			doubleJumped = true;
		}
	}

}
