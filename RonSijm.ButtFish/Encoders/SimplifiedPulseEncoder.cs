namespace RonSijm.ButtFish.Encoders;

public class SimplifiedPulseEncoder : ICharacterEncoder
{
    public string EncodeCharacter(char input)
    {
        var result = SimplifiedPulseDictionary[input];

        return result;
    }

    private static readonly Dictionary<char, string> SimplifiedPulseDictionary = new()
    {
        {'a', "."},
        {'b', ".."},
        {'c', "..."},
        {'d', "-"},
        {'e', "-."},
        {'f', "-.."},
        {'g', "-..."},
        {'h', "--"},

        {'1', "."},
        {'2', ".."},
        {'3', "..."},
        {'4', "-"},
        {'5', "-."},
        {'6', "-.."},
        {'7', "-..."},
        {'8', "--"},
    };
}