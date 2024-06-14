using Godot;
using TopDown2DGame.Scripts.Runtime.Common;

namespace TopDown2DGame.Scripts.Runtime.UI.Menus;

public partial class MainMenu : Control
{
    private MenuManager _menuManager;
    private LoadGameMenu _loadGameMenu;
    private SettingsMenu _settingsMenu;
    private HelpMenu _helpMenu;
    private CreditsMenu _creditsMenu;

    public override void _Ready()
    {
        AddToGroup("MenuGroup");

        _menuManager = GetParent<MenuManager>();

        GD.Print("MainMenu is ready.");
    }

    private void OnNewGameButtonPressed()
    {
        GD.Print("New Game button pressed.");
        SceneLoader.ChangeSceneToFile(this,
            "res://Scenes/Runtime/Maps/Harbor/Floor01.tscn");
    }

    private void OnLoadButtonPressed()
    {
        GD.Print("Load Game button pressed.");
        _menuManager.ShowMenu("LoadGameMenu");
    }

    private void OnSettingsButtonPressed()
    {
        GD.Print("Settings button pressed.");
        _menuManager.ShowMenu("SettingsMenu");
    }

    private void OnHelpButtonPressed()
    {
        GD.Print("Help button pressed.");
        _menuManager.ShowMenu("HelpMenu");
    }

    private void OnCreditsButtonPressed()
    {
        GD.Print("Credits button pressed.");
        _menuManager.ShowMenu("CreditsMenu");
    }

    private void OnQuitButtonPressed()
    {
        GD.Print("Quit button pressed.");
        GetTree().Quit();
    }
}
