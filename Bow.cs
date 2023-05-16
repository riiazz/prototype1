using Godot;
using System;

public class Bow : Node2D
{
    private AnimationPlayer animationPlayer;
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("Shoot");
    }

    public void OnAnimationPlayerAnimationFinished(string animName){
        QueueFree();
    }
}
