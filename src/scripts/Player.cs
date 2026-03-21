using Godot;


public partial class Player : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    public AnimatedSprite2D animaciones;

    enum State {Idle, Walk, Jump, Fall}

    private State currentState = State.Idle;
    public override void _Ready()
    {
        animaciones = GetNode<AnimatedSprite2D>("animaciones");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        if (!IsOnFloor())
            velocity += GetGravity() * (float)delta;
        
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            velocity.Y = JumpVelocity;
 

        float direction = Input.GetAxis("izq", "der");
        if (direction != 0)
            velocity.X = direction * Speed;
        else
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        
        Velocity = velocity;
        MoveAndSlide();

        UpdateState(direction);
        PlayAnimation();
    }

    private void UpdateState(float dir)
    {
        if (!IsOnFloor())
        {
            currentState = (Velocity.Y < 0) ? State.Jump : State.Fall;
            if (dir != 0 && currentState == State.Jump)
            {
                animaciones.FlipH = dir > 0;
            }
        }
        else
        {
            currentState = (dir != 0) ? State.Walk : State.Idle;
            if (dir != 0)
            {
                animaciones.FlipH = dir < 0;
            }
        }
    }
    private void PlayAnimation()
    {
        string name = currentState.ToString().ToLower();

        animaciones.Play(name);

    }
}



