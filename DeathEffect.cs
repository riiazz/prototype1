using Godot;
using System;

public class DeathEffect : AnimatedSprite
{
    private AudioStreamPlayer audioStreamPlayer;
    public override void _Ready()
    {
        audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    public void ChangeSFX(string path){
        AudioStream SFX = ResourceLoader.Load<AudioStream>(path);
        audioStreamPlayer.Stream = SFX;
        audioStreamPlayer.Play();
    }

    public void OnDeathEffectAnimationFinished(){
        QueueFree();
    }
}
