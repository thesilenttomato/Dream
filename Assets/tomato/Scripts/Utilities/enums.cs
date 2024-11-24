using System;

[Flags]
public enum EmoType
{
    Happness = 1,
    Sadness = 2,
    Calmness = 4,
    Fear = 8,
    Astonishment = 16,
    Shame = 32,
    Anger = 128,
    Hate = 256,
    AddHour = 512,
    MinHour = 1024,
}
