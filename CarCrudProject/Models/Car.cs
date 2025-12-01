using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarCrudProject.Models
{
    public class Car
    {
        private int Id;
        private string Company;
        private string Model;
        private string Engine;
        private int HorsePower;
        private int Price;
        private string FuelType;
        private int Seat;
        private bool IsUsed;
        private int Mileage;

        public int GetId()
        {
            return this.Id;
        }

        public string GetCompany()
        {
            return this.Company;
        }

        public void SetCompany(string company)
        {
            this.Company = company;
        }
        public string GetModel()
        {
            return this.Model;
        }
        public void SetModel(string model)
        {
            this.Model = model;
        }
        public string GetEngine()
        {
            return this.Engine;
        }
        public void SetEngine(string engine)
        {
            this.Engine = engine;
        }
        public int GetHorsePower()
        {
            return this.HorsePower;
        }
        public void SetHorsePower(int hp)
        {
            this.HorsePower = hp;
        }
        public int GetPrice()
        {
            return this.Price;
        }
        public void SetPrice(int price)
        {
            this.Price = price;
        }

        public string GetFuelType()
        {
            return this.FuelType;
        }
        public void SetFuelType(string fuelType)
        {
            this.FuelType = fuelType;
        }

        public int GetSeat()
        {
            return this.Seat;
        }
        public void SetSeat(int seat)
        {
            this.Seat = seat;
        }

        public bool GetIsUsed()
        {
            return this.IsUsed;
        }
        public void SetIsUsed(bool isUsed)
        {
            this.IsUsed = isUsed;
        }

        public int GetMileage()
        {
            return this.Mileage;
        }
        public void SetMileage(int mileage)
        {
            this.Mileage = mileage;
        }


        public Car(int id, string company, string model, string engine, int hp, 
                   int price, string fuelType, int seat, bool isUsed, int mileage)
        {
            this.Id = id;
            this.Company = company;
            this.Model = model;
            this.Engine = engine;
            this.HorsePower = hp;
            this.Price = price;
            this.FuelType = fuelType;
            this.Seat = seat;
            this.IsUsed = isUsed;
            this.Mileage = mileage;
        }
        
        public void ShowInfo()
        {
            Console.WriteLine("_______________________________________________");
            Console.WriteLine($"ID: {Id}");
            Console.Write($"{Company}, Model: {Model}.\nIt has: {FuelType} fuel type, {Engine} engine, " +
                          $"{HorsePower} HP, {Seat} seats.\n");
            if (IsUsed) Console.Write($"Car status: used, mileage: {Mileage} km.\n");
            else Console.Write("This car is brand new!\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Price: {Price} $");
            Console.ResetColor();
        }

        public void ShowInfoForEdit()
        {
            Console.WriteLine($"COMPANY: [{this.Company}], MODEL: [{this.Model}], ENGINE: [{this.Engine}], HORSEPOWER: [{this.HorsePower}], PRICE: [{this.Price}], " +
                              $"FUEL TYPE: [{this.FuelType}], NUMBER OF SEATS: [{this.Seat}], CAR IS USED: [{this.IsUsed}], MILEAGE: [{this.Mileage}]");
        }

        public void ShowInfoForDelete()
        {
            Console.WriteLine($"ID: {this.GetId()}, {this.Company} {this.Model}, ENGINE: {this.Engine}, {this.HorsePower} HP, PRICE: {this.Price}$, " +
                              $"FUEL TYPE: {this.FuelType}, NUMBER OF SEATS: {this.Seat}, CAR IS USED: {this.IsUsed}, MILEAGE: {this.Mileage} KM");
        }
    }
}
