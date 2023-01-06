using System.Text;
using System.Text.RegularExpressions;

namespace RonSijm.ButtFish.Ascii;

public static class FENToCharArrayConverter
{
    private const string DigitsRegexString = "[\\d]+";
    // TODO (CORE7): Use [RegexGenerator(DigitsRegexString)]
    private static readonly Regex DigitsRegex = new(DigitsRegexString);

    public static char[,] ConvertToCharArray(this string fenCode)
    {
        var result = new char[8, 8];
        var fenLineSplit = fenCode.Split(' ');

        var piecePositionsArray = ReplaceDigitsWithEmptyStrings(fenLineSplit[0]).Split('/');

        if (piecePositionsArray.Length != 8)
        {
            return null;
        }

        for (var index = 0; index < piecePositionsArray.Length; index++)
        {
            var row = piecePositionsArray[index];

            for (var i = 0; i < 8; i++)
            {
                result[index, i] = row[i];
            }
        }

        return result;
    }

    public static bool IsWhiteToMove(this string fenCode)
    {
        var fenLineSplit = fenCode.Split(' ');
        if (fenLineSplit[1] == "w")
        {
            return true;
        }

        if (fenLineSplit[1] == "b")
        {
            return false;
        }

        throw new ArgumentOutOfRangeException();
    }

    private static string ReplaceDigitsWithEmptyStrings(string position)
    {
        var isMatch = DigitsRegex.Match(position);
        var bob = new StringBuilder(position);

        while (isMatch.Success)
        {
            var emptySquareCount = int.Parse(isMatch.Value);
            var newPositionString = string.Empty;

            for (var i = 0; i < emptySquareCount; i++)
            {
                newPositionString += " ";
            }

            bob.Remove(isMatch.Index, 1);
            bob.Insert(isMatch.Index, newPositionString);

            position = bob.ToString();
            isMatch = DigitsRegex.Match(position);
        }

        return position;
    }
}