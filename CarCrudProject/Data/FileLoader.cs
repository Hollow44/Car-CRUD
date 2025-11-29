using CarCrudProject.Models;
using CarCrudProject.Utilities;

namespace CarCrudProject.Data;

public class FileLoader
{
    public IEnumerable<string> Load(string path)
    {
        return File.ReadLines(path)
            .Where(line => !string.IsNullOrWhiteSpace(line));
    }
}