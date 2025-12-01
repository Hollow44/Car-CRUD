using CarCrudProject.Utilities;
using CarCrudProject.Models;
using CarCrudProject.Repositories;

namespace CarCrudProject.Data;

public static class FileSaver
{
    
    public static void SaveAs(string path)
    {
        //if (FileName.IsValidFileName(fileName) && !FileName.IsReservedName(fileName))
        //{
        //    int numberOfCars = CarList.Cars.Count;
        //    string[] file = new string[numberOfCars + 1];
        //    file[0] = "id,company,model,engine,horse power,price,fuel type, number of seats, status of the car,mileage";
        //    for (int i = 0, j = 1; i < numberOfCars; i++)
        //    {
        //        file[j] += $"{CarList.Cars[i].GetId()}," +
        //                   $"{CarList.Cars[i].GetCompany()}," +
        //                   $"{CarList.Cars[i].GetModel()}," +
        //                   $"{CarList.Cars[i].GetEngine()}," +
        //                   $"{CarList.Cars[i].GetHorsePower()}," +
        //                   $"{CarList.Cars[i].GetPrice()}," +
        //                   $"{CarList.Cars[i].GetFuelType()}," +
        //                   $"{CarList.Cars[i].GetSeat()}," +
        //                   $"{CarList.Cars[i].GetIsUsed()}," +
        //                   $"{CarList.Cars[i].GetMileage()}\n";
        //    }
        //    File.WriteLines(path, file);
        //}
    }
    
    public static void Save()
    {
        string path = Program.path;
        int numberOfCars = CarRepository.Cars.Count;
        string[] file = new string[numberOfCars + 1];
        file[0] = "company,model,engine,horse power,price,fuel type, number of seats, status of the car,mileage";
        for (int i = 0, j = 1; i < numberOfCars; i++)
        {
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