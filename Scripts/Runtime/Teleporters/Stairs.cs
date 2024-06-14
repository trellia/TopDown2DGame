using Godot;

namespace TopDown2DGame.Scripts.Runtime.Teleporters;

public partial class Stairs : Area2D
{
    [Export] public NodePath TargetDestinationNodePath;

    private Vector2 _targetPosition;

    [Signal]
    public delegate void PlayerEnteredStairsEventHandler(Vector2 destination);

    public override void _Ready()
    {
        AddToGroup("Stairs");
        BodyEntered += OnBodyEntered;

        var targetPositionNode = GetNode<Marker2D>(TargetDestinationNodePath);
        GD.Print(targetPositionNode.Position);
        _targetPosition = targetPositionNode.Position;
    }

    private void OnBodyEntered(Node body)
    {
        GD.Print("Stairs entered.");
        if (body is not Players.Player) return;
        GD.Print("The Player entered the stairs.");
        GD.Print($"The _targetPosition is {_targetPosition}");
        EmitSignal(SignalName.PlayerEnteredStairs, _targetPosition);
    }
}
