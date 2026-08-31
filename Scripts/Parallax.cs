using Godot;
using System;

public partial class Parallax : Node2D
{
	[Export] private Camera2D cam;

	[Export] private Sprite2D bg1;
	[Export] private Sprite2D bg2;
	[Export] private Sprite2D bg3;

	public override void _Process(double delta)
	{
		CallDeferred(MethodName.Move);
	}

	private void Move()
	{
		bg1.GlobalPosition = new Vector2(bg1.GlobalPosition.X, cam.GlobalPosition.Y);
		bg2.GlobalPosition = new Vector2(bg2.GlobalPosition.X, cam.GlobalPosition.Y);
		bg3.GlobalPosition = new Vector2(bg3.GlobalPosition.X, cam.GlobalPosition.Y);
	}

	public void _on_checkpoint_1_body_entered(Node2D body)
	{
		if (body is PlayerMotion)
		{
			Tween t = CreateTween();
			t.TweenProperty(bg1, "modulate:a", 0, 3);
		}
	}

	public void _on_checkpoint_2_body_entered(Node2D body)
	{
		if (body is PlayerMotion)
		{
			Tween t = CreateTween();
			t.TweenProperty(bg1, "modulate:a", 0, 3);
			t.TweenProperty(bg2, "modulate:a", 0, 3);
		}
	}

}
