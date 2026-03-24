using Godot;


public partial class Manzana : Area2D
{
	AnimatedSprite2D animacion;
    public override void _Ready()
    {
        BodyEntered += body_entered;
		animacion = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animacion.Play("default");
    }

	public void body_entered(Node2D b)
	{
		if (b.IsInGroup("player"))
		{
			if (b is Player player)
			{
				player.ActualizarPuntos(1);
				QueueFree();	
			}
		}
	}
}
