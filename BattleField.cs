using Godot;
using System;

public class BattleField : Node2D
{
    private BGM track;
    public override void _Ready()
    {
        track = GetNode<BGM>("/root/Bgm");
        track.LoopSoundtrack(true);
    }
}
