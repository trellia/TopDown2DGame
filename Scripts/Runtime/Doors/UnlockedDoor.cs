using Godot;
using TopDown2DGame.Scripts.Runtime.Players;

namespace TopDown2DGame.Scripts.Runtime.Doors;

public partial class UnlockedDoor : Area2D
{
    [Signal]
    public delegate void PlayerEnteredDoorEventHandler(Vector2 destination);

    [Signal]
    public delegate void PlayerExitedDoorEventHandler(Vector2 destination);

    public override void _Ready()
    {
        AddToGroup("Doors");
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node body)
    {
        if (body is not Player) return;
        GD.Print($"Door '{Name}' opened by {body.Name}.");
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play("opening");
        EmitSignal(SignalName.PlayerEnteredDoor);
    }

    private void OnBodyExited(Node body)
    {
        if (body is not Player) return;
        GD.Print($"Door '{Name}' closed by {body.Name}.");
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play("closing");
        EmitSignal(SignalName.PlayerExitedDoor);
    }
}
