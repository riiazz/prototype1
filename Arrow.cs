using Godot;
using System;

public class Arrow : Area2D
{
    [Export]
    public int SPEED { get; set; } = 500;
    public override void _PhysicsProcess(float delta)
    {
        Vector2 arrowDirection = Vector2.Right.Rotated(Rotation);
        GlobalPosition += SPEED * arrowDirection * delta;
    }

    public void ArrowHitSomething(){
        QueueFree();
    }

    public void OnArrowAreaEntered(Area2D area){
        ArrowHitSomething();
    }

    public void OnVisibilityNotifier2DScreenExited(){
        QueueFree();
    }
}
