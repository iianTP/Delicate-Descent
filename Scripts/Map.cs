using Godot;
using System;

public partial class Map : Node2D
{
	[Export] private Timer resetTimer;

	public override void _Ready()
	{
		resetTimer.Timeout += ResetGame;
		SignalBus.Instance.Died += () => { resetTimer.Start(); } ;
		SignalBus.Instance.Finished += FinishedGame;

		AudioManager.Instance.StartMusic();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("reset"))
			ResetGame();
	}


	private void ResetGame()
	{
		GetTree().ReloadCurrentScene();
	}

	private void FinishedGame()
	{
		GetTree().Quit(); 
	}

}
