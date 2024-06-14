using Godot;

namespace TopDown2DGame.Scripts.Runtime.Teleporters;

public partial class GlobalTeleporter : Area2D
{
    [Export] public string TargetDestination { get; set; }

    private string _targetDestination;

    [Signal]
    public delegate void PlayerEnteredGlobalTeleporterEventHandler(string
        destination);

    public override void _Ready()
    {
        GD.Print($"Setting up GlobalTeleporter: {Name}");
        AddToGroup("GlobalTeleporters");
        _targetDestination = TargetDestination;
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        GD.Print("Global Teleporter entered.");
        if (body is not Players.Player) return;
        GD.Print("The Player entered the Global Teleporter.");
        GD.Print($"The _targetDestination is {_targetDestination}");
        EmitSignal(SignalName.PlayerEnteredGlobalTeleporter,
            _targetDestination);
    }
}
