using Godot;
using System;

public class Enemy : KinematicBody2D
{
    private AnimationPlayer animationPlayer;
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("Idle");
    }
}
