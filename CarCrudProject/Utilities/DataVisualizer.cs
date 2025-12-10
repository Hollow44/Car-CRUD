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
    
    private static char[][] graph = new char[22][];
    private static int[] graphVerticalCoords = new int[12];
    private static int[] graphHorizontalCoords = new int[12];
    private static char graphPoint = '*';
    
    private static char[][] histogram = new char[23][];
    private static int[] histogramVerticalCoords = new int[12]; 
    private static int[] histogramHorizontalCoords = new int[12];
    

    public static double FormatNumber(int num)
    {
        return num / 1_000_000.0;
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

    private static int FindMostExpensiveCardSold()
    {
        int highestPrice = 0;
        int highestPriceCarId = 0;

        foreach (var transaction in TransactionRepository.Transactions)
        {
            int id = transaction.GetCarId();
            var car = CarRepository.Get(id);
            if (car == null) continue;
            
            if (car.GetPrice() > highestPrice)
            {
                highestPrice = car.GetPrice();
                highestPriceCarId = id;
            }
        }
        return highestPriceCarId;
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
        
        Console.WriteLine(line);
        Console.WriteLine($"Total revenue: {totalRevenue.ToString("N0",culture)}$");
        Console.WriteLine($"Total cars sold: {totalCarsSold}");
    }

    private static int[] CalculateEachMonthsCarsSales()
    {
        int[] carSales = new int[12];
        for (int i = 0; i < 12; i++)
        {
            int totalCarCales = 0;
            foreach (var transaction in Statistics.Analytcis)
            {
                if (transaction.Value.month == i + 1)
                {
                    totalCarCales += transaction.Value.carsSold;
                }
            }

            carSales[i] = totalCarCales;
        }

        return carSales;
    }
    public static double[] CalculateEachMonthsRevenue()
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
        double[] monthsRevenueDouble = new double[12];

        for (int i = 0; i < revenue.Length; i++)
        {
            monthsRevenueDouble[i] = FormatNumber(revenue[i]);
        }
        return monthsRevenueDouble;
    }

    private static void SetupGraph(double[] revenue)
    {
        for (int i = 0; i < graph.Length; i++) graph[i] = new char[144];
        
        double max = revenue.Max();
        double min = revenue.Min();
        
        for (int i = 1, j = 0; i < 144; i+=12, j++)
        {
            int vertical = NormalizeNumber(revenue[j], max, min);
            graphVerticalCoords[j] = vertical;
            graphHorizontalCoords[j] = i;
            graph[vertical][i] = graphPoint;   
        }

        for (int i = 0; i < graph.Length; i++)
        {
            for (int j = 0; j < graph[i].Length; j++)
            {
                if (graph[i][j] == graphPoint) continue;
                graph[i][j] = ' ';
            }
        }

        for (int i = 0; i < graphHorizontalCoords.Length; i++)
        {
            int y = graphVerticalCoords[i];
            int x = graphHorizontalCoords[i];
            while (y >= 0)
            {
                if (graph[y][x] == graphPoint)
                {
                    y--;
                    continue;
                }
                graph[y][x] = '|';
                y--;
            }
        }
        
        // тут соединяются точки между месяцами *----*
        for (int i = 0; i < 11; i++)
        {
            ConnectLine(graphHorizontalCoords[i],graphVerticalCoords[i],graphHorizontalCoords[i+1],graphVerticalCoords[i+1]);
        }
    }

    public static void DrawYear()
    {
        var culture = new CultureInfo("en-US");
        culture.NumberFormat.NumberGroupSeparator = ".";
        int highestPriceCarId = FindMostExpensiveCardSold();
        var mostExpensiveCar = CarRepository.Get(highestPriceCarId);
        Console.WriteLine($"Most expensive model: {mostExpensiveCar.GetCompany()} " +
                          $"{mostExpensiveCar.GetModel()} - {mostExpensiveCar.GetPrice().ToString("N0",culture)}$");
        
        Console.WriteLine();
        Console.WriteLine($"Total revenue: {CalculateTotalRevenue().ToString("N0")}$");
        Console.WriteLine();
        // 1 stroka = 144 simvola
        double[] eachMonthRevenue = CalculateEachMonthsRevenue();
        
        SetupGraph(eachMonthRevenue);
        
        eachMonthRevenue.Sort();
        Array.Reverse(eachMonthRevenue);
        
        graphVerticalCoords.Sort();
        Array.Reverse(graphVerticalCoords);

        var numbersLeftSide = new Dictionary<int, double>();
        
        for (int i = 0; i < graphVerticalCoords.Length; i++)
        {
            numbersLeftSide[graphVerticalCoords[i]] = eachMonthRevenue[i];
        }

        DrawGraph(numbersLeftSide);
        
        Console.WriteLine();
        Console.WriteLine();
        
        DrawHistogram();
    }

    private static void DrawHistogram()
    {
        Console.WriteLine($"Total cars sold: {CalculateTotalCars()}");
        Console.WriteLine();
        
        int[] eachMonthsSales = CalculateEachMonthsCarsSales();
        
        SetupHistogram(eachMonthsSales);
        
        string bottomLine = new string('_', 93);
        string spaceBetweenMonths = new string(' ', 5);
        
        for (int i = histogram.Length - 1; i >= 0; i--)
        {
            Console.WriteLine($"       | {new string(histogram[i])}");
        }
        
        Console.WriteLine("       |" + bottomLine);
        Console.WriteLine($"         JAN{spaceBetweenMonths}FEB{spaceBetweenMonths}MAR{spaceBetweenMonths}" +
                          $"APR{spaceBetweenMonths}MAY{spaceBetweenMonths}JUN{spaceBetweenMonths}" +
                          $"JUL{spaceBetweenMonths}AUG{spaceBetweenMonths}SEP{spaceBetweenMonths}" +
                          $"OCT{spaceBetweenMonths}NOV{spaceBetweenMonths}DEC");
    }

    private static void SetupHistogram(int[] carSalesByMonth)
    {
        for (int i = 0; i < histogram.Length; i++) histogram[i] = new char[99];
        
        double max = carSalesByMonth.Max() + 0.0;
        double min = carSalesByMonth.Min() + 0.0;
        
        for (int i = 1, j = 0; i < 97; i+=8, j++)
        {
            int vertical = NormalizeNumber((double) carSalesByMonth[j] + 0.0, max, min);
            histogramVerticalCoords[j] = vertical;
            histogramHorizontalCoords[j] = i;
            histogram[vertical][i] = BarChart;   
        }

        for (int i = 0; i < histogram.Length; i++)
        {
            for (int j = 0; j < histogram[i].Length; j++)
            {
                if (histogram[i][j] == BarChart) continue;
                histogram[i][j] = ' ';
            }
        }
        
        for (int i = 0; i < histogramHorizontalCoords.Length; i++)
        {
            int y = histogramVerticalCoords[i];
            int x = histogramHorizontalCoords[i];

            char[] monthRevenueChar = carSalesByMonth[i].ToString().ToCharArray();
            int xD = x;
            
            for (int j = 0; j < monthRevenueChar.Length; j++)
            {
                if (monthRevenueChar.Length > 2)
                {
                    histogram[y + 1][xD - 1] = monthRevenueChar[j];
                    xD++;
                }
                else
                {
                    histogram[y + 1][xD] = monthRevenueChar[j];
                    xD++;
                }
            }
            
            while (y >= 0)
            {
                histogram[y][x - 1] = BarChart;
                histogram[y][x] = BarChart;
                histogram[y][x + 1] = BarChart;
                y--;
            }
        }
    }
    
    private static void DrawGraph(Dictionary<int, double> normalizedRevenueNumbers)
    {
        string bottomLine = "" + new string('_', 135);
        string spaceBetweenMonths = new string(' ', 9);
        
        for (int i = graph.Length - 1, j = graph.Length - 1; i >= 0; i--)
        {
            if (i == j)
            {
                if (!normalizedRevenueNumbers.ContainsKey(j))
                {
                    j--;
                    Console.WriteLine($"       |{new string(graph[i])}");
                    continue;
                }
                if (normalizedRevenueNumbers[j] < 10.0)
                {
                    Console.WriteLine($" {normalizedRevenueNumbers[j]:F1} M |{new string(graph[i])}");
                    j--;
                }
                else
                {
                    Console.WriteLine($"{normalizedRevenueNumbers[j]:F1} M |{new string(graph[i])}");
                    j--;
                }
            }
            
        }
        
        Console.WriteLine("       |" + bottomLine);
        Console.WriteLine($"        JAN{spaceBetweenMonths}FEB{spaceBetweenMonths}MAR{spaceBetweenMonths}" +
                          $"APR{spaceBetweenMonths}MAY{spaceBetweenMonths}JUN{spaceBetweenMonths}" +
                          $"JUL{spaceBetweenMonths}AUG{spaceBetweenMonths}SEP{spaceBetweenMonths}" +
                          $"OCT{spaceBetweenMonths}NOV{spaceBetweenMonths}DEC");
    }

    public static void ConnectLine(int x1, int y1, int x2, int y2)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;
        
        for (int i = x1 + 1; i < x2; i++)
        {
            double t = (double)(i - x1) / (x2 - x1);
            double y = y1 + (y2 - y1) * t;
            int yi = (int)Math.Round(y);
            
            yi = Math.Clamp(yi, 0, graph.Length - 1);
            graph[yi][i] = GetSymbol(dx, dy);
        }
    }

    public static char GetSymbol(int dx, int dy)
    {
        if (dy == 0) return '-';
        if (dx == 0) return '|';
        return '-';
    }
    public static int NormalizeNumber(double num, double max, double min)
    {
        if (max == min) return 0;

        double t = (num - min) / (max - min);
        int normalizedNum = (int)Math.Round( t * (graph.Length - 1) );
        return Math.Clamp(normalizedNum,0,graph.Length - 1);
    }
}