using CarCrudProject.Repositories;

namespace CarCrudProject.Services;

public static class Commands
{
    private static readonly CarRepository repo = new();


    public static void Show()
    {
        foreach (var car in repo.Cars)
        {
            car.ShowInfo();
        }
    }

    public static void Show(int id)
    {
        foreach (var car in repo.Cars)
        {
            if (car.GetId() == id)
            {
                car.ShowInfo();
            }
        }
    }

    public static void Help()
    {
        
    }

    public static void Help(string command)
    {
        
    }
}