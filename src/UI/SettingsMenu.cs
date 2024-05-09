using Godot;

public partial class SettingsMenu : Control
{
    public override void _Ready()
    {
        GD.Print("SettingsMenu.cs: Successfully initialized.");
    }

    public override void _Process(double delta)
    {
    }

    private void _OnNonfunctionalButtonPressed()
    {
        GD.Print("Nonfunctional button pressed.");
    }

    private void _OnReturnToMainMenuButtonPressed()
    {
        GD.Print("Return to main menu button pressed.");
        GetTree().ChangeSceneToFile("res://src/UI/MainMenu.tscn");
    }

    private void _OnQuitToDesktopButtonPressed()
    {
        GD.Print("Quit to desktop button pressed.");
        GetTree().Quit();
    }
}
