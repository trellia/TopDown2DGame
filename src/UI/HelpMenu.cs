using Godot;

public partial class HelpMenu : Control
{
    public override void _Ready()
    {
        GD.Print("HelpMenu.cs: Successfully initialized.");
    }

    public override void _Process(double delta)
    {
    }

    private void _OnReturnButtonPressed()
    {
        GD.Print("Back button pressed.");
        GetTree().ChangeSceneToFile("res://src/UI/MainMenu.tscn");
    }
}
