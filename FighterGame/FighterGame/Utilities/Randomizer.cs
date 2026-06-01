namespace FighterGame.Utilities;

static class Randomizer
{
    private static readonly Random _random = new Random();

    public static int GetInt( int maxValue )
    {
        return _random.Next( maxValue );
    }

    public static int GetInt( int minValue, int maxValue )
    {
        return _random.Next( minValue, maxValue );
    }

    public static double GetDouble()
    {
        return _random.NextDouble();
    }
}