using CarCrudProject.Services;

namespace CarCrudProject.Utilities;

public static class FileName
{
    private static string[] reservedNames = {"CON","PRN","AUX","NUL","COM1","COM2","COM3","COM4",
        "COM5","COM6","COM7","COM8","COM9","LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"};
    
    public static bool IsValidFilePath(string path)
    {
        if (path.Length > 260)
        {
            Console.WriteLine($"'{path}'");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR! Name of the file can't be higher than 260 symbols");
            Logger.LogError($"'{path}' - name of the file can't be higher than 260 symbols");
            Console.ResetColor();
            return false;
        }
        try
        {
            string fullPath = Path.GetFullPath(path);

            if (File.Exists(fullPath))
            {
                Console.WriteLine($"'{fullPath}'");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"ATTENTION! File already exists, do you want to rewrite it?");
                Console.ResetColor();
                Console.WriteLine("press 'Y' to confirm OR type anything to decline");
                Logger.Write("USER INPUT", "waiting for user to confirm rewriting the file while saving");

                string userInput = Console.ReadLine() ?? "";
                Logger.Write("USER INPUT", userInput);
                
                return Parser.ParseChoiceIsYes(userInput);
            }

            string[] pathParts = fullPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            string fileName = Path.GetFileNameWithoutExtension(pathParts.Last());

            if (fileName.EndsWith(' '))
            {
                Console.WriteLine($"'{path}'");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR! Name of the file can't end with an empty space ' '");
                Logger.LogError("name of the file can't end with an empty space ' '");
                Console.ResetColor();
                return false;
            }
            foreach (var part in pathParts)
            {
                if (string.IsNullOrWhiteSpace(part)) continue;

                string name = Path.GetFileNameWithoutExtension(part);

                if (reservedNames.Contains(name, StringComparer.OrdinalIgnoreCase)
                    || name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"'{part}' ERROR! Invalid name, see '--help saveAs'");
                    Logger.LogError($"{part} invalid name");
                    Console.ResetColor();
                    return false;
                }
            }
            if (pathParts.Last().EndsWith(".txt", StringComparison.OrdinalIgnoreCase)
                || pathParts.Last().EndsWith(".csv", StringComparison.OrdinalIgnoreCase)) return true;

            Console.WriteLine($"'{path}' incorrect path. See '--help saveAs'");
            Logger.LogError($"'{path}' incorrect path");
            return false;
        }
        catch
        {
            Console.WriteLine($"'{path}' incorrect path. See '--help saveAs'");
            Logger.LogError($"'{path}' incorrect path");
            return false;
        }
    }
}