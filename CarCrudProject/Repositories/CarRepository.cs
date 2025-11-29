using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using CarCrudProject.Models;

namespace CarCrudProject.Repositories;

public class CarRepository
{
    public List<Car> Cars = new List<Car>();
    private int _nextId = 1;

    public void Add(Car car)
    {
        Cars.Add(car);
        _nextId++;
    }

    public int NextId => _nextId;
}

