using Godot;

namespace TopDown2DGame.Scripts.Runtime.Common;

public static class SceneLoader
{
    public static void ChangeSceneToPacked(Node node, string scenePath)
    {
        var destination = GD.Load<PackedScene>(scenePath);

        if (destination != null)
        {
            switch (node.GetTree().ChangeSceneToPacked(destination))
            {
                case Error.Ok:
                    GD.Print("Scene loaded successfully.");
                    break;
                case Error.InvalidParameter:
                    GD.Print("Error: Invalid parameter.");
                    break;
                case Error.CantCreate:
                    GD.Print("Error: Could not create the scene.");
                    break;
            }
        }
        else
        {
            GD.Print($"Error: Destination scene '{scenePath}' is null");
        }
    }

    public static void ChangeSceneToFile(Node node, string scenePath)
    {
        switch (node.GetTree().ChangeSceneToFile(scenePath))
        {
            case Error.Ok:
                GD.Print("Scene loaded successfully.");
                break;
            case Error.CantOpen:
                GD.Print("Error: Could not open the file.");
                break;
            case Error.CantCreate:
                GD.Print("Error: Could not create the scene.");
                break;
        }
    }
}
