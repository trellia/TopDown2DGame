using Godot;

namespace TopDown2DGame.Scripts.Runtime.Traps
{
    public partial class KnifeTrap : Trap
    {
        [Export] public override int Damage { get; set; } = 25;
    }
}
