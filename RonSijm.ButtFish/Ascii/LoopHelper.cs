namespace RonSijm.ButtFish.Ascii
{
    internal static class Loop
    {
        internal static IEnumerable<int> Between(int minValue, int maxValue, bool inverted)
        {
            if (!inverted)
            {
                for (var i = minValue; i < maxValue; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (var i = maxValue - 1; i >= minValue; i--)
                {
                    yield return i;
                }
            }
        }
    }
}