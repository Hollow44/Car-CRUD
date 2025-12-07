using CarCrudProject.Models;

namespace CarCrudProject.Repositories;

public static class Statistics
{
    public static readonly Dictionary<string, int> Months = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        {"january", 1},
        {"february", 2},
        {"march", 3},
        {"april", 4},
        {"may", 5},
        {"june", 6},
        {"july", 7},
        {"august", 8},
        {"september", 9},
        {"october", 10},
        {"november", 11},
        {"december", 12},
    };
    
    // string = car brand
    public static Dictionary<string, (int month, int revenue, int carsSold)> Analytcis = new();

    public static void AddTransaction(string brand, int month, int price)
    {
        brand = brand.ToUpper();
        if (!Analytcis.ContainsKey(brand))
        {
            Analytcis[brand] = (month, 0, 0);
        }

        var current = Analytcis[brand];
        Analytcis[brand] = (month, current.revenue + price, current.carsSold + 1);
    }
}