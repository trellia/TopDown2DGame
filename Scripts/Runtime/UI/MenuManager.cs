using Godot;
using System.Collections.Generic;

namespace TopDown2DGame.Scripts.Runtime.UI;

public partial class MenuManager : Control
{
    private readonly List<Control> _menus = [];

    public override void _Ready()
    {
        var menuNodes = GetTree().GetNodesInGroup("MenuGroup");

        GD.Print($"Found {menuNodes.Count} nodes in MenuGroup");

        foreach (var node in menuNodes)
        {
            if (node is not Control menu) continue;
            GD.Print(menu.Name);
            _menus.Add(menu);
            menu.Visible = false;
        }

        ShowMenu("MainMenu");
        GetNode<Button>("MainMenu/VBoxContainer/NewGameButton").GrabFocus();
        GD.Print("MenuManager is ready.");
    }

    public void ShowMenu(string menuName)
    {
        GD.Print($"Calling ShowMenu with {menuName}");
        foreach (var menu in _menus)
        {
            GD.Print($"Menu {menu.Name}; Visibility: {menu.Visible}");
            menu.Visible = menu.Name == menuName;
            GD.Print($"Menu {menu.Name}; Visibility: {menu.Visible}");
        }

        GD.Print($"_menus: {_menus.Count}");
        ResetGrabFocus();
    }

    public void OnReturnButtonPressed()
    {
        ShowMenu("MainMenu");
    }

    private void ResetGrabFocus()
    {
        GetNode<Button>("MainMenu/VBoxContainer/NewGameButton").GrabFocus();
    }
}
