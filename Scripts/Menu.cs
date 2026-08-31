using Godot;
using System;

public partial class Menu : Control
{
	[Export] private PackedScene map;
	[Export] private Button play;
	public void _on_button_pressed()
	{
		GetParent().AddChild(map.Instantiate());
		CallDeferred("free");
	}

	// public void _on_button_mouse_entered()
	// {
	// 	play.Modulate = new Color(1,1,0,1);
	// }

	// public void _on_button_mouse_exited()
	// {
	// 	play.Modulate = new Color(255,255,255,255);
	// }
}
