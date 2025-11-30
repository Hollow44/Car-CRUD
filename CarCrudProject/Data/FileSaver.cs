using CarCrudProject.Utilities;
using CarCrudProject.Models;

namespace CarCrudProject.Data;

public static class FileSaver
{
    public static void SaveAs(string path)
    {
        if (FileName.IsValidFileName(fileName) && !FileName.IsReservedName(fileName))
        {
            int numberOfCars = CarList.Cars.Count;
            string[] file = new string[numberOfCars + 1];
            file[0] = "id,company,model,engine,horse power,price,fuel type, number of seats, status of the car,mileage";
            for (int i = 0, j = 1; i < numberOfCars; i++)
            {
                file[j] += $"{CarList.Cars[i].GetId()}," +
                           $"{CarList.Cars[i].GetCompany()}," +
                           $"{CarList.Cars[i].GetModel()}," +
                           $"{CarList.Cars[i].GetEngine()}," +
                           $"{CarList.Cars[i].GetHorsePower()}," +
                           $"{CarList.Cars[i].GetPrice()}," +
                           $"{CarList.Cars[i].GetFuelType()}," +
                           $"{CarList.Cars[i].GetSeat()}," +
                           $"{CarList.Cars[i].GetIsUsed()}," +
                           $"{CarList.Cars[i].GetMileage()}\n";
            }
            File.WriteLines(path, file);
        }
    }
    
    public static void Save()
    {
        string path = @"C:\Users\TimZhu\source\repos\CarCrudProject\CarCrudProject\cars_dataset_modifiedExtra.csv";
        int numberOfCars = CarList.Cars.Count;
        string[] file = new string[numberOfCars + 1];
        file[0] = "company,model,engine,horse power,price,fuel type, number of seats, status of the car,mileage";
        for (int i = 0, j = 1; i < numberOfCars; i++)
        {
            file[j] += $"{CarList.Cars[i].GetCompany()}," +
                       $"{CarList.Cars[i].GetModel()}," +
                       $"{CarList.Cars[i].GetEngine()}," +
                       $"{CarList.Cars[i].GetHorsePower()}," +
                       $"{CarList.Cars[i].GetPrice()}," +
                       $"{CarList.Cars[i].GetFuelType()}," +
                       $"{CarList.Cars[i].GetSeat()}," +
                       $"{CarList.Cars[i].GetIsUsed()}," +
                       $"{CarList.Cars[i].GetMileage()}\n";
        }
        File.WriteAllLines(path, file);
    }
}