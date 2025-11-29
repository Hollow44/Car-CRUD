using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Text;

namespace CarCrudProject.Models
{
    public class CarList
    {
        public static List<Car> Cars = new List<Car>();
        private static int nextId = 1;


        public static bool isVerifiedParsedNumber(string str)
        {
            double parseDouble;
            if (double.TryParse(str,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out parseDouble))
            {
                return true;
            }
            else return false;
        }
        public static void PrintChoice()
        {
            Console.Clear();
            Console.WriteLine("WHAT DO YOU WANT?                             BEATCH");
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("Print all the cars - Press '1'");
            Console.WriteLine("Add car to the list - Press '2'");
            Console.WriteLine("Edit car from the list - Press '3'");
            Console.WriteLine("Save as - Press '4'");
            Console.WriteLine("Save - Press '5'");
            Console.WriteLine("Delete - Press '6'");
            Console.WriteLine("Info - Press '7'");
            Console.WriteLine("Press 'Q' to exit the app.");
            Console.WriteLine();

            string userInput = Console.ReadLine();
            Controller(userInput);
        }

        public static bool AskUserToContinue()
        {
            Console.WriteLine("Do you want to continue?");
            Console.WriteLine("(Y) - to continue or (N) - to quit");
            Console.WriteLine();
            string userInput = Console.ReadLine();
            userInput = userInput.ToLower();

            if (UserChoiceValid(userInput)) return userInput == "y";
            else
            {
                Console.Clear();
                Console.WriteLine("Only 'N' and 'Y' are allowed!");
                Console.WriteLine();
                return AskUserToContinue();
            }
        }

        public static bool UserChoiceValid(string userInput)
        {
            if (userInput == "y" || userInput == "n") return true;
            else return false;
        
        }

        public static void Controller(string input)
        {
            input = input.ToLower();
            switch (input)
            {
                case "1":
                    foreach(Car car in Cars)
                    {
                        car.ShowInfo();
                    }
                    Console.WriteLine();
                    if (AskUserToContinue()) PrintChoice();
                    else Environment.Exit(0);
                    break;
                case "2":
                    Add();
                    break;
                case "3":
                    Edit();
                    break;
                case "4":
                    SaveAs();
                    break;
                case "5":
                    Save();
                    break;
                case "6":
                    Delete();
                    break;
                case "7":
                    Info();
                    break;
                case "q":
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Heeeeeeeeeeey, don't fuck around. Choose the correct option!!!");
                    Console.WriteLine();
                    if (AskUserToContinue()) PrintChoice();
                    else Environment.Exit(0);
                    break;
            }
        }

        public static void Info()
        {
            Console.Clear();
            Console.WriteLine("Choose category:");
            Console.WriteLine();
            Console.WriteLine("1.Companies  |  2.Models  |   3.Engines    |  4.Horse power  |  5.Price\n" +
                              "_____________|____________|________________|_________________|_______________\n" +
                              "             |            |                |                 |\n" +
                              "6.Fuel type  |  7.Seats   |  8.Car status  |  9.Mileage      |  10.Main menu");
            Console.WriteLine();

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                //1) Company brand grouping
                case "1":
                    Console.Clear();
                    var groupCompany = Cars.GroupBy(car => car.GetCompany())
                                       .Select(car => new
                                       {
                                           Company = car.Key,
                                           Count = car.Count()
                                       }).OrderByDescending(car => car.Count);

                    foreach (var car in groupCompany)
                    {
                        Console.WriteLine($"{car.Company}: {car.Count} cars");
                    }
                    Console.WriteLine();

                    if (AskUserToContinue()) Info();
                    else PrintChoice();
                    break;

                // 2) Models grouping
                case "2":
                    Console.Clear();
                    var groupModels = Cars.GroupBy(car => car.GetModel())
                                      .Select(car => new
                                      {
                                          Model = car.Key,
                                          Count = car.Count()
                                      }).OrderByDescending(car => car.Count);

                    foreach (var car in groupModels)
                    {
                        Console.WriteLine($"{car.Model}: {car.Count} car/s");
                    }
                    Console.WriteLine();

                    if (AskUserToContinue()) Info();
                    else PrintChoice();
                    break;

                // 3) engine grouping
                case "3":
                    Console.Clear();
                    var groupEngines = Cars.GroupBy(car => car.GetEngine())
                                      .Select(car => new
                                      {
                                          Engine = car.Key,
                                          Count = car.Count()
                                      }).OrderByDescending(car => car.Count);

                    foreach (var car in groupEngines)
                    {
                        Console.WriteLine($"{car.Engine}: {car.Count} engines");
                    }
                    Console.WriteLine();

                    if (AskUserToContinue()) Info();
                    else PrintChoice();
                    break;

                // 4) HP grouping
                case "4":
                    Console.Clear();
                    var groupHorsePower = Cars.OrderByDescending(car => car.GetHorsePower());

                    foreach (var car in groupHorsePower)
                    {
                        Console.Write($"ID: {car.GetId()}, {car.GetCompany()} {car.GetModel()} ");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write($"{car.GetHorsePower()} HP\n");
                        Console.ResetColor();
                    }
                    Console.WriteLine();

                    if (AskUserToContinue()) Info();
                    else PrintChoice();
                    break;

                // 5) price grouping
                case "5":
                    Console.Clear();
                    var groupPrice = Cars.OrderByDescending(car => car.GetPrice());

                    foreach (var car in groupPrice)
                    {
                        Console.Write($"ID: {car.GetId()}, {car.GetCompany()} {car.GetModel()} ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write($"{car.GetPrice()}$\n");
                        Console.ResetColor();
                    }
                    Console.WriteLine();

                    if (AskUserToContinue()) Info();
                    else PrintChoice();
                    break;

                // 6) fuel type grouping
                case "6":
                    Console.Clear();
                    var groupFuelTypes = Cars.GroupBy(car => car.GetFuelType())
                                      .Select(car => new
                                      {
                                          FuelType = car.Key,
                                          Count = car.Count()
                                      }).OrderByDescending(car => car.Count);

                    foreach (var car in groupFuelTypes)
                    {
                        Console.WriteLine($"{car.FuelType}: {car.Count} car\\s");
                    }
                    Console.WriteLine();

                    if (AskUserToContinue()) Info();
                    else PrintChoice();
                    break;

                // 7) seats grouping
                case "7":
                    Console.Clear();
                    var groupSeats = Cars.GroupBy(car => car.GetSeat())
                                      .Select(car => new
                                      {
                                          Seats = car.Key,
                                          Count = car.Count()
                                      }).OrderByDescending(car => car.Count);

                    foreach (var car in groupSeats)
                    {
                        Console.Write($"Cars with {car.Seats} seat/s: ");
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write($"{car.Count}\n");
                        Console.ResetColor();
                    }
                    Console.WriteLine();

                    if (AskUserToContinue()) Info();
                    else PrintChoice();
                    break;

                //8) status grouping
                case "8":
                    Console.Clear();

                    int usedCars = 0;
                    int newCars = 0;

                    foreach (var car in Cars)
                    {
                        if (car.GetIsUsed()) usedCars++;
                        else if (!car.GetIsUsed()) newCars++;
                    }

                    Console.WriteLine($"Used cars: {usedCars}");
                    Console.WriteLine($"New cars: {newCars}");
                    Console.WriteLine();

                    Console.WriteLine("1.Used cars  |  2. New cars  |  3.Back to main menu");
                    Console.WriteLine();
                    userInput = Console.ReadLine() ?? "";

                    bool correctChoice = false;
                    if (userInput == "1" || userInput == "2" || userInput == "3") correctChoice = true;
                    while (!correctChoice)
                    {
                        Console.Clear();
                        Console.WriteLine($"Used cars: {usedCars}");
                        Console.WriteLine($"New cars: {newCars}");
                        Console.WriteLine();

                        Console.WriteLine("1.Used cars  |  2. New cars  |  3.Back to main menu");
                        Console.WriteLine("___________________________________________________");

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Incorrect input");
                        Console.ResetColor();
                        Console.WriteLine();

                        Console.WriteLine("Please only type '1','2' or '3'");
                        Console.WriteLine();

                        userInput = Console.ReadLine() ?? "";
                        if (userInput == "1" || userInput == "2" || userInput == "3") correctChoice = true;
                    }

                    if (userInput == "1")
                    {
                        Console.Clear();
                        var groupUsedCars = Cars.OrderByDescending(car => car.GetMileage());
                        Console.WriteLine("Used cars:");
                        foreach (var car in groupUsedCars)
                        {
                            if (car.GetIsUsed())
                            {
                                Console.Write($"ID:{car.GetId()}, {car.GetCompany()} {car.GetModel()}, mileage - ");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write($"{car.GetMileage()}\n");
                                Console.ResetColor();
                            }
                        }

                        Console.WriteLine();
                        if (AskUserToContinue()) Info();
                        else PrintChoice();
                    }
                    else if (userInput == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("New cars:");
                        foreach (var car in Cars)
                        {
                            if (!car.GetIsUsed())
                            {
                                Console.Write($"ID:{car.GetId()}, {car.GetCompany()} {car.GetModel()}, ");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write("BRAND NEW!\n");
                                Console.ResetColor();
                            }
                        }
                        Console.WriteLine();
                        if (AskUserToContinue()) Info();
                        else PrintChoice();
                    }
                    else if (userInput == "3") PrintChoice();
                    break;

                // 9) mileage grouping
                case "9":
                    Console.Clear();
                    var groupMileage = Cars.OrderByDescending(car => car.GetMileage());

                    foreach (var car in groupMileage)
                    {
                        Console.Write($"ID: {car.GetId()}, {car.GetCompany()} {car.GetModel()} ");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{car.GetMileage()}KM\n");
                        Console.ResetColor();
                    }
                    Console.WriteLine();

                    if (AskUserToContinue()) Info();
                    else PrintChoice();
                    break;

                // 10) back to main menu
                case "10":
                    PrintChoice();
                    break;
                default:
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Please type the correct option!");
                    Console.ResetColor();
                    Console.WriteLine();

                    if (AskUserToContinue()) Info();
                    else PrintChoice();
                    break;
            }
        }
        public static void SaveAs()
        {
            string path = @"C:\Users\TimZhu\source\repos\CarCrudProject\CarCrudProject\CarListData";
            Console.Clear();
            Console.WriteLine("Please type the name of the file...");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Example - test.txt OR test.csv");
            Console.ResetColor();
            Console.WriteLine($"It will be saved here:\n{path}");
            Console.WriteLine();

            string userInput = Console.ReadLine();

            if (IsValidFileName(userInput) && !IsReservedName(userInput))
            {
                path += $"\\{userInput}";
                int numberOfCars = Cars.Count;
                string[] file = new string[numberOfCars + 1];
                file[0] = "id,company,model,engine,horse power,price,fuel type, number of seats, status of the car,mileage";
                for (int i = 0, j = 1; i < numberOfCars; i++)
                {
                    file[j] += $"{Cars[i].GetId()}," +
                               $"{Cars[i].GetCompany()}," +
                               $"{Cars[i].GetModel()}," +
                               $"{Cars[i].GetEngine()}," +
                               $"{Cars[i].GetHorsePower()}," +
                               $"{Cars[i].GetPrice()}," +
                               $"{Cars[i].GetFuelType()}," +
                               $"{Cars[i].GetSeat()}," +
                               $"{Cars[i].GetIsUsed()}," +
                               $"{Cars[i].GetMileage()}\n";
                }

                File.WriteAllLines(path, file);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("File saved succesfully.");
                Console.WriteLine($"File path - {path}");
                Console.ResetColor();
                Console.WriteLine();

                if (AskUserToContinue()) PrintChoice();
                else Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid file name!");
                Console.WriteLine("Make sure to type a valid file name AND that it ends WITH .txt or .csv");
                Console.ResetColor();
                Console.WriteLine();

                if (AskUserToContinue()) SaveAs();
                else PrintChoice();
            }
        }

        public static void Save()
        {
            string path = @"C:\Users\TimZhu\source\repos\CarCrudProject\CarCrudProject\cars_dataset_modifiedExtra.csv";
            int numberOfCars = Cars.Count;
            string[] file = new string[numberOfCars + 1];
            file[0] = "company,model,engine,horse power,price,fuel type, number of seats, status of the car,mileage";
            for (int i = 0, j = 1; i < numberOfCars; i++)
            {
                file[j] += $"{Cars[i].GetCompany()}," +
                           $"{Cars[i].GetModel()}," +
                           $"{Cars[i].GetEngine()}," +
                           $"{Cars[i].GetHorsePower()}," +
                           $"{Cars[i].GetPrice()}," +
                           $"{Cars[i].GetFuelType()}," +
                           $"{Cars[i].GetSeat()}," +
                           $"{Cars[i].GetIsUsed()}," +
                           $"{Cars[i].GetMileage()}\n";
            }

            File.WriteAllLines(path, file);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("File saved succesfully.");
            Console.WriteLine($"File path - {path}");
            Console.ResetColor();
            Console.WriteLine();

            if (AskUserToContinue()) PrintChoice();
            else Environment.Exit(0);
        }


        public static bool IsReservedName(string fileName)
        {
            string[] reservedNames = {"CON","PRN","AUX","NUL","COM1","COM2","COM3","COM4",
            "COM5","COM6","COM7","COM8","COM9","LPT1","LPT2","LPT3","LPT4","LPT5","LPT6",
            "LPT7","LPT8","LPT9"};

            return reservedNames.Contains(fileName);
        }

        public static bool IsValidFileName(string fileName)
        {
            bool correctName = fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
            fileName = fileName.ToLower();
            return correctName && fileName.EndsWith(".txt") || fileName.EndsWith(".csv");
        }

        public static int parseNumber(string str)
        {
            int number = 0;
            double horsePowerAsDouble;
            if (double.TryParse(str,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out horsePowerAsDouble))
            {
                return number = (int)horsePowerAsDouble;
            }
            return number;
        }


        public static bool isValidUserInput(string input)
        {
            if (input == "" || input == null) return false;
            string[] carDetails = input
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(item => item.Trim())
                    .ToArray();

            if (carDetails.Length != 9 && carDetails.Length != 8) return false;

            string horsePower = carDetails[3];
            string price = carDetails[4];
            string seat = carDetails[6];

            string carStatus = carDetails[7].ToLower(); // тут статус машины БУ или НОВАЯ
                                                     // если новая, то не обязательно пробег добавлять т.к. он 0

            if (carDetails.Length == 8 || carStatus == "n")
            {
                if (isVerifiedParsedNumber(horsePower)
                    && isVerifiedParsedNumber(price)
                    && isVerifiedParsedNumber(seat))
                {
                    if (parseNumber(horsePower) > 0 
                        && parseNumber(price) > 0
                        && parseNumber(seat) > 0)
                    {
                        return true;
                    } 
                }
                return false;
            }
            else
            {
                string mileage = carDetails[8];
                if (isVerifiedParsedNumber(horsePower)
                    && isVerifiedParsedNumber(price)
                    && isVerifiedParsedNumber(seat)
                    && isVerifiedParsedNumber(mileage))
                {
                    if (parseNumber(horsePower) > 0
                        && parseNumber(price) > 0
                        && parseNumber(seat) > 0
                        && parseNumber(mileage) > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static void Add()
        {
            Console.Clear();
            Console.WriteLine("Add the car as follows:\n" +
                "Company, Model, Engine, Horse power, Price, Fuel type, " +
                "Amount of seats, 'N' - new car OR 'U' - used car, Mileage");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("EXAMPLE:\nToyota, camry, v8, 200, 20000, gasoline, 5, u, 200000");
            Console.ResetColor();

            Console.WriteLine();
            string inputAdd = Console.ReadLine();

            if (isValidUserInput(inputAdd))
            {
                string[] carDetails = inputAdd
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(detail => detail.Trim())
                    .ToArray();

                string company = carDetails[0];
                string model = carDetails[1];
                string engine = carDetails[2];
                int hp = parseNumber(carDetails[3]);
                int price = parseNumber(carDetails[4]);
                string fuelType = carDetails[5];
                int seat = parseNumber(carDetails[6]);
                bool isUsed = carDetails[7].Trim().ToLower() == "u";

                int mileage = carDetails.Length == 9 ? parseNumber(carDetails[8]) : 0;

                var newCar = new Car(
                    nextId++,
                    company,
                    model,
                    engine,
                    hp,
                    price,
                    fuelType,
                    seat,
                    isUsed,
                    mileage
                    );
                Cars.Add(newCar);

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The new car has been added.");
                Console.WriteLine($"ID: {newCar.GetId()}");
                Console.ResetColor();

                Console.WriteLine();
                if (AskUserToContinue()) Add();
                else PrintChoice();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect input....");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Please add the car following the correct rules:\n" +
                "Company, Model, Engine, Horse power, Price, Fuel type, " +
                "Amount of seats, 'N' - new car OR 'U' - used car, Mileage");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("EXAMPLE:\nToyota, camry, v8, 200, 20000, gasoline, 5, u, 200000");
                Console.ResetColor();

                Console.WriteLine();
                if (AskUserToContinue()) Add();
                else PrintChoice();
            }
        }

        public static void Edit()
        {
            Console.Clear();
            Console.WriteLine("Type the ID of the car that you want to edit");
            Console.WriteLine();

            string id = Console.ReadLine();

            if (!isVerifiedParsedNumber(id))
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please enter the NUMBER of ID!!!!!\nNOT YOUR BULLSHIT");
                Console.ResetColor();

                Console.WriteLine();
                if (AskUserToContinue()) Edit();
                else PrintChoice();
            }
            int idInt = parseNumber(id);
            if (idInt >= nextId || idInt < 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid ID");
                Console.ResetColor();
                Console.WriteLine();

                if (AskUserToContinue()) Edit();
                else PrintChoice();
            }
            else
            {
                Car carToEdit = Cars.Find(c => c.GetId() == idInt);
                if (carToEdit != null)
                {
                    Console.Clear();
                    carToEdit.ShowInfo();
                    
                    Console.WriteLine();
                    Console.WriteLine("Please update the car following the correct rules:\n" +
                    "Company, Model, Engine, Horse power, Price, Fuel type, " +
                    "Amount of seats, 'N' - new car OR 'U' - used car, Mileage");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("EXAMPLE:\nToyota, camry, v8, 200, 20000, gasoline, 5, u, 200000");
                    Console.ResetColor();

                    Console.WriteLine();
                    string userInput = Console.ReadLine();
                    if (isValidUserInput(userInput))
                    {
                        string[] carDetails = userInput
                                 .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim())
                                 .ToArray();


                        string company = carDetails[0];
                        string model = carDetails[1];
                        string engine = carDetails[2];
                        int hp = parseNumber(carDetails[3]);
                        int price = parseNumber(carDetails[4]);
                        string fuelType = carDetails[5];
                        int seat = parseNumber(carDetails[6]);
                        bool isUsed = carDetails[7].Trim().ToLower() == "u"; ;

                        int mileage = carDetails.Length == 9 ? parseNumber(carDetails[8]) : 0;

                        carToEdit.SetCompany(company);
                        carToEdit.SetModel(model);
                        carToEdit.SetEngine(engine);
                        carToEdit.SetHorsePower(hp);
                        carToEdit.SetPrice(price);
                        carToEdit.SetFuelType(fuelType);
                        carToEdit.SetSeat(seat);
                        carToEdit.SetIsUsed(isUsed);
                        carToEdit.SetMileage(mileage);

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"The car number {idInt} has been updated successfully!");
                        Console.ResetColor();
                        Console.WriteLine();

                        if (AskUserToContinue()) Edit();
                        else PrintChoice();
                    } 
                    else
                    {
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Incorrect edition");
                        Console.ResetColor();
                        Console.WriteLine();

                        Console.WriteLine("Please update the car following the correct rules:\n" +
                                          "Company, Model, Engine, Horse power, Price, Fuel type, " +
                                          "Amount of seats, 'N' - new car OR 'U' - used car, Mileage");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("EXAMPLE:\nToyota, camry, v8, 200, 20000, gasoline, 5, u, 200000");
                        Console.ResetColor();
                        Console.WriteLine();

                        if (AskUserToContinue()) Edit();
                        else PrintChoice();
                    }

                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Car with this ID already been removed.");
                    Console.ResetColor();
                    Console.WriteLine();

                    if (AskUserToContinue()) Edit();
                    else PrintChoice();
                }
            }
        }

        public static void Delete()
        {
            Console.Clear();
            Console.WriteLine("Type the ID of the car that you want to DELETE");
            Console.WriteLine();

            string id = Console.ReadLine();
            if (!isVerifiedParsedNumber(id))
            {
                Console.Clear();
                Console.WriteLine("Please enter the NUMBER of ID!!!!!\nNOT YOUR BULLSHIT");
                Console.WriteLine();

                if (AskUserToContinue()) Delete();
                else PrintChoice();
            }
            int idInt = parseNumber(id);
            if (idInt >= nextId || idInt < 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid ID");
                Console.ResetColor();
                Console.WriteLine();

                if (AskUserToContinue()) Delete();
                else PrintChoice();
            } 
            else
            {
                Car carToRemove = Cars.Find(c => c.GetId() == idInt);
                if (carToRemove != null)
                {
                    Cars.Remove(carToRemove);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Car number {idInt} has been removed.");
                    Console.ResetColor();
                    Console.WriteLine();

                    if (AskUserToContinue()) Delete();
                    else PrintChoice();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Car with this ID already been removed.");
                    Console.WriteLine();

                    if (AskUserToContinue()) Delete();
                    else PrintChoice();
                }
            }
        }

        public static void Initialize()
        {
            string path = @"C:\Users\TimZhu\source\repos\CarCrudProject\CarCrudProject\cars_dataset_modifiedExtra.csv";

            var lines = File.ReadAllLines(path)
                            .Where(line => !string.IsNullOrWhiteSpace(line))
                            .ToArray();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',');
                
                string company = columns[0];
                string model = columns[1];
                string engine = columns[2];
                
                //horsepower
                int horsePower = 0;
                string horsePowerStr = columns[3].Trim().Replace(",", ".");
                if (isVerifiedParsedNumber(horsePowerStr)) horsePower = parseNumber(horsePowerStr);

                // price
                int price = 0;
                string priceStr = columns[4].Trim().Replace(",", ".");
                if (isVerifiedParsedNumber(priceStr)) price = parseNumber(priceStr);


                // fuel type
                string fuelType = columns[5];


                // seat quantity
                if (int.TryParse(columns[6], out int seat));
                else
                {
                    seat = 0;
                }

                // new or used
                bool isUsed;
                if (columns[7].ToLower() == "false")
                {
                    isUsed = false;
                } else
                {
                    isUsed = true;
                }

                // mileage
                int mileage = 0;
                string mileageStr = columns[8];
                if (isVerifiedParsedNumber(mileageStr)) mileage = parseNumber(mileageStr);    

                Cars.Add(new Car(nextId++, company, model, engine, horsePower, 
                price,fuelType, seat, isUsed, mileage));
            }
            PrintChoice();
        }
    }
}
