using Godot;

public partial class PauseMenu : Control
{
    public override void _Ready()
    {
        GD.Print("PauseMenu.cs: Successfully initialized.");
    }

    public override void _Process(double delta)
    {
    }

    private void _OnReturnButtonPressed()
    {
        GD.Print("Return button pressed.");
        GD.Print("The game will be unpaused.");

        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://src/UI/ActiveGame.tscn");
    }

    private void _OnSaveGameButtonPressed()
    {
        GD.Print("Save Game button pressed.");
        GetTree().ChangeSceneToFile("res://src/UI/SaveGameMenu.tscn");
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

    private void _OnQuitToMainMenuButtonPressed()
    {
        GD.Print("Quit to Main Menu button pressed.");
        GetTree().ChangeSceneToFile("res://src/UI/MainMenu.tscn");
    }

    private void _OnQuitToDesktopButtonPressed()
    {
        GD.Print("Quit to desktop button pressed.");
        GetTree().Quit();
    }


}
