using Godot;
using System;

public class EntitySfx : AudioStreamPlayer
{
    public void ChangeSFX(string path){
        AudioStream track = ResourceLoader.Load<AudioStream>(path);
        Stream = track;
        Play();
    }
}
