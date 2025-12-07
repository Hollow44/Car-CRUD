using CarCrudProject.Models;
using CarCrudProject.Utilities;
using CarCrudProject.Services;
using CarCrudProject.Repositories;

namespace CarCrudProject.Data;

public static class FileLoader
{
    private static readonly CarFactory carFactory = new();
    private static readonly TransactionFactory transactionFactory = new();
    
    public static void Load(string path)
    {
        Random random = new Random();
        
        foreach (var line in File.ReadLines(path).Skip(1))
        {
            int sell = random.Next(0, 3);
            
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            var columns = Parser.ParseCsv(line);
            if (columns.Length != 9) continue;
            
            var car = carFactory.Create(columns, CarRepository.NextId);
            CarRepository.Add(car);
           
            if (sell == 1)
            {
                car.SetIsSold(true);
                Transaction transaction = transactionFactory.Create(car, TransactionRepository.NextId);
                TransactionRepository.Add(transaction);
                Statistics.AddTransaction(car.GetCompany(),transaction.GetDate().Month,car.GetPrice());
            }
            else car.SetIsSold(false);
        }
        
        Logger.Write("LOAD",$"Dataset has been loaded ({CarRepository.Cars.Count} cars) " +
                     $"({TransactionRepository.Transactions.Count} cars sold)");
    }
}