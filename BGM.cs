using Godot;
using System;

public class BGM : Node2D
{
    private AudioStreamPlayer audioStreamPlayer;
    public override void _Ready()
    {
        audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    public void LoopSoundtrack(bool isLoop){
        audioStreamPlayer.Play();
        if(isLoop){
            OnAudioStreamPlayerFinished();
        }
    }

    public void OnAudioStreamPlayerFinished(){
        audioStreamPlayer.Play();
    }
}
