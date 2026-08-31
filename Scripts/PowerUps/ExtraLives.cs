using Godot;
using System;

public partial class ExtraLives : PowerUp
{
	private int count = 0;
	
	public override void Effect(Player player)
	{
		if (count < 2)
			player.hasExtraLives = true;
		else	
			player.hasExtraLives = false;
	}

}
