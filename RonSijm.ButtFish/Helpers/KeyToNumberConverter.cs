namespace RonSijm.ButtFish.Helpers;

public static class KeyToNumberConverter
{
    public static int ToNumber(this ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.D0 => 0,
            ConsoleKey.D1 => 1,
            ConsoleKey.D2 => 2,
            ConsoleKey.D3 => 3,
            ConsoleKey.D4 => 4,
            ConsoleKey.D5 => 5,
            ConsoleKey.D6 => 6,
            ConsoleKey.D7 => 7,
            ConsoleKey.D8 => 8,
            ConsoleKey.D9 => 9,
            _ => throw new ArgumentOutOfRangeException(nameof(key))
        };
    }
}