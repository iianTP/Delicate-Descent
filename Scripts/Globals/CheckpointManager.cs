using Godot;
using System;

public partial class CheckpointManager : Node
{
	public static CheckpointManager Instance { get; private set; }

	public Vector2 CheckpointCoords;

	public override void _Ready()
	{
		Instance = this;
	}

}
