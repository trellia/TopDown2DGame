using Godot;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        GD.Print("MainMenu.cs: Successfully initialized.");
    }

    public override void _Process(double delta)
    {
    }

    private void _OnNewGameButtonPressed()
    {
        GD.Print("New Game button pressed.");
        GetTree().ChangeSceneToFile("res://src/UI/ActiveGame.tscn");
    }

    private void _OnLoadGameButtonPressed()
    {
        GD.Print("Load Game button pressed.");
        GetTree().ChangeSceneToFile("res://src/UI/LoadGameMenu.tscn");
    }

    private void _OnSettingsButtonPressed()
    {
        GD.Print("Settings button pressed.");
        GetTree().ChangeSceneToFile("res://src/UI/SettingsMenu.tscn");
    }

    private void _OnHelpButtonPressed()
    {
        GD.Print("Help button pressed.");
        GetTree().ChangeSceneToFile("res://src/UI/HelpMenu.tscn");
    }

    private void _OnQuitButtonPressed()
    {
        GD.Print("Quit button pressed.");
        GetTree().Quit();
    }
}
