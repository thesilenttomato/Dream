using System;

[Flags]
public enum RoomType
{
    Enmey = 1,
    Elite = 2,
    Shop = 4,
    Troops = 8,
    Event = 16,
    Wall = 32,
    Clear = 64,
    Boss = 128
}
public enum RoomState
{
    Locked,
    Attaintable
}
