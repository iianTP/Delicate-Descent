using Godot;
using System;

public partial class PowerUpOrb : Area2D
{
	private enum PowerUpEnum { DOUBLE_JUMP, DASH, EXTRA_LIVES }
	[Export] private PowerUpEnum powerUp;

	private PowerUp GetPowerUp(PlayerMotion body)
	{
		PowerUp pu = null;
		switch (powerUp)
		{
			case PowerUpEnum.DOUBLE_JUMP:
				pu = new DoubleJump();
				break;
			case PowerUpEnum.DASH:
				break;
			case PowerUpEnum.EXTRA_LIVES:
				pu = new ExtraLives();
				break;
		}
		return pu;
	}

	public void _on_body_entered(Node2D body)
	{
		if (body is PlayerMotion playerBody)
		{
			Player player = playerBody.GetParent<Player>();
			playerBody = player.Body;
			player.AddPowerUp(GetPowerUp(playerBody));
			Hide();
		}
	}
}
