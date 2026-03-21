using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    public AnimatedSprite2D animaciones;

    public override void _Ready()
    {
        animaciones = GetNode<AnimatedSprite2D>("animaciones");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
            animaciones.Play("fall");
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
            animaciones.Play("jump");
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        float direction = Input.GetAxis("izq", "der");
        if (direction != 0)
        {
            if (direction < 0)
            {
                animaciones.FlipH = true;
            }
            else if (direction > 0)
            {
                animaciones.FlipH = false;
            }
            animaciones.Play("run");
            velocity.X = direction * Speed;
        }
        else
        {
            animaciones.Play("idle");
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
