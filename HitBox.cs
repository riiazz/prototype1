using Godot;
using System;

public class HitBox : Area2D
{
    [Export]
    public int Damage { get; set; } = 1;
}
