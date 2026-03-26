using Godot;


public partial class Player : CharacterBody2D
{

    [Signal]
    public delegate void SaludarEventHandler(string msg);
    public int Puntos { get; set; }

    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    public AnimatedSprite2D animaciones;

    private int health = 10;

    enum State { Idle, Walk, Jump, Fall }

    private State currentState = State.Idle;

    private Label textoPuntos;
    public override void _Ready()
    {
        Saludar += DiHola;
        EmitSignal(SignalName.Saludar, "Cristian");
        Puntos = 0;
        animaciones = GetNode<AnimatedSprite2D>("animaciones");
        textoPuntos = GetNode<Label>("CanvasLayer/Label");
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


            animaciones.FlipH = dir > 0;

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

    public void ActualizarPuntos(int punto)
    {
        Puntos += punto;
        textoPuntos.Text = "Puntos: " + Puntos;
    }

    private void DiHola(string m)
    {
        GD.Print("Hola: " + m);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    private void Die()
    {
        QueueFree();
    }

}
