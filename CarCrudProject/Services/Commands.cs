using CarCrudProject.Repositories;
using CarCrudProject.Utilities;
using CarCrudProject.Models;

namespace CarCrudProject.Services;

public static class Commands
{
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
  
    public static void Show(string argument)
    {
        string userInput = argument.ToLower();
        if (userInput == "all")
        {
            foreach (var car in CarRepository.Cars)
            {
                car.ShowInfo();
            }
        }
        else if (Parser.IsValidNumberToParse(argument))
        {
            int id = Parser.ParseNumber(argument);

            if (id <= 0 || id >= CarRepository.NextId)
                Console.WriteLine($"'show {id}' invalid id");

            var car = CarRepository.Cars.FirstOrDefault(car => car.GetId() == id);
            
            if (car == null) Console.WriteLine($"'show {id}' car with this id already been deleted");
            else car.ShowInfo();
        }
        else
        {
            Console.WriteLine($"'show {argument}' is not correct command. See '--help show'");
        }
    }

    public static void Edit(string argument)
    {
        if (!Parser.IsValidNumberToParse(argument))
        {
            Console.WriteLine($"'edit {argument}' is not correct command. See '--help edit'");
            return;
        }

        int id = Parser.ParseNumber(argument);

        var car = CarRepository.Cars.FirstOrDefault(car => car != null && car.GetId() == id);

        if (car == null)
        {
            Console.WriteLine($"there is no car with id '{id}'");
            return;
        }
        
        Console.WriteLine($"ID: {car.GetId()}");
        car.ShowInfoForEdit();

        string userInput = Console.ReadLine() ?? "";
        
        if (userInput.Contains(','))
        {
            string[] arguments = userInput
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(argument => argument.Trim())
                .ToArray();

            if (arguments.Length == 9)
            {
                string company = arguments[0];
                string model = arguments[1];
                string engine = arguments[2];
                string fuelType = arguments[5];
                
                string hpStr = arguments[3];
                string priceStr = arguments[4];
                string seatStr = arguments[6];
                string carStatusStr = arguments[7];
                string mileageStr = arguments[8];
                
                if (Parser.IsValidNumberToParse(hpStr)
                    && Parser.IsValidNumberToParse(priceStr)
                    && Parser.IsValidNumberToParse(seatStr)
                    && Parser.IsValidNumberToParse(mileageStr)
                    && Parser.IsValidCarStatus(carStatusStr))
                {
                    int hp = Parser.ParseNumber(hpStr);
                    int price = Parser.ParseNumber(priceStr);
                    int seat = Parser.ParseNumber(seatStr);
                    int mileage = Parser.ParseNumber(mileageStr);
                    bool carStatus = Parser.ParseCarStatus(carStatusStr);

                    if (hp > 0 && price > 0 && seat > 0 && mileage >= 0)
                    {
                        car.SetCompany(company);
                        car.SetModel(model);
                        car.SetEngine(engine);
                        car.SetHorsePower(hp);
                        car.SetPrice(price);
                        car.SetFuelType(fuelType);
                        car.SetSeat(seat);
                        car.SetIsUsed(carStatus);
                        car.SetMileage(mileage);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"car with id '{id}' has been updated");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"'edit {argument}' horsepower, price and seat can not less than 1, mileage can not be less than 0. See '--help edit'");
                    }
                }
                else Console.WriteLine($"'edit {argument}' invalid numeric arguments. See '--help edit'");
            }
            else Console.WriteLine($"'edit {argument}' invalid arguments. See '--help edit'");
        }

        
    }

    public static void Delete(string argument)
    {
        if (!Parser.IsValidNumberToParse(argument))
        {
            Console.WriteLine($"'delete {argument}' is not correct command. See '--help delete'");
            return;
        }

        int id = Parser.ParseNumber(argument);

        var car = CarRepository.Cars.FirstOrDefault(car => car != null && car.GetId() == id);

        if (car == null)
        {
            Console.WriteLine($"there is no car with id '{id}'");
            return;
        }
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"you are about to delete this car:");
        Console.ResetColor();
        car.ShowInfoForDelete();
        Console.WriteLine("are you sure you want to delete this car?");
        Console.WriteLine("press 'Y' to confirm OR type anything to decline");

        string userInput = Console.ReadLine() ?? "";
        
        if (Parser.ParseChoiceIsYes(userInput))
        {
            CarRepository.Cars.Remove(car);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"car with id '{id}' has been removed successfully");
            Console.ResetColor();
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
            string carStatusStr = arguments[7];
            string hpStr = arguments[3];
            string priceStr = arguments[4];
            string seatStr = arguments[6];
            string mileageStr = arguments[8];


            if (Parser.IsValidNumberToParse(hpStr)
                && Parser.IsValidNumberToParse(priceStr)
                && Parser.IsValidNumberToParse(seatStr)
                && Parser.IsValidNumberToParse(mileageStr)
                && Parser.IsValidCarStatus(carStatusStr))
            {
                int hp = Parser.ParseNumber(hpStr);
                int price = Parser.ParseNumber(priceStr);
                int seat = Parser.ParseNumber(seatStr);
                int mileage = Parser.ParseNumber(mileageStr);
                bool carStatus = Parser.ParseCarStatus(carStatusStr);

                if (hp > 0 && price > 0 && seat > 0 && mileage >= 0)
                {
                    var car = factory.Create(arguments, CarRepository.NextId);
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"new car with id: {CarRepository.NextId} has been added successfully");
                    Console.ResetColor();
                    
                    CarRepository.Add(car);
                }
                else
                {
                    Console.WriteLine($"'add {argument}' horsepower, price and seat can not less than 1, mileage can not be less than 0. See '--help add'");
                }
            }
            else Console.WriteLine($"'add {argument}' invalid numeric arguments. See '--help add'");
        }
        else Console.WriteLine($"'add {argument}' invalid arguments. See '--help add'");
    }

    public static void Help()
    {
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
        Console.WriteLine($"'{command}' not working");
    }
}