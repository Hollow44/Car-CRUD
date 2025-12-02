using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using CarCrudProject.Models;

namespace CarCrudProject.Repositories;

public static class CarRepository
{
    public static List<Car> Cars = new List<Car>();
    private static int _nextId = 1;

    public static void Add(Car car)
    {
        Cars.Add(car);
        _nextId++;
    }

    public static int NextId => _nextId;
}

