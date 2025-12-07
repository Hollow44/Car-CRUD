using System.Runtime.InteropServices.JavaScript;
using CarCrudProject.Models;

namespace CarCrudProject.Services;

public class TransactionFactory
{
    public Transaction Create(Car car, int id)
    {
        int carId = car.GetId();

        Random random = new Random();
        DateTime date = new DateTime(2025, 1, 1);
        DateTime randomDate = date.AddDays(random.Next(365));
        
        return new Transaction(id, carId, randomDate);
    }
}