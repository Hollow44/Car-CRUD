using CarCrudProject.Repositories;
using CarCrudProject.Utilities;

namespace CarCrudProject.Services;

public static class Commands
{
    private static readonly CarRepository repo = new();
    private static readonly CarFactory factory = new();

    public static void Show()
    {
        foreach (var car in repo.Cars)
        {
            car.ShowInfo();
        }
    }

    // TODO: нужно добавить отдельную проверку является ли argument 'id' или 'all'
    public static void Show(string argument) 
    {
        foreach (var car in repo.Cars)
        {
            if (car.GetId() == id)
            {
                car.ShowInfo();
            }
        }
    }
    

    public static void Exit()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Are you sure you want to exit the app?");
        Console.ResetColor();
        Console.WriteLine("Press 'Y' to confirm OR type anything to decline");
        string userInput = Console.ReadLine() ?? "";

        if (Parser.ParseChoiceYesOrNo(userInput)) Environment.Exit(0);
    }

    public static void Add(string argument)
    {
        
        string[] arguments = argument.Split(',');

        if (arguments.Length == 9)
        {
            if ( (Parser.IsValidNumberToParse(arguments[3]) && Parser.ParseNumber(arguments[3]) > 0)
                && (Parser.IsValidNumberToParse(arguments[4]) && Parser.ParseNumber(arguments[4]) > 0)
                && (Parser.IsValidNumberToParse(arguments[6]) && Parser.ParseNumber(arguments[6]) > 0)
                && (Parser.IsValidNumberToParse(arguments[8]) && Parser.ParseNumber(arguments[8]) >= 0) )
            {
                var car = factory.Create(arguments, repo.NextId);
                repo.Add(car);
            }
            else Commands.Help("add");
        }
        else Commands.Help("add");
    }

    public static void Help()
    {
        Console.WriteLine("These are all the commands available:");
        Console.WriteLine();
        
        Console.WriteLine("display all the cars or 1 particular car");
        Console.WriteLine("\tshow all\tShows the list of all cars with detailed information");
        Console.WriteLine("\tshow [id]\tShow the details about 1 car with the particular 'id'");
        Console.WriteLine();
        
        Console.WriteLine("add the car to the list");
        Console.WriteLine("\tadd [company], [model], [engine], [horse power], [price], [fuel type], [seat], [car status], [mileage]" +
                          "\tAdd car to the list by passing correct arguments (all the arguments must be separated with ','." +
                          " Do not include '[' or ']' symbols - they are shown for easier demonstration purposes. For the [horse power]," +
                          " [price], [seat], and [mileage] arguments only pass numbers and they should not be negative. For the" +
                          " [car status] argument only pass 'new' or 'used' argument");
        Console.WriteLine();
        
        Console.WriteLine("edit the existing car's information");
        Console.WriteLine("\tedit [id]\tPass only [id] argument to be able to edit all of the particular's car information");
        Console.WriteLine("\tedit [id] [argument]\tHere in argument you can edit the particular argument of the particular's car." +
                          "For example: edit [3] [model] ");
        Console.WriteLine();
        
        
    }

    public static void Help(string command)
    {
        
    }
}