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

    public static bool IsValidCarStatus(string userInput)
    {
        userInput = userInput.ToLower();

        if (userInput == "new" || userInput == "used") return true;
        return false;
    }

    public static bool ParseCarStatus(string userInput)
    {
        userInput = userInput.ToLower();

        if (userInput == "used") return true;
        return false;
    }

    public static string[] ParseCommand(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput)) return new string[]{""};

        userInput = userInput.Trim();

        int firstSpaceIndex = userInput.IndexOf(" "); // понадобится для того чтобы первую комануд схватить

        if (firstSpaceIndex == -1) return new[] { userInput }; // если команда в одно слово - save || clear etc.

        
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

        
        // if (arguments.Contains(' '))
        // {
        //     Console.WriteLine($"'{command}' is not a correct command. See '--help'");
        // }

        return new[] { command, arguments };
    }
}