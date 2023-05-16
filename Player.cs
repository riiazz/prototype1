using Godot;
using System;

public class Player : KinematicBody2D
{
    private PackedScene Arrow = ResourceLoader.Load<PackedScene>("res://Arrow.tscn");
    private PackedScene Bow = ResourceLoader.Load<PackedScene>("res://Bow.tscn");
    public Vector2 PlayerPosition { get; set; }
    private int LIMIT_RIGHT = 96;
    private int LIMIT_LEFT = 0;
    private int LIMIT_UP = 64;
    private int LIMIT_DOWN = 160;
    private int PIXEL_MOVEMENT = 32;
    private float COOLDOWN = 0.1f;
    private AnimationPlayer animationPlayer;
    private Timer timer;
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("Idle");
        timer = GetNode<Timer>("Timer");
        timer.WaitTime = COOLDOWN;
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveState(delta);
    }

    private void MoveState(float delta){
        if(Input.IsActionJustPressed("ui_right") && timer.TimeLeft == 0){
            if(GlobalPosition.x + PIXEL_MOVEMENT <= LIMIT_RIGHT){
                GlobalPosition = GlobalPosition + new Vector2(32, 0);
                timer.Start();
            }
        }
        if(Input.IsActionJustPressed("ui_down") && timer.TimeLeft == 0){
            if(GlobalPosition.y + PIXEL_MOVEMENT <= LIMIT_DOWN){
                GlobalPosition = GlobalPosition + new Vector2(0, 32);
                timer.Start();
            }
        }
        if(Input.IsActionJustPressed("ui_left") && timer.TimeLeft == 0){
            if(GlobalPosition.x - PIXEL_MOVEMENT >= LIMIT_LEFT){
                GlobalPosition = GlobalPosition + new Vector2(-32, 0);
                timer.Start();
            }
        }
        if(Input.IsActionJustPressed("ui_up") && timer.TimeLeft == 0){
            if(GlobalPosition.y - PIXEL_MOVEMENT >= LIMIT_UP){
                GlobalPosition = GlobalPosition + new Vector2(0, -32);
                timer.Start();
            }
        }
        if(Input.IsActionJustPressed("ui_select")){
            Node2D bow = Bow.Instance<Node2D>();
            bow.GlobalPosition = GlobalPosition;
            GetTree().CurrentScene.AddChild(bow);
            Area2D arrow = Arrow.Instance<Area2D>();
            GetTree().CurrentScene.AddChild(arrow);
            arrow.GlobalPosition = GlobalPosition;
        }
    }
}
