using Godot;
using System;

public partial class SignalBus : Node
{
	public static SignalBus Instance { get; private set; }
	[Signal] public delegate void UpdatedMaxSpeedStateEventHandler();

	[Signal] public delegate void FallingEventHandler();

	[Signal] public delegate void SurvivedFallEventHandler();

	[Signal] public delegate void DiedEventHandler();

	[Signal] public delegate void FinishedEventHandler();

	public override void _Ready()
	{
		Instance = this;
	}

}
