using Godot;
using System;

public partial class Checkpoint : Area2D
{
	public void _on_body_entered(Node2D body)
	{
		if (body is PlayerMotion)
			CheckpointManager.Instance.CheckpointCoords = GlobalPosition;
	}
}
