using Godot;

namespace TopDown2DGame.Scripts.Runtime.Traps
{
    public partial class RazorTrap : Trap
    {
        [Export] public override int Damage { get; set; } = 10;
    }
}
