namespace CarCrudProject.Utilities;

public static class FileName
{
    public static bool IsValidFileName(string fileName)
    {
        bool correctName = fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        fileName = fileName.ToLower();
        return correctName && ( fileName.EndsWith(".txt") || fileName.EndsWith(".csv") );
    }
    
    public static bool IsReservedName(string fileName)
    {
        fileName = fileName.ToUpper();
        string[] reservedNames = {"CON","PRN","AUX","NUL","COM1","COM2","COM3","COM4",
            "COM5","COM6","COM7","COM8","COM9","LPT1","LPT2","LPT3","LPT4","LPT5","LPT6",
            "LPT7","LPT8","LPT9"};

        return reservedNames.Contains(fileName);
    }
}