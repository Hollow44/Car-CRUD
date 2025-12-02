using CarCrudProject.Utilities;
using CarCrudProject.Models;
using CarCrudProject.Repositories;
using System.IO;
using CarCrudProject.Services;

namespace CarCrudProject.Data;

public static class FileSaver
{
    private static string currentPath = @"..\..\..\..\CarCrudProject\outputs/";
    private static string defaultFileName = "output.csv";
    public static void SaveAs()
    {
        Console.WriteLine(@"enter the path and filename (for example: C:\Desktop\cars.csv)");
        Console.WriteLine("or press 'Enter', to save it in output.csv in current folder:");
        Console.WriteLine(Path.GetFullPath(currentPath));
        Logger.Write("SAVE AS", "waiting for user to choose saving method");

        string userInput = Console.ReadLine() ?? "";
        Logger.Write("USER INPUT", $"{userInput}");

        if (userInput == "")
        {
            string dir = Path.GetDirectoryName(Path.GetFullPath(currentPath + defaultFileName))!;
            Directory.CreateDirectory(dir);
            using (var writer = new StreamWriter(currentPath + defaultFileName, append: false))
            {
                writer.WriteLine("id,company,model,engine,horse power,price,fuel type,number of seats,status of the car,mileage");
                foreach (var car in CarRepository.Cars)
                {
                    writer.WriteLine(car.GetInfo());
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("file saved successfully:");
            Console.ResetColor();
            Console.WriteLine($"'{currentPath + defaultFileName}'");
            Logger.Write("SAVE AS",$"user chose default saving (entered empty string). File path - " +
                                   $"'{currentPath + defaultFileName}'");
        }
        else 
        {
            if (FileName.IsValidFilePath(userInput))
            {
                string dir = Path.GetDirectoryName(userInput)!;
                Directory.CreateDirectory(dir);
                
                using (var writer = new StreamWriter(userInput, append: false))
                {
                    writer.WriteLine("id,company,model,engine,horse power,price,fuel type,number of seats,status of the car,mileage");
                    foreach (var car in CarRepository.Cars)
                    {
                        writer.WriteLine(car.GetInfo());
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("file saved successfully:");
                Console.ResetColor();
                Console.WriteLine($"'{userInput}'");
                Logger.Write("SAVE AS",$"user chose manual saving. File path - " +
                                       $"'{currentPath + defaultFileName}'");
            }
        }
    }
    
    public static void Save()
    {
        using (var writer = new StreamWriter(Path.GetFullPath(Program.path), append: false))
        {
            writer.WriteLine("id,company,model,engine,horse power,price,fuel type,number of seats,status of the car,mileage");
            foreach (var car in CarRepository.Cars)
            {
                writer.WriteLine(car.GetInfo());
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("saved successfully");
        Console.ResetColor();
        Logger.Write("SAVE",$"file saved successfully. Path - {Path.GetFullPath(Program.path)}");
    }
}