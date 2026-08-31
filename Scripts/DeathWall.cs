using Godot;
using System;

public partial class DeathWall : Node2D
{
	[Export] private int speed = 10;

	public override void _Process(double delta)
	{
		GlobalPosition += Vector2.Down * speed * (float)delta;
	}

	public void _on_area_2d_body_entered(Node2D body)
	{
		if (body is PlayerMotion)
			SignalBus.Instance.EmitSignal(SignalBus.SignalName.Died);
	}
}
