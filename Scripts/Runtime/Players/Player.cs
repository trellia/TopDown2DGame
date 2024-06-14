using Godot;
using TopDown2DGame.Scripts.Runtime.Common;
using TopDown2DGame.Scripts.Runtime.Teleporters;
using TopDown2DGame.Scripts.Runtime.Traps;
using TopDown2DGame.Scripts.Runtime.Enemies;

namespace TopDown2DGame.Scripts.Runtime.Players;

public partial class Player : CharacterBody2D
{
    // Collisions.
    private Vector2 _velocity;
    private Vector2 _knockbackVelocity;
    private float _knockbackDamping = 1000f;
    private bool _isFlashing = false;
    private float _flashDuration = 0.5f;

    // Animations.
    [Export] public NodePath TransitionNodePath;
    private CanvasLayer _transitionLayer;
    private AnimationPlayer _transitionAnimation;

    // User Interface (HUD).
    //private UI _ui;
    // private Vector2 _screenSize;

    // Player attributes.
    private Vector2 Direction { set; get; } = Vector2.Down;
    [Export] public float Speed { get; set; } = 100f;
    [Export] public int Experience { get; set; } = 0;
    [Export] public int Level { get; set; } = 1;
    [Export] public int MaxHealth { get; set; } = 100;
    private int _currentHealth;

    [Signal]
    public delegate void PlayerDefeatedEventHandler();

    private int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            int previousHealth = _currentHealth;
            if (value <= 0)
            {
                _currentHealth = 0;
            }
            else if (value > MaxHealth)
            {
                _currentHealth = MaxHealth;
            }
            else
            {
                _currentHealth = value;
            }

            GD.Print($"Player Health updated:");
            GD.Print($"Previous Health: {previousHealth}");
            GD.Print($"New Health: {_currentHealth}");

            UpdateHealth(_currentHealth);
            if (_currentHealth <= 0)
            {
                EmitSignal(SignalName.PlayerDefeated);
            }
        }
    }

    public override void _Ready()
    {
        _currentHealth = MaxHealth;

        // Set up collisions for area transitions.
        SetupOnPlayerEnteredStairs();
        SetupOnDoors();
        SetupOnPlayerEnteredLocalTeleporter();
        SetupOnPlayerEnteredGlobalTeleporter();

        // Set up collisions for environmental traps.
        SetupOnPlayerEnteredTrap();

        // Set up collisions with enemies.
        SetupOnPlayerAttackedByEnemy();
        var damageFlashTimer = GetNode<Timer>("DamageFlashTimer");
        damageFlashTimer.Timeout += ResetFlash;

        // Set up collisions with interactive objects.

        // Set up collisions with collectible objects.

        // Set up animations.

        // Set up the UI.
    }


    public override void _PhysicsProcess(double delta)
    {
        var velocity = Vector2.Zero;
        var walk = Input.IsActionPressed("Walk");

        if (Input.IsActionPressed("MoveRight"))
        {
            velocity.X += 1;
            Direction = Vector2.Right;
        }

        if (Input.IsActionPressed("MoveLeft"))
        {
            velocity.X -= 1;
            Direction = Vector2.Left;
        }

        if (Input.IsActionPressed("MoveUp"))
        {
            velocity.Y -= 1;
            Direction = Vector2.Up;
        }

        if (Input.IsActionPressed("MoveDown"))
        {
            velocity.Y += 1;
            Direction = Vector2.Down;
        }

        if (Input.IsActionPressed("Action"))
        {
            // EmitSignal(nameof(ActionEventHandler));
        }

        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        if (velocity.Length() > 0)
        {
            velocity = walk
                ? velocity.Normalized() * Speed / 2
                : velocity.Normalized() * Speed;
            animatedSprite2D.Play();
        }
        else
        {
            animatedSprite2D.Stop();
        }

        if (velocity.X > 0)
        {
            animatedSprite2D.Animation = "WalkRight";
        }
        else if (velocity.X < 0)
        {
            animatedSprite2D.Animation = "WalkLeft";
        }
        else if (velocity.Y > 0)
        {
            animatedSprite2D.Animation = "WalkDown";
        }
        else if (velocity.Y < 0)
        {
            animatedSprite2D.Animation = "WalkUp";
        }

        if (_knockbackVelocity.Length() > 0)
        {
            velocity += _knockbackVelocity;
            _knockbackVelocity = _knockbackVelocity.MoveToward(Vector2.Zero,
                (float)(_knockbackDamping * delta));
        }

        Position += velocity * (float)delta;

        MoveAndSlide();
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        CurrentHealth = 100;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    private void SetupOnPlayerEnteredStairs()
    {
        GD.Print($"Setting up the stairs...");
        foreach (var node in GetTree().GetNodesInGroup("Stairs"))
        {
            GD.Print($"Checking Group: Stairs: {node.Name}");
            if (node is Stairs stairs)
            {
                stairs.PlayerEnteredStairs += OnPlayerEnteredStairs;
            }
        }
    }

    private void OnPlayerEnteredStairs(Vector2 destination)
    {
        GD.Print("Player entered stairs.");
        MovePlayerTo(destination);
    }

    private void SetupOnDoors()
    {
        foreach (var node in GetTree().GetNodesInGroup("Doors"))
        {
            GD.Print($"Checking Group: Doors: {node.Name}");
            if (node is UnlockedDoor door)
            {
                door.PlayerEnteredDoor += OnPlayerEnteredUnlockedDoor;
                door.PlayerExitedDoor += OnPlayerExitedUnlockedDoor;
            }
        }
    }

    private void OnPlayerEnteredUnlockedDoor(Vector2 destination)
    {
        GD.Print($"Player opened the door.");
        // GD.Print($"Moving to destination: {destination}");
        // Position = destination;
    }

    private void OnPlayerExitedUnlockedDoor(Vector2 destination)
    {
        GD.Print($"Player closed the door.");
        // GD.Print($"Moving to destination: {destination}");
        // Position = destination;
    }

    private void SetupOnPlayerEnteredLocalTeleporter()
    {
        foreach (var node in GetTree().GetNodesInGroup("LocalTeleporters"))
        {
            GD.Print($"Checking Group: LocalTeleporters: {node.Name}");
            if (node is LocalTeleporter localTeleporter)
            {
                localTeleporter.PlayerEnteredLocalTeleporter +=
                    OnPlayerEnteredLocalTeleporter;
            }
        }
    }

    private void OnPlayerEnteredLocalTeleporter(Vector2 destination)
    {
        GD.Print(
            $"Player moving through Local Teleporter to new destination: {destination}"
        );
        Position = destination;
    }

    private void SetupOnPlayerEnteredGlobalTeleporter()
    {
        GD.Print($"Checking Group: GlobalTeleporters...");
        foreach (var node in GetTree().GetNodesInGroup("GlobalTeleporters"))
        {
            GD.Print($"Checking Group: GlobalTeleporters: {node.Name}");
            if (node is GlobalTeleporter globalTeleporter)
            {
                globalTeleporter.PlayerEnteredGlobalTeleporter +=
                    OnPlayerEnteredGlobalTeleporter;
            }
        }
    }

    private void OnPlayerEnteredGlobalTeleporter(string destination)
    {
        GD.Print($"Player moving through Global Teleporter to new destination: {destination}");
        SceneLoader.ChangeSceneToFile(this, destination);
    }

    private void SetupOnPlayerEnteredTrap()
    {
        foreach (var node in GetTree().GetNodesInGroup("Traps"))
        {
            GD.Print($"Checking Group: Traps: {node.Name}");
            if (node is Trap trap)
            {
                trap.PlayerEnteredTrap += OnPlayerEnteredTrap;
            }
        }
    }

    private void OnPlayerEnteredTrap(string trapName, int damage,
        Vector2 knockBackForce)
    {
        GD.Print($"{Name} entered the trap: {trapName}");
        GD.Print($"{Name} took {damage} damage from {trapName}");
        CurrentHealth -= damage;
        _knockbackVelocity = knockBackForce;

        if (_isFlashing) return;
        _isFlashing = true;
        GetNode<Timer>("DamageFlashTimer").Start(_flashDuration);
        FlashRed();
    }

    private void SetupOnPlayerAttackedByEnemy()
    {
        GD.Print("Check enemies group");
        foreach (var node in GetTree().GetNodesInGroup("Enemies"))
        {
            GD.Print($"Checking Group: Enemies: {node.Name}");
            if (node is Enemy enemy)
            {
                enemy.PlayerAttackedByEnemy += OnPlayerAttackedByEnemy;
            }
        }
    }

    private void OnPlayerAttackedByEnemy(string enemyName, int damage, Vector2
        knockBackForce)
    {
        GD.Print($"{Name} attacked by enemy: {enemyName}");
        GD.Print($"{Name} took {damage} damage from {enemyName}");
        CurrentHealth -= damage;
        _knockbackVelocity = knockBackForce;

        if (_isFlashing) return;
        _isFlashing = true;
        GetNode<Timer>("DamageFlashTimer").Start(_flashDuration);
        FlashRed();
    }

    private void FlashRed()
    {
        GD.Print("Player and UI are flashing red.");
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Modulate =
            new Color(1, 0, 0);
        GetNode<Label>("CanvasLayer/HealthAmount").Modulate =
            new Color(1, 0, 0);
    }

    private void ResetFlash()
    {
        GD.Print("Player and UI stopped flashing red.");
        _isFlashing = false;
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Modulate =
            new Color(1, 1, 1);
        GetNode<Label>("CanvasLayer/HealthAmount").Modulate =
            new Color(0, 1, 0);
    }

    private void MovePlayerTo(Vector2 targetPosition)
    {
        Position = targetPosition;
    }

    private void UpdateHealth(int amount)
    {
        GetNode<Label>("CanvasLayer/HealthAmount").Text = amount.ToString();
    }
}
