using CarCrudProject.Repositories;
using CarCrudProject.Utilities;
using CarCrudProject.Models;

namespace CarCrudProject.Services;

public static class Commands
{
    private static readonly CarRepository repo = new();
    private static readonly CarFactory factory = new();

    /* TODO:
        show id
        show all
        show where price > 0
            
        edit id
        edit id model Mercedez benz, horsepower 8
            
        saveas path

        save
            
        --help
        --help [command]

        добавить логирование
     */
    public static void Show()
    {
        foreach (var car in repo.Cars)
        {
            car.ShowInfo();
        }
    }

    public static void Show(string argument) 
    {
        if (argument == "all")
        {
            foreach (var car in repo.Cars)
            {
                car.ShowInfo();
            }
        }
    }

    public static void Edit(string argument)
    {

    }

    public static void Delete(string userIput)
    {
        if (Parser.IsValidNumberToParse(userIput))
        {
            int id = Parser.ParseNumber(userIput);

            if (id <= 0 || id >= repo.NextId)
                Console.WriteLine($"'delete {id}' invalid id");

            if (repo.Cars.Exists(car => car.GetId() == id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"you are about to delete this car:");
                Console.ResetColor();
                repo.Cars[id - 1].ShowInfo();
                Console.WriteLine("are you sure you want to delete this car?");
                Console.WriteLine("press 'Y' to confirm OR type anything to decline");

                string userInput = Console.ReadLine() ?? "";

                if (Parser.ParseChoiceIsYes(userInput))
                {
                    repo.Cars[id - 1] = null;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"car with id '{id}' has been removed successfully");
                    Console.ResetColor();
                }
            }
            else Console.WriteLine($"there is no car with id '{id}'. See '--help delete'");
        }
        else
        {
            Console.WriteLine($"'delete {userIput}' is not correct command. See '--help delete'");
        }
    }
    public static void Exit()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("are you sure you want to exit the app?");
        Console.ResetColor();
        Console.WriteLine("press 'Y' to confirm OR type anything to decline");
        string userInput = Console.ReadLine() ?? "";

        if (Parser.ParseChoiceIsYes(userInput)) Environment.Exit(0);
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