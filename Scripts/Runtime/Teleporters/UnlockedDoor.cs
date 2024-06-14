using Godot;
using TopDown2DGame.Scripts.Runtime.Players;

namespace TopDown2DGame.Scripts.Runtime.Teleporters;

public partial class UnlockedDoor : Area2D
{
    [Export] public NodePath TargetDestinationNodePath;

    private Vector2 _targetPosition;

    [Signal]
    public delegate void PlayerEnteredDoorEventHandler(Vector2 destination);

    [Signal]
    public delegate void PlayerExitedDoorEventHandler(Vector2 destination);

    public override void _Ready()
    {
        AddToGroup("Doors");
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;

        var targetPositionNode = GetNode<Marker2D>(TargetDestinationNodePath);
        _targetPosition = targetPositionNode.Position;
    }

    private void OnBodyEntered(Node body)
    {
        GD.Print($"Door '{Name}' opened.");
        if (body is not Player) return;
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play("opening");
        EmitSignal(SignalName.PlayerEnteredDoor, _targetPosition);
    }

    private void OnBodyExited(Node body)
    {
        GD.Print($"Door '{Name}' closed.");
        if (body is not Player) return;
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play("closing");
        EmitSignal(SignalName.PlayerExitedDoor, _targetPosition);
    }
}
