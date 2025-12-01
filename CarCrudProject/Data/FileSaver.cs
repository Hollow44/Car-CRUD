using CarCrudProject.Utilities;
using CarCrudProject.Models;
using CarCrudProject.Repositories;

namespace CarCrudProject.Data;

public static class FileSaver
{
    
    public static void SaveAs()
    {
        Console.WriteLine(@"enter the path and filename (for example: C:\Desktop\cars.csv)");
        Console.WriteLine("or press 'Enter', to save it in output.csv in current folder:");
        Console.WriteLine(Program.path);

        string userInput = Console.ReadLine() ?? "";
        
        
    }
    
    public static void Save()
    {
        string path = Program.path;
        int numberOfCars = CarRepository.Cars.Count;
        string[] file = new string[numberOfCars + 1];
        file[0] = "company,model,engine,horse power,price,fuel type, number of seats, status of the car,mileage";
        for (int i = 0, j = 1; i < numberOfCars; i++)
        {
            if (CarRepository.Cars[i].Equals(null)) continue;
            file[j] += $"{CarRepository.Cars[i].GetCompany()}," +
                       $"{CarRepository.Cars[i].GetModel()}," +
                       $"{CarRepository.Cars[i].GetEngine()}," +
                       $"{CarRepository.Cars[i].GetHorsePower()}," +
                       $"{CarRepository.Cars[i].GetPrice()}," +
                       $"{CarRepository.Cars[i].GetFuelType()}," +
                       $"{CarRepository.Cars[i].GetSeat()}," +
                       $"{CarRepository.Cars[i].GetIsUsed()}," +
                       $"{CarRepository.Cars[i].GetMileage()}\n";
        }
        File.WriteAllLines(path, file);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("saved successfully");
        Console.ResetColor();
    }
}