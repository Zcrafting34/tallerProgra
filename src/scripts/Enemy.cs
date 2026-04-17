using Godot;
using Microsoft.VisualBasic.FileIO;
using System;

public partial class Enemy : CharacterBody2D
{
    public const float Speed = 50.0f;
    public const float JumpVelocity = -400.0f;

    private bool can_attack = true;

    private Area2D vision;
    private Timer cd;


    private Timer c_direct;

    private Player player = null;

    private int direction = 1;


    public override void _Ready()

    {

        vision = GetNode<Area2D>("vision");
        cd = GetNode<Timer>("Timer");
        c_direct = GetNode<Timer>("timerCaminar");

        c_direct.Timeout += ChangeDirection;
        vision.BodyEntered += JugadorEntro;
        cd.Timeout += CanAttack;

        vision.BodyExited += JugadorSalio;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        if (player != null)
        {
            if (Position.DistanceTo(player.Position) < 30)
            {
                if (can_attack)
                {
                    player.TakeDamage(1);
                    can_attack = false;
                    cd.Start();
                }
            }
            else
            {
                Vector2 dir = player.Position - Position;
                dir = dir.Normalized();
                velocity.X = dir.X * Speed;
            }
        }
        else
        {
            velocity.X = direction * Speed;
        }

        Velocity = velocity;

        MoveAndSlide();
    }


    private void JugadorEntro(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            if (body is Player py)
            {
                player = py;
            }
        }
    }

    private void JugadorSalio(Node2D body)
    {
        if (body.IsInGroup("player"))
            player = null;
    }

    private void CanAttack()
    {
        can_attack = true;
    }

    private void ChangeDirection()
    {
        direction = -1 * direction;
    }
}
