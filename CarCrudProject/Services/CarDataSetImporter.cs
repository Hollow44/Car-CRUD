using CarCrudProject.Data;
using CarCrudProject.Repositories;
using CarCrudProject.Utilities;

namespace CarCrudProject.Services;

public class CarDataSetImporter
{
    private readonly FileLoader loader = new();
    private readonly CarFactory factory = new();
    private readonly CarRepository repo = new();

    public void Import()
    {
        string path = @"D:\prog-cs\CarCrud\CarCrudProject\cars_dataset_modified_final";
        var lines = loader.Load(path).Skip(1); // 1 строчка это названия колонок

        foreach (var line in lines)
        {
            var columns = Parser.ParseCsv(line);
            var car = factory.Create(columns, repo.NextId);
            repo.Add(car);
        }
    }
}