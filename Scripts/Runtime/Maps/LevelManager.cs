using Godot;
using TopDown2DGame.Scripts.Runtime.Players;

namespace TopDown2DGame.Scripts.Runtime.Maps;

public partial class LevelManager : Node
{
    public override void _Ready()
    {
        NewGame();
        var player = GetNode<Player>("Player");
        player.PlayerDefeated += OnPlayerDefeated;
    }

    public override void _Process(double delta)
    {
    }

    private void NewGame()
    {
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);
    }

    private void OnPauseButtonPressed()
    {
    }

    private void OnPlayerDefeated()
    {
        GD.Print("Restarting level...");
        NewGame();
    }
}
