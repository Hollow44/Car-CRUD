using CarCrudProject.Utilities;
using CarCrudProject.Models;
using CarCrudProject.Repositories;
using System.IO;

namespace CarCrudProject.Data;

public static class FileSaver
{
    private static string currentPath = @"..\..\..\..\CarCrudProject/";
    private static string defaultFileName = "output.csv";
    public static void SaveAs()
    {
        Console.WriteLine(@"enter the path and filename (for example: C:\Desktop\cars.csv)");
        Console.WriteLine("or press 'Enter', to save it in output.csv in current folder:");
        Console.WriteLine(Path.GetFullPath(currentPath));

        string userInput = Console.ReadLine() ?? "";

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
            }
        }
    }
    
    public static void Save()
    {
        using (var writer = new StreamWriter(Program.path, append: false))
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
    }
}