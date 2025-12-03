using CarCrudProject.Repositories;
using CarCrudProject.Utilities;
using CarCrudProject.Models;

namespace CarCrudProject.Services;

public static class Commands
{
    private static readonly CarFactory factory = new();

    /* TODO:
        show where price > 0
        edit id model Mercedez benz, horsepower 8
     */
  
    public static void Show(string argument)
    {
        string userInput = argument.ToLower();
        int carCount = 0;
        if (userInput == "all")
        {
            foreach (var car in CarRepository.Cars)
            {
                car.ShowInfo();
                carCount++;
            }
            Logger.Write("SHOW",$"displayed {carCount} cars");
        }
        else if (Parser.IsValidNumberToParse(argument))
        {
            int id = Parser.ParseNumber(argument);

            if (id <= 0 || id >= CarRepository.NextId)
            {
                Console.WriteLine($"'show {id}' invalid id"); 
                Logger.LogError($"'show {id}' invalid id");
            }
            
            var car = CarRepository.Cars.FirstOrDefault(car => car.GetId() == id);

            if (car == null)
            {
                Console.WriteLine($"'show {id}' car with this id already been deleted");
                Logger.Write("INFO",$"'show {id}' car with this id already been deleted");
            }
            else car.ShowInfo();
            Logger.Write("SHOW", car!.GetInfo());
        }
        else
        {
            Console.WriteLine($"'show {argument}' is not correct command. See '--help show'");
            Logger.LogError($"'show {argument}' is not correct command");
        }
    }

    public static void Edit(string argument)
    {
        if (!Parser.IsValidNumberToParse(argument))
        {
            Console.WriteLine($"'edit {argument}' is not correct command. See '--help edit'");
            Logger.LogError($"'edit {argument}' is not correct command");
            return;
        }

        int id = Parser.ParseNumber(argument);
        
        if (id < 1 || id > (CarRepository.NextId - 1))
        {
            Console.WriteLine($"'edit {id}' invalid id. See '--help edit'");    
            Logger.LogError($"'edit {id}' invalid id");
            return;
        }
        
        var car = CarRepository.Cars.FirstOrDefault(car => car.GetId() == id);

        if (car == null)
        {
            Console.WriteLine($"there is no car with id '{id}'");
            Logger.LogError($"there is no car with id '{id}'");
            return;
        }
        
        Console.WriteLine($"ID: {car.GetId()}");
        car.ShowInfoForEdit();
        Logger.Write("EDIT", $"{car.GetInfo()}");

        string userInput = Console.ReadLine() ?? "";
        Logger.Write("USER INPUT", userInput);
        
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
                        Logger.Write("EDIT", $"before EDIT: ID {car.GetId()},{car.GetCompany()} {car.GetModel()}," +
                                     $"{car.GetEngine()},{car.GetHorsePower()},{car.GetPrice()},{car.GetFuelType()}," +
                                     $"{car.GetSeat()},{car.GetIsUsed()},{car.GetMileage()}");
                        car.SetCompany(company);
                        car.SetModel(model);
                        car.SetEngine(engine);
                        car.SetHorsePower(hp);
                        car.SetPrice(price);
                        car.SetFuelType(fuelType);
                        car.SetSeat(seat);
                        car.SetIsUsed(carStatus);
                        car.SetMileage(mileage);

                        Logger.Write("EDIT", $"after EDIT: ID {car.GetId()},{car.GetCompany()} {car.GetModel()}," +
                                             $"{car.GetEngine()},{car.GetHorsePower()},{car.GetPrice()},{car.GetFuelType()}," +
                                             $"{car.GetSeat()},{car.GetIsUsed()},{car.GetMileage()}");
                        
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"car with id '{id}' has been updated");
                        Logger.Write("EDIT", $"car with id '{id}' has been updated");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"'edit {argument}' horsepower, price and seat can not less than 1, " +
                                          $"mileage can not be less than 0. Car status should " +
                                          $"only be 'new' or 'used'. See '--help edit'");
                        Logger.LogError($"'{userInput}' wrong parameters for editing cars data");
                    }
                }
                else
                {
                    Console.WriteLine($"'edit {argument}' invalid numeric arguments. See '--help edit'");
                    Logger.LogError($"'edit {argument}' invalid numeric arguments");
                }
                
            }
            else
            {
                Console.WriteLine($"'edit {argument}' invalid arguments. See '--help edit'");
                Logger.LogError($"'edit {argument}' invalid arguments");
            }
        }
        else
        {
            Console.WriteLine($"'{userInput}' incorrect arguments. See '--help edit'");
            Logger.LogError($"'{userInput}' incorrect arguments (EDIT)");
        }
    }

    public static void Delete(string argument)
    {
        if (!Parser.IsValidNumberToParse(argument))
        {
            Console.WriteLine($"'delete {argument}' is not correct command. See '--help delete'");
            Logger.LogError($"'delete {argument}' is not correct command");
            return;
        }

        int id = Parser.ParseNumber(argument);
        if (id < 1 || id > CarRepository.NextId - 1)
        {
            Console.WriteLine($"'delete {id}' invalid id. See '--help delete'");
            Logger.LogError($"'delete {id}' invalid id");
        }
        var car = CarRepository.Cars.FirstOrDefault(car => car.GetId() == id);

        if (car == null)
        {
            Console.WriteLine($"there is no car with id '{id}'");
            Logger.LogError($"'there is no car with id '{id}'");
            return;
        }
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"you are about to delete this car:");
        Console.ResetColor();
        car.ShowInfoForDelete();
        Console.WriteLine("are you sure you want to delete this car?");
        Console.WriteLine("press 'Y' to confirm OR type anything to decline");
        Logger.Write("DELETE", "waiting for user's confirmation");

        string userInput = Console.ReadLine() ?? "";
        Logger.Write("USER INPUT", userInput);
        
        if (Parser.ParseChoiceIsYes(userInput))
        {
            Logger.Write("DELETE", $"car with ID {id} was deleted ({car.GetCompany()} {car.GetModel()})");
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
        Logger.Write("EXIT", "waiting for user's confirmation to EXIT the app");
        string userInput = Console.ReadLine() ?? "";
        Logger.Write("USER INPUT", userInput);
        

        if (Parser.ParseChoiceIsYes(userInput))
        {
            Logger.Write("EXIT", "app closed");
            Environment.Exit(0);
        }
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

                if (hp > 0 && price > 0 && seat > 0 && mileage >= 0)
                {
                    var car = factory.Create(arguments, CarRepository.NextId);
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"new car with id: {CarRepository.NextId} has been added successfully");
                    Logger.Write("ADD", $"new car with ID {CarRepository.NextId} was added successfully " +
                                        $"({arguments[0]} {arguments[1]})");
                    Console.ResetColor();
                    
                    CarRepository.Add(car);
                }
                else
                {
                    Console.WriteLine($"'add {argument}' horsepower, price and seat can not be less than 1, " +
                                      $"mileage can not be less than 0. See '--help add'");
                    Logger.LogError($"'add {argument}' incorrect numeric input");
                }
            }
            else
            {
                Console.WriteLine($"'add {argument}' invalid numeric arguments. See '--help add'");
                Logger.LogError($"'add {argument}' invalid numeric input");
            }
        }
        else
        {
            Console.WriteLine($"'add {argument}' invalid arguments. See '--help add'");
            Logger.LogError($"'add {argument}' invalid arguments");
        }
    }

    public static void Help()
    {
        Logger.Write("HELP", "displayed '--help' menu");
        Console.WriteLine("display all the cars or 1 particular car");
        Console.WriteLine("\tshow all\tShows the list of all cars with detailed information");
        Console.WriteLine("\tshow [id]\tShow the details about 1 car with the particular 'id'");
        Console.WriteLine();
        
        Console.WriteLine("add the car to the list");
        Console.WriteLine("\tadd [company], [model], [engine], [horse power], [price], [fuel type], [seat], [car status], [mileage]." +
                          " Add car to the list by passing correct arguments (all the arguments must be separated with ','." +
                          " Do not include '[' or ']' symbols - they are shown for easier demonstration purposes. For the [horse power]," +
                          " [price], [seat], and [mileage] arguments only pass numbers and they should not be negative. For the" +
                          " [car status] argument only pass 'new' or 'used' argument");
        Console.WriteLine();
        
        Console.WriteLine("edit the existing car's information");
        Console.WriteLine("\tedit [id]\tPass only [id] argument to be able to edit all of the particular's car information");
        Console.WriteLine("\tedit [id] [argument]\tHere in argument you can edit the particular argument of the particular's car." +
                          "For example: edit [3] [model] ");
        Console.WriteLine();
        
        Console.WriteLine("delete the existing car's information");
        Console.WriteLine("\tdelete [id]\tPass only [id] argument to be able to delete all of the particular's car information");
        Console.WriteLine();
        
        Console.WriteLine("clear the console");
        Console.WriteLine("\tclear\tType this command to clear the console");
        Console.WriteLine();
        
        Console.WriteLine("exit the application");
        Console.WriteLine("\texit\tType this command in order to quit the application");
        Console.WriteLine();
        
        Console.WriteLine("saving the car list");
        Console.WriteLine($"\tsave\tSave the current car list state into the initial file that you are working from - {Program.path}");
        Console.WriteLine("\tsaveAs\tSave the car list state into the path and file format that you choose yourself.");
        Console.WriteLine();
    }

    public static void Help(string command)
    {
        command = command.ToLower();
        switch (command)
        {
            case "show":
                Logger.Write("HELP", $"displayed '--help {command}' menu");
                Console.WriteLine("display all the cars or 1 particular car");
                Console.WriteLine("\tshow all\tShows the list of all cars with detailed information");
                Console.WriteLine("\tshow [id]\tShow the details about 1 car with the particular 'id'");
                break;
            
            case "add":
                Logger.Write("HELP", $"displayed '--help {command}' menu");
                Console.WriteLine("add the car to the list");
                Console.WriteLine("\tadd [company], [model], [engine], [horse power], [price], [fuel type], [seat], [car status], [mileage]." +
                                  " Add car to the list by passing correct arguments (all the arguments must be separated with ','." +
                                  " Do not include '[' or ']' symbols - they are shown for easier demonstration purposes. For the [horse power]," +
                                  " [price], [seat], and [mileage] arguments only pass numbers and they should not be negative. For the" +
                                  " [car status] argument only pass 'new' or 'used' argument");
                break;
            
            case "edit":
                Logger.Write("HELP", $"displayed '--help {command}' menu");
                Console.WriteLine("edit the existing car's information");
                Console.WriteLine("\tedit [id]\tPass only [id] argument to be able to edit all of the particular's car information. " +
                                  "Id can't be lower than 1 or higher than ID of the last car on the list");
                Console.WriteLine("\tedit [id] [argument]\tHere in argument you can edit the particular argument of the particular's car." +
                                  "For example: edit [3] [model] ");
                break;
            
            case "delete":
                Logger.Write("HELP", $"displayed '--help {command}' menu");
                Console.WriteLine("delete the existing car's information");
                Console.WriteLine("\tdelete [id]\tPass only [id] argument to be able to delete all of the particular's car information");
                break;
            
            case "clear":
                Logger.Write("HELP", $"displayed '--help {command}' menu");
                Console.WriteLine("clear the console");
                Console.WriteLine("\tclear\tType this command to clear the console");
                break;
            
            case "exit":
                Logger.Write("HELP", $"displayed '--help {command}' menu");
                Console.WriteLine("exit the application");
                Console.WriteLine("\texit\tType this command in order to quit the application");
                break;
            
            case "save":
                Logger.Write("HELP", $"displayed '--help {command}' menu");
                Console.WriteLine("saving the car list");
                Console.WriteLine($"\tsave\tSave the current car list state into the initial file that you are working from - {Program.path}");
                break;
            
            case "saveas":
                Logger.Write("HELP", $"displayed '--help {command}' menu");
                Console.WriteLine("saving the car list");
                Console.WriteLine("\tsaveAs\tSave the car list state into the path and file format that you choose yourself");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Note:");
                Console.ResetColor();
                Console.WriteLine(@"The path to the file should be absolute. For example - C:\Desktop\cars.csv");
                Console.WriteLine("Filenames should not end with empty spaces ' ' (Example - test .csv)");
                Console.WriteLine("Path should contain less than 260 characters");
                Console.WriteLine("Only '.csv' and '.txt' formats are allowed");
                Console.WriteLine("Folders or file names can not be reserved names, here is the list of reserved names:\n" +
                                  "\"CON\",\"PRN\",\"AUX\",\"NUL\",\"COM1\",\"COM2\",\"COM3\",\"COM4\",\"COM5\",\"COM6\",\"COM7\"," +
                                  "\"COM8\",\"COM9\",\"LPT1\",\"LPT2\",\"LPT3\",\"LPT4\",\"LPT5\",\"LPT6\",\"LPT7\",\"LPT8\",\"LPT9\"");
                
                break;
            default:
                Console.WriteLine($"'--help {command}' incorrect argument '{command}'");
                Logger.LogError($"'--help {command}' incorrect argument '{command}'");
                break;
        }
    }
}