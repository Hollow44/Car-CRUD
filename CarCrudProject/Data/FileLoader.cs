using CarCrudProject.Models;
using CarCrudProject.Utilities;
using CarCrudProject.Services;
using CarCrudProject.Repositories;

namespace CarCrudProject.Data;

public static class FileLoader
{
    private static readonly CarFactory factory = new();
    public static void Load(string path)
    {
        foreach (var line in File.ReadLines(path).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            var columns = Parser.ParseCsv(line);
            if (columns.Length != 9) continue;
            
            var car = factory.Create(columns, CarRepository.NextId);
            CarRepository.Add(car);
        }
    }
}