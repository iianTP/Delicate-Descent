using Godot;
using System;

public partial class AudioManager : Node2D
{
	public static AudioManager Instance { get; private set; }
	[Export] private AudioStreamPlayer2D died;
	[Export] private AudioStreamPlayer2D[] tracks;

	private int trackIndex = 0;

	public override void _Ready()
	{
		Instance = this;
		trackIndex = GD.RandRange(0,2);
		foreach (AudioStreamPlayer2D track in tracks)
			track.Finished += NextTrack;
	}


	public void DeathSfx()
	{
		died.Play();
	}

	public void StartMusic()
	{
		tracks[trackIndex].Play();
	}

	public void StopMusic()
	{
		tracks[trackIndex].Stop();
	}

	private void NextTrack()
	{
		trackIndex = (trackIndex + 1) % tracks.Length;
		tracks[trackIndex].Play();
	}


}
