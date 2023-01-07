namespace RonSijm.ButtFish.Encoders;

public class MorseEncoder : ICharacterEncoder
{
    public string EncodeCharacter(char input)
    {
        var validInput = MorseAlphabetDictionary.TryGetValue(input, out var result);

        if (!validInput)
        {
            Console.WriteLine($"Invalid input: '{input}' - Use a~h or 1~8", Color.Red);
        }

        return result;
    }

    private static readonly Dictionary<char, string> MorseAlphabetDictionary = new()
    {
        {'a', ".-"},
        {'b', "-..."},
        {'c', "-.-."},
        {'d', "-.."},
        {'e', "."},
        {'f', "..-."},
        {'g', "--."},
        {'h', "...."},
       
        {'1', ".----"},
        {'2', "..---"},
        {'3', "...--"},
        {'4', "....-"},
        {'5', "....."},
        {'6', "-...."},
        {'7', "--..."},
        {'8', "---.."},

        {' ', " "},
    };

}