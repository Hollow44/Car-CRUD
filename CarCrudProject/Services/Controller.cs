using System.ComponentModel.Design;
using CarCrudProject.Data;
using CarCrudProject.Utilities;
using CarCrudProject.Repositories;

namespace CarCrudProject.Services;

public class Controller
{
    private readonly CarFactory factory = new();
    private readonly CarRepository repo = new();
    public void ProcessInput(string str)
    {
        // TODO: добавить отдельные методы проверки и тд. Чтобы в каждом свитч кейсе был просто 1 метод, например 'Add()'
        
        string[] userInput = Parser.ParseCommand(str);

        string command = userInput[0];

        if (userInput.Length == 1)
        {
            switch (command)
            {
                case "save":
                    FileSaver.Save();
                    break;

                case "clear":
                    Console.Clear();
                    break;

                case "exit":
                    Environment.Exit(0);
                    break;

                default:
                    Commands.Help();
                    break;
            }
        }
        else
        {
            string argument = userInput[1];
            // add model, company, price, hp
            // ["add", "model,", "company," "price,", "hp,"]
            switch (command)
            {
                case "add":
                    string[] arguments = argument.Split(',');

                    if (arguments.Length == 9)
                    {
                        if (Parser.IsValidNumberToParse(arguments[3])
                            && Parser.IsValidNumberToParse(arguments[4])
                            && Parser.IsValidNumberToParse(arguments[6])
                            && Parser.IsValidNumberToParse(arguments[8]))
                        {
                            var car = factory.Create(arguments, repo.NextId);
                            repo.Add(car);
                        }
                        else Commands.Help("add");
                    }
                    else Commands.Help("add");
                    break;
                
                case "show":
                    if (Parser.IsValidNumberToParse(argument))
                    {
                        int id = Parser.ParseNumber(argument);
                        Commands.Show(id);
                    }
                    else
                    {
                        argument = argument.ToLower();
                        if (argument == "all") Commands.Show();
                        else Console.WriteLine($"'show '{argument}' is not a valid command. See '--help show'");
                    }
                    break;
                
                case "edit":
                    // Edit(id);
                    break;
                
                case "delete":
                    // Delete(id);
                    break;
                
                case "saveas":
                    // SaveAs(path);
                    break;
                
                case "--help":
                    // Help(command);
                    break;
                
                default:
                    Commands.Help();
                    break;
            }
        }
    }
}