using Godot;
using System;
using System.Collections.Generic;

public partial class Player : Node2D
{
	
	[Export] private Camera2D cam;
	[Export] public PlayerMotion Body { get; private set; }
	[Export] private Area2D groundDetector;
	
	[Export] private Timer deathTimer;
	
	private List<PowerUp> powerUps = [];


	private Tween fallingTween;
	private bool dead = false;
	public bool hasExtraLives = false;

	public override void _Ready()
	{
		SignalBus.Instance.Died += AudioManager.Instance.StopMusic;
		SignalBus.Instance.Died += AudioManager.Instance.DeathSfx; 
		SignalBus.Instance.UpdatedMaxSpeedState += () => { CallDeferred(MethodName.Falling); };
		deathTimer.Timeout += Die;

		if (CheckpointManager.Instance.CheckpointCoords != Vector2.Zero)
			GlobalPosition = CheckpointManager.Instance.CheckpointCoords;
	}


	public override void _Process(double delta)
	{
		cam.GlobalPosition = Body.GlobalPosition + GetCamOffset();

		groundDetector.GlobalPosition = Body.GlobalPosition + new Vector2(0, 16);

		if (Body.IsJumping) dead = false;

		foreach (PowerUp p in powerUps)
			p.Effect(this);
	}


	private Vector2 GetCamOffset()
	{
		int offset = 0;
		if (Input.IsActionPressed("camUp"))
			offset = 200;
		else if (Input.IsActionPressed("camDown"))
			offset = -200;
		
		return Vector2.Up * offset;
	}

	
	public void AddPowerUp(PowerUp powerUp)
	{
		powerUps.Add(powerUp);
	}

	private void Falling()
	{
		if (Body.IsAtSpeedDeadline())
		{
			deathTimer.Start();
			SignalBus.Instance.EmitSignal(SignalBus.SignalName.Falling);
		}
		else
		{
			
			if (dead)
			{
				SignalBus.Instance.EmitSignal(SignalBus.SignalName.Died);
			}
			else
			{
				deathTimer.Stop();
				SignalBus.Instance.EmitSignal(SignalBus.SignalName.SurvivedFall);
			}
		}
	}

	private void Die()
	{
		deathTimer.Stop();
		if (!hasExtraLives) dead = true;
	}




}
