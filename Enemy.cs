using Godot;
using System;

enum Direction{
    UP, DOWN, LEFT, RIGHT
}

public class Enemy : KinematicBody2D
{
    private PackedScene DeathEffect = ResourceLoader.Load<PackedScene>("res://DeathEffect.tscn");
    private PackedScene Axe = ResourceLoader.Load<PackedScene>("res://Axe.tscn");
    private const string MOVESFX = "res://enemyMove.wav";
    private const string HURTSFX = "res://hitHurt enemy.wav";
    private const string SHOOTSFX = "res://axe.wav";
    private EntitySfx sfx;
    private Direction randomDirection = Direction.RIGHT;
    private AnimationPlayer animationPlayer;
    private Movement movement;
    private Timer timer;
    private Timer attackTimer;
    private Stats stats;
    private Label hp;
    public override void _Ready()
    {
        stats = GetNode<Stats>("Stats");
        hp = GetNode<Label>("Label");
        hp.Text = stats.Health.ToString();
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("Idle");
        timer = GetNode<Timer>("Timer");
        attackTimer = GetNode<Timer>("AttackTimer");
        movement = new Movement{
            LIMIT_UP = 64,
            LIMIT_DOWN = 160,
            LIMIT_LEFT = 96,
            LIMIT_RIGHT = 192
        };
        timer.Start();
        attackTimer.Start();
        sfx = GetNode<EntitySfx>("Sfx");
    }

    public override void _PhysicsProcess(float delta)
    {
        if(timer.TimeLeft == 0){
            RandomMovement();
            if(attackTimer.TimeLeft == 0){
                Area2D axe = Axe.Instance<Area2D>();
                GetTree().CurrentScene.AddChild(axe);
                axe.GlobalPosition = GlobalPosition;
                attackTimer.Start();
                sfx.ChangeSFX(SHOOTSFX);
            }
        }
    }

    private Direction GetRandomDirection(){
        Godot.Collections.Array<Direction> directions = new Godot.Collections.Array<Direction>();
        directions.Add(Direction.UP);
        directions.Add(Direction.DOWN);
        directions.Add(Direction.LEFT);
        directions.Add(Direction.RIGHT);

        directions.Shuffle();
        return directions[0];
    }

    public void RandomMovement(){
        randomDirection = GetRandomDirection();

        if(randomDirection == Direction.UP || randomDirection == Direction.LEFT){
            movement.PIXEL_MOVEMENT = movement.PIXEL_MOVEMENT * (-1);
        }

        if(randomDirection == Direction.LEFT || randomDirection == Direction.RIGHT){
            if(GlobalPosition.x + movement.PIXEL_MOVEMENT >= movement.LIMIT_LEFT && GlobalPosition.x + movement.PIXEL_MOVEMENT <= movement.LIMIT_RIGHT){
                sfx.ChangeSFX(MOVESFX);
                GlobalPosition = GlobalPosition + new Vector2(movement.PIXEL_MOVEMENT, 0);
                timer.Start();
            }
        }

        if(randomDirection == Direction.UP || randomDirection == Direction.DOWN){
            if(GlobalPosition.y + movement.PIXEL_MOVEMENT >= movement.LIMIT_UP && GlobalPosition.y + movement.PIXEL_MOVEMENT <= movement.LIMIT_DOWN){
                sfx.ChangeSFX(MOVESFX);
                GlobalPosition = GlobalPosition + new Vector2(0, movement.PIXEL_MOVEMENT);
                timer.Start();
            }
        }
    }

    public void OnHurtBoxAreaEntered(Area2D area){
        if(area.IsInGroup("Projectile")){
            stats.Health -= (area as Arrow).Damage;
            hp.Text = stats.Health.ToString();
            sfx.ChangeSFX(HURTSFX);
        }
    }

    public void OnStatsNoHealthEventHandler(){
        QueueFree();
        DeathEffect deathEffect = DeathEffect.Instance<DeathEffect>();
        GetTree().CurrentScene.AddChild(deathEffect);
        deathEffect.GlobalPosition = GlobalPosition;
        deathEffect.Play();
        deathEffect.ChangeSFX("res://explosion enemy.wav");
    }
}
