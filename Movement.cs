using Godot;
using System;

public class Movement : Node
{
    public int LIMIT_RIGHT { get; set; } = 96;
    public int LIMIT_LEFT { get; set; } = 0;
    public int LIMIT_UP { get; set; } = 64;
    public int LIMIT_DOWN { get; set; } = 160;
    public int PIXEL_MOVEMENT { get; set; } = 32;
    public float COOLDOWN { get; set; } = 0.1f;
}
