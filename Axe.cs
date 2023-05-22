using Godot;
using System;

public class Axe : HitBox
{
    [Export]
    private int SPEED = 250;
    private AnimationPlayer animationPlayer;
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("Throw");
    }
    public override void _PhysicsProcess(float delta)
    {
        Vector2 axeDirection = Vector2.Left.Rotated(Rotation);
        GlobalPosition += SPEED * axeDirection * delta;
    }

    private void AxeHitSomething(){
        QueueFree();
    }

    public void OnVisibilityNotifier2DScreenExited(){
        QueueFree();
    }

    public void OnAxeAreaEntered(Area2D area){
        AxeHitSomething();
    }
}
