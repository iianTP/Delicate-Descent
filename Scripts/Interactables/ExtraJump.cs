using Godot;
using System;

public partial class ExtraJump : Area2D
{
	[Export] private int force;
	[Export] private int angle;
	[Export] private Sprite2D sprite;
 
	private Vector2 direction;

	private bool playerInArea = false;
	private PlayerMotion playerBody;

	public override void _Ready()
	{
		direction = Vector2.FromAngle(Mathf.DegToRad(angle));
		sprite.Rotation = Mathf.DegToRad(angle);
	}


	public override void _Process(double delta)
	{
		if (playerInArea && Input.IsActionJustPressed("jump"))
		{
			playerBody.IsJumping = true;
			playerBody.LinearVelocity = direction * force + (Vector2.Right * playerBody.LinearVelocity);
		}
	}

	public void _on_body_entered(Node2D body)
	{
		if (body is PlayerMotion rb2d)
		{
			playerBody = rb2d;
			playerInArea = true;
		}
	}

	public void _on_body_exited(Node2D body)
	{
		if (body is PlayerMotion)
			playerInArea = false;
	}

}
