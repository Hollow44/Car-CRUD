using System.Globalization;

namespace CarCrudProject.Utilities;

public static class Parser
{
    public static bool IsValidNumberToParse(string num)
    {
        num = num.Trim();

        return double.TryParse(
            num,
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out _
        );
    }
    
    public static int ParseNumber(string userInput)
    {
        
        double num = double.Parse(userInput, CultureInfo.InvariantCulture);
        return (int)Math.Round(num, MidpointRounding.AwayFromZero);
    }

    public static bool IsUsedCar(string userInput)
    {
        userInput = userInput.ToLower().Trim();
        return userInput == "used";
    }

    public static string[] ParseCsv(string line)
    {
        return line.Split(',');
    }

    public static bool ParseChoiceIsYes(string userInput)
    {
        if (string.IsNullOrEmpty(userInput)) return false;
        
        userInput = userInput.Trim().ToLower();
        return userInput == "y";
    }

    public static string[] ParseCommand(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput)) return Array.Empty<string>();

        userInput = userInput.Trim();

        int firstSpaceIndex = userInput.IndexOf(' '); // понадобится для того чтобы первую комануд схватить

        if (firstSpaceIndex == -1) return new[] { userInput }; // если команда в одно слово - save || clear etc.

        
        // разделяю команду и аргументы
        string command = userInput.Substring(0, firstSpaceIndex);
        string arguments = userInput.Substring(firstSpaceIndex + 1).Trim();

        
        // если аргументы разделены запятыми (для команды 'add')
        if (arguments.Contains(','))
        {
            string[] argumentsSplit = arguments
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(argument => argument.Trim())
                .ToArray();

            return new[] { command }.Concat(argumentsSplit).ToArray();
        }

        // это для команд у которых только 1 аргумент (delete ID || edit ID etc.)
        if (arguments.Contains(' '))
        {
            throw new Exception($"'{userInput}' commands with 1 argument - do not support multiple arguments.\nFor more info check --help [command name]");
        }

        return new[] { command, arguments };
    }
}