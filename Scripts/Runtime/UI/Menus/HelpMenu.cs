using Godot;

namespace TopDown2DGame.Scripts.Runtime.UI.Menus;

public partial class HelpMenu : Control
{
    private MenuManager _menuManager;
    private Button _returnButton;

    public override void _Ready()
    {
        AddToGroup("MenuGroup");

        _menuManager = GetParent<MenuManager>();

        Button returnButton = GetNode<Button>("VBoxContainer/ReturnButton");
        returnButton.Pressed += OnReturnButtonPressed;
        GD.Print("HelpMenu is ready.");
    }

    private void OnReturnButtonPressed()
    {
        GD.Print("Return button pressed.");
        _menuManager.OnReturnButtonPressed();
    }
}
