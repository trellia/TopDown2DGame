using Godot;

namespace TopDown2DGame.Scripts.Runtime.Teleporters;

public partial class LocalTeleporter : Area2D
{
    [Export] public NodePath TargetDestinationNodePath;

    private Vector2 _targetPosition;

    [Signal]
    public delegate void PlayerEnteredLocalTeleporterEventHandler(
        Vector2 destination);

    public override void _Ready()
    {
        AddToGroup("LocalTeleporters");
        BodyEntered += OnBodyEntered;

        var targetPositionNode = GetNode<Marker2D>(TargetDestinationNodePath);
        _targetPosition = targetPositionNode.Position;
    }

    private void OnBodyEntered(Node body)
    {
        GD.Print("Local Teleporter entered.");
        if (body is not Players.Player) return;
        GD.Print("The Player entered the Local Teleporter.");
        GD.Print($"The _targetPosition is {_targetPosition}");
        EmitSignal(SignalName.PlayerEnteredLocalTeleporter, _targetPosition);
    }
}
