using Godot;
using System;

public partial class Goal : Area2D
{
	public void _on_body_entered(Node2D body)
	{
		if (body is PlayerMotion)
			SignalBus.Instance.EmitSignal(SignalBus.SignalName.Finished);
	}
}
