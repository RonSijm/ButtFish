namespace RonSijm.ButtFish;

public static class EngineSelector
{
    public static (string Name, string Path) SelectEngine(Dictionary<string, string> engines)
    {
        var locatedEngines = LocateViableEngines(engines);

        if (!locatedEngines.Any())
        {
            ShowNoViableEnginesWarning(engines);

            return (null, null);
        }

        if (locatedEngines.Count == 1)
        {
            var engineToUse = locatedEngines.First();

            return engineToUse;
        }

        Console.WriteLine($"Located {locatedEngines.Count} Engines.");

        do
        {
            Console.WriteLine("Which one do you want to use?");

            for (var index = 0; index < locatedEngines.Count; index++)
            {
                var locatedEngine = locatedEngines[index];
                Console.WriteLine($"{index + 1}: {locatedEngine.Name}");
            }

            var usageChoice = Console.ReadKey().KeyChar;
            var validChoiceResult = int.TryParse(usageChoice.ToString(), out var selectedIndex);

            if (!validChoiceResult || selectedIndex > locatedEngines.Count)
            {
                Console.WriteLine($"{usageChoice} is not a valid option.");
            }
            else
            {
                return locatedEngines[selectedIndex - 1];
            }
        } while (true);
    }

    private static void ShowNoViableEnginesWarning(Dictionary<string, string> engines)
    {
        Console.WriteLine("UCIEngine is missing");

        var startupPath = Environment.CurrentDirectory;

        foreach (var optionsEngine in engines)
        {
            Console.WriteLine(
                $"Expected Executable for {optionsEngine.Key} to be at '{startupPath}\\{optionsEngine.Value}'");
        }

        Console.WriteLine(
            "You can download StockFish over here: https://github.com/RonSijm/ButtFish/blob/main/RonSijm.ButtFish/stockfish_15_x64_avx2.exe");
        Console.WriteLine("You can LC0 over here: https://lczero.org/play/download/");
    }

    private static List<(string Name, string Path)> LocateViableEngines(Dictionary<string, string> engines)
    {
        var startupPath = Environment.CurrentDirectory;

        var locatedEngines = new List<(string Name, string Path)>();

        foreach (var option in engines)
        {
            var expectedEnginePath = $"{startupPath}\\{option.Value}";

            if (File.Exists(expectedEnginePath))
            {
                locatedEngines.Add((option.Key, option.Value));
            }
        }

        return locatedEngines;
    }
}