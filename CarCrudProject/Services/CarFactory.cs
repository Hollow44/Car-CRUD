using CarCrudProject.Models;
using CarCrudProject.Utilities;

namespace CarCrudProject.Services;

public class CarFactory
{
    public Car Create(string[] data, int id)
    {
        string company = data[0];
        string model = data[1];
        string engine = data[2];
                
        //horsepower
        string horsePowerStr = data[3];
        int horsePower = 0;
        if (Parser.IsValidNumberToParse(horsePowerStr)) horsePower = Parser.ParseNumber(horsePowerStr);

        // price
        string priceStr = data[4];
        int price = 0;
        if (Parser.IsValidNumberToParse(priceStr)) price = Parser.ParseNumber(priceStr);
            
        // fuel type
        string fuelType = data[5];
            
        // seat quantity
        string seatStr = data[6];
        int seat = 0;
        if (Parser.IsValidNumberToParse(seatStr)) seat = Parser.ParseNumber(seatStr);

        // new or used
        bool isUsed = data[7].ToLower() == "true";
            
        // mileage
        string mileageStr = data[8];
        int mileage = 0;
        if (Parser.IsValidNumberToParse(mileageStr)) mileage = Parser.ParseNumber(mileageStr);    

        return new Car(id, company, model, engine,horsePower, price, fuelType, seat, isUsed, mileage);
    }
}