using Godot;

namespace TopDown2DGame.Scripts.Runtime.Scenery;

public partial class WaterWheel : Area2D
{
    private bool _isSpinning = true;

    [Export]
    public bool IsSpinning
    {
        get => _isSpinning;
        set
        {
            _isSpinning = value;
            if (_isSpinning)
            {
                GD.Print($"WaterWheel '{Name}' is now spinning.");
                EmitSignal(SignalName.StartedSpinning);
            }
            else
            {
                GD.Print($"WaterWheel '{Name}' stopped spinning.");
                EmitSignal(SignalName.StoppedSpinning);
            }
        }
    }

    [Signal]
    public delegate void StartedSpinningEventHandler();

    [Signal]
    public delegate void StoppedSpinningEventHandler();

    public override void _Ready()
    {
        AddToGroup("WaterWheels");
        GD.Print($"Setting up WaterWheel {Name}");
        ControlWheel();
    }

    private void ControlWheel()
    {
        var nodes = GetChildren();
        foreach (var node in nodes)
        {
            if (node is not AnimatedSprite2D animatedSprite2D) continue;
            if (_isSpinning)
            {
                animatedSprite2D.Play("spin");
            }
            else
            {
                animatedSprite2D.Stop();
            }
        }
    }
}
