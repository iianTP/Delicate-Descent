using Godot;
using System;

public partial class DeathVignette : ColorRect
{
	private Tween deathTween;

	public override void _Ready()
	{
		SignalBus.Instance.Falling += StartVignette;
		SignalBus.Instance.SurvivedFall += StopVignette;
		SignalBus.Instance.Died += DeadVignette;
		
		Color = new Color(0.6f,0,0,0);
	}

	private void StartVignette()
	{
		deathTween = CreateTween();
		deathTween.TweenProperty(this, "color:a", 1f, 5).Connect("finished", Callable.From(
			() => { SignalBus.Instance.EmitSignal(SignalBus.SignalName.Died); }
		));
	}

	private void StopVignette()
	{
		Color = new Color(0.6f,0,0,0);
		if (deathTween != null)
			deathTween.Kill();
	}

	private void DeadVignette()
	{
		if (deathTween != null)
			deathTween.Kill();
		Color = new Color(0,0,0,1);
	}

}
