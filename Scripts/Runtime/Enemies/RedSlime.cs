using Godot;

namespace TopDown2DGame.Scripts.Runtime.Enemies;

public partial class RedSlime : Enemy
{
    [Export] public override int Damage { get; set; } = 30;
}
