using Godot;

namespace TopDown2DGame.Scripts.Runtime.Enemies;

public partial class Enemy : Area2D
{
    [Export] public float KnockbackForce { get; set; } = 300f;
    [Export] public float Speed { get; set; } = 100f; // Adjust speed as needed

    public virtual int Damage { get; set; }
    private Vector2 _velocity = Vector2.Zero;

    [Signal]
    public delegate void PlayerAttackedByEnemyEventHandler(string name,
        int damage, Vector2 knockbackForce);

    public override void _Ready()
    {
        AddToGroup("Enemies");
        BodyEntered += OnBodyEntered;

        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play();
    }

    private void OnBodyEntered(Node body)
    {
        GD.Print($"{Name} OnBodyEntered triggered by {body.Name}");
        if (body is not Players.Player player)
        {
            GD.Print($"{Name} ignored {body.Name}");
            return;
        }

        GD.Print($"{Name} inflicted {Damage} damage to {body.Name}");

        Vector2 knockbackDirection =
            (player.GlobalPosition - GlobalPosition).Normalized();
        Vector2 knockbackForce = (knockbackDirection * KnockbackForce);

        EmitSignal(SignalName.PlayerAttackedByEnemy, Name, Damage,
            knockbackForce);
    }
}
