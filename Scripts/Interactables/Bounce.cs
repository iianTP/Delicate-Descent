using Godot;
using System;

public partial class Bounce : Area2D
{
	[Export] private int bounceForce = 800;
	public void _on_body_entered(Node2D body)
	{
		if (body is PlayerMotion playerBody)
		{
			playerBody.IsJumping = true;
			playerBody.LinearVelocity = new Vector2(playerBody.LinearVelocity.X, -bounceForce);
		}
	}
}
