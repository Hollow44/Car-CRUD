using System.Globalization;
using CarCrudProject.Repositories;
using CarCrudProject.Services;
using System.Diagnostics;

namespace CarCrudProject.Utilities;

public static class DataVisualizer
{
    private static char BarChart = '█';
    private static readonly int WindowWidth = 120;
    public static readonly int MaxNumbersInfo = 21; // 99.999.999$ (99 cars)

    public static int CalculateLongestBrandName(string month)
    {
        int longestBrandName = 0;
        
        foreach (var brandName in Statistics.Analytcis.Where(
                     tr => tr.Value.month == Statistics.Months[month]))
        { 
            if (brandName.Key.Length > longestBrandName)
            {
                longestBrandName = brandName.Key.Length;
            }  
        }

        return longestBrandName;
    }

    public static int CalculateTotalCars(string timePeriod)
    {
        int totalCars = 0;
        
        if (timePeriod.Equals("year", StringComparison.OrdinalIgnoreCase)) 
            totalCars = TransactionRepository.Transactions.Count;
        
        else if (Statistics.Months.ContainsKey(timePeriod))
        {
            if (!Statistics.Analytcis.Any(tr => tr.Value.month == Statistics.Months[timePeriod]))
            {
                Console.WriteLine($"No cars were sold in {timePeriod.ToLower()}");
                Logger.Write("STATS",$"No cars were sold in {timePeriod.ToLower()}");
                return 0;
            }
            foreach (var pair in Statistics.Analytcis.Where(tr => tr.Value.month == Statistics.Months[timePeriod]))
            {
                totalCars += pair.Value.carsSold;
            }
        }

        return totalCars;
    }
    
    public static void DrawMonth(string month)
    {
        int longestBrandName = CalculateLongestBrandName(month);
        
        int longestChartLengthPossible = WindowWidth - (longestBrandName + 3 + MaxNumbersInfo); // LAND ROVER | ; 99.999.999 $ (99 cars) 
        
        var culture = new CultureInfo("en-US");
        culture.NumberFormat.NumberGroupSeparator = ".";
        
        if (!Statistics.Analytcis.Any(tr => tr.Value.month == Statistics.Months[month]))
        {
            Console.WriteLine($"No cars were sold in {month.ToLower()}");
            Logger.Write("STATS",$"No cars were sold in {month.ToLower()}");
            return;
        }
        int longestChart = Statistics.Analytcis
            .Where(tr => tr.Value.month == Statistics.Months[month])
            .Max(tr => tr.Value.revenue) / 100_000;
        
        string line = new string('-', longestChart > longestChartLengthPossible ? 
            longestChartLengthPossible + longestBrandName + MaxNumbersInfo + 3
            : longestChart + longestBrandName + 3 + MaxNumbersInfo);
        
        Console.WriteLine(line);
        Console.WriteLine($"{month.ToUpper()}:");
        Console.WriteLine(line);
        
        int totalRevenue = 0;
        int totalCarsSold = 0;
        var transactionsThisMonth = TransactionRepository.Transactions
                                    .Where(tr => tr.GetDate().Month == Statistics.Months[month]).ToArray();

        int highestPrice = 0;
        int highestPriceCarId = 0;
        
        for (int i = 0; i < transactionsThisMonth.Length; i++)
        {
            int id = transactionsThisMonth[i].GetCarId();
            var car = CarRepository.Get(id);
            if (car == null) continue;
            
            if (car.GetPrice() > highestPrice)
            {
                highestPrice = car.GetPrice();
                highestPriceCarId = id;
            }
            
        }
        
        foreach (var pair in Statistics.Analytcis.Where(tr => tr.Value.month == Statistics.Months[month])
                                                 .OrderByDescending(tr => tr.Value.revenue))
        {
            totalRevenue += pair.Value.revenue;
            totalCarsSold += pair.Value.carsSold;
            
            string brandName = pair.Key;

            if (brandName.Length < longestBrandName)
            {
                brandName += new string(' ', longestBrandName - brandName.Length);
            }
            
            string chart = new string(BarChart, (pair.Value.revenue / 100_000) <= 1 ? 1 : pair.Value.revenue / 100_000);

            if (chart.Length > longestChartLengthPossible)
            {
                chart = new string(BarChart, longestChartLengthPossible);
                longestChart = chart.Length;
            }

            if (chart.Length < longestChart)
            {
                chart += new string(' ', longestChart - chart.Length);
            }
            Console.WriteLine($"{brandName} | {chart} {pair.Value.revenue.ToString("N0", culture)}$ ({pair.Value.carsSold} cars)");
        }

        var mostExpensiveCar = CarRepository.Get(highestPriceCarId);
        Console.WriteLine(line);
        Console.WriteLine($"Total revenue: {totalRevenue.ToString("N0",culture)}$");
        Console.WriteLine($"Total cars sold: {totalCarsSold}");
        Console.WriteLine($"Most expensive model: {mostExpensiveCar.GetCompany()} " +
                          $"{mostExpensiveCar.GetModel()} - {mostExpensiveCar.GetPrice().ToString("N0",culture)}$");
        Console.WriteLine(line);
    }

    public static void DrawYear()
    {
        
    }
}