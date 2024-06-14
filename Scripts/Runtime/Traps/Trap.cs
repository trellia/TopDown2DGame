using Godot;

namespace TopDown2DGame.Scripts.Runtime.Traps;

public partial class Trap : Area2D
{
    public virtual int Damage { get; set; }
    [Export] public float KnockbackForce { get; set; } = 200f;

    [Signal]
    public delegate void PlayerEnteredTrapEventHandler(string name, int damage,
        Vector2 knockbackForce);

    public override void _Ready()
    {
        AddToGroup("Traps");
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

        EmitSignal(SignalName.PlayerEnteredTrap, Name, Damage, knockbackForce);
    }
}
