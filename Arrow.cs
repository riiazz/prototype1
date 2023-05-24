using Godot;
using System;

public class Arrow : HitBox
{
    [Export]
    public int SPEED { get; set; } = 500;
    private PackedScene SFX = ResourceLoader.Load<PackedScene>("res://EntitySfx.tscn");
    private const string path = "res://weaponCollision.wav";
    public override void _PhysicsProcess(float delta)
    {
        Vector2 arrowDirection = Vector2.Right.Rotated(Rotation);
        GlobalPosition += SPEED * arrowDirection * delta;
    }

    public void ArrowHitSomething(){
        QueueFree();
    }

    public void OnArrowAreaEntered(Area2D area){
        if(area.IsInGroup("Projectile")){
            EntitySfx collisionSFX = SFX.Instance<EntitySfx>();
            GetTree().CurrentScene.AddChild(collisionSFX);
            collisionSFX.ChangeSFX(path);
        }
        ArrowHitSomething();
    }

    public void OnVisibilityNotifier2DScreenExited(){
        QueueFree();
    }
}
