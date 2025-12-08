using System.Globalization;
using CarCrudProject.Repositories;
using CarCrudProject.Services;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CarCrudProject.Utilities;

public static class DataVisualizer
{
    private static char BarChart = '█';
    private static readonly int WindowWidth = 120;
    public static readonly int MaxNumbersInfo = 21; // 99.999.999$ (99 cars)

    private static char graphPoint = '*';

    public static string FormatNumber(double num)
    {
        var culture = new CultureInfo("en-US");
        culture.NumberFormat.NumberGroupSeparator = ".";
        if (num >= 1_000_000_000)
        {
            return (num / 1_000_000_000).ToString("0.#", culture) + "B";
        }
        if (num >= 1_000_000)
        {
            return (num / 1_000_000).ToString("0.#", culture) + "M";
        }
        if (num >= 1000)
        {
            return (num / 1000).ToString("0.#", culture) + "K";
        }
        return num.ToString("0");
    }

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

    public static int CalculateTotalCars()
    {
        int totalCars = 0;
        
        totalCars = TransactionRepository.Transactions.Count;

        return totalCars;
    }
    
    public static int CalculateTotalRevenue()
    {
        int totalRevenue = 0;
        
        foreach (var stat in Statistics.Analytcis)
        {
            totalRevenue += stat.Value.revenue;
        }
        
        return totalRevenue;
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

    public static int[] CalculateEachMonthsRevenue()
    {
        int[] revenue = new int[12];
        for (int i = 0, j = 1; i < 12; i++, j++)
        {
            int monthRevenue = 0;
            foreach(var month in Statistics.Analytcis)
            {
                if (month.Value.month == j)
                {
                    monthRevenue += month.Value.revenue;
                }
            }
            revenue[i] = monthRevenue;
        }
        return revenue;
    }

    public static void DrawYear()
    {
        // 1 stroka 144 simvolov

        char[][] graph = new char[34][];

        //graph 
        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = new char[144];
        }

         for (int i = 1; i < 144; i+=12)
        {
            graph[33][i] = graphPoint;   
        }

        for (int i = 0; i < graph.Length; i++)
        {
            for (int j = 0; j < graph[i].Length; j++)
            {
                if (graph[i][j] == graphPoint) continue;
                else graph[i][j] = '-';
            }
        }
        
        string bottomLine = "" + new string('_', 144);
        string spaceBetweenMonths = new string(' ', 9);

        string tab = "     ";
        for (int i = 0; i < graph.Length; i++)
        {
            Console.WriteLine(graph[i]);
        }
        
        Console.WriteLine(bottomLine);
        Console.WriteLine($"JAN{spaceBetweenMonths}FEB{spaceBetweenMonths}MAR{spaceBetweenMonths}" +
                          $"APR{spaceBetweenMonths}MAY{spaceBetweenMonths}JUN{spaceBetweenMonths}" +
                          $"JUL{spaceBetweenMonths}AUG{spaceBetweenMonths}SEP{spaceBetweenMonths}" +
                          $"OCT{spaceBetweenMonths}NOV{spaceBetweenMonths}DEC");
        int totalCarsSold = CalculateTotalCars();
        int totalRevenue = CalculateTotalRevenue();
        Console.WriteLine($"Total revenue: {totalRevenue.ToString("N0")}$");
        Console.WriteLine($"Total cars sold: {totalCarsSold}");

        int[] eachMonthRevenue = CalculateEachMonthsRevenue();
        foreach(var month in eachMonthRevenue)
        {
            Console.WriteLine(FormatNumber(month));
        }
    }
}