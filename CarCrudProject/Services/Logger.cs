namespace CarCrudProject.Services;

public static class Logger
{
    private static string path = $"..\\..\\..\\..\\CarCrudProject\\logs/{DateTime.Now:dd.MM.yyyy}";
    private static string fullPath = Path.GetFullPath(path);

    public static void LogError(string message)
    {
        Write("ERROR", message);
    }
    public static void Write(string logType, string message)
    {
        
        string line = $"[{DateTime.Now:G}] {logType}: {message}\n";
        File.AppendAllText(fullPath, line);
    }
}