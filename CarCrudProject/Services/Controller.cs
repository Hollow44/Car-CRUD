using System.ComponentModel.Design;
using CarCrudProject.Data;
using CarCrudProject.Utilities;
using CarCrudProject.Repositories;

namespace CarCrudProject.Services;

public class Controller
{
    public void ProcessInput(string str)
    {
        string[] userInput = Parser.ParseCommand(str);
        
        string command = userInput[0].ToLower();

        if (userInput.Length == 1)
        {
            switch (command)
            {
                case "save":
                    FileSaver.Save();
                    break;

                case "clear":
                    Console.Clear();
                    Logger.Write("CLEAR","console got cleared");
                    break;

                case "exit":
                    Commands.Exit();
                    break;
                
                case "--help":
                    Commands.Help();
                    break;
                
                case "delete":
                    Console.WriteLine($"'{command}' you can't use this command without passing the 'id' parameter. See '--help {command}'");
                    break;
                
                case "show":
                    Console.WriteLine($"'{command}' you can't use this command without passing the 'id' or 'all' parameter. See '--help {command}'");
                    break;
                
                case "add":
                    Console.WriteLine($"'{command}' you can't use this command without passing the 'company', 'model', " +
                                      $"'engine', 'horsepower', 'price', 'fuel type', 'number of seats', 'car status' and 'mileage'  parameters. See '--help {command}'");
                    break;
                
                case "edit":
                    Console.WriteLine($"'{command}' you can't use this command without passing the 'id' parameter. See '--help {command}'");
                    break;
                
                case "saveas":
                    FileSaver.SaveAs();
                    break;
                
                case "":
                    break;

                default:
                    Console.WriteLine($"'{str}' is not a correct command. See '--help'");
                    break;
            }
        }
        else
        {
            string argument = userInput[1];

            string addArguments = string.Join(",", userInput
                .Where((arg,index) => index != 0));
            
            switch (command)
            {
                case "add":
                    Commands.Add(addArguments);
                    break;
                
                // TODO: добавить возможность фильтрации, например - show cars where price < 100_000 and status is new
                case "show":
                    Commands.Show(argument);
                    break;
                
                // TODO: добавить возможность обновления только конкретного поля, например - edit 12.
                // На экран выходит машина с id 12 и теперь можно только отдельные поля ей заменить
                // например, model TESLA
                case "edit":
                    Commands.Edit(argument);
                    break;

                case "delete":
                    Commands.Delete(argument);
                    break;
                
                case "--help":
                    Commands.Help(argument);
                    break;
                
                default:
                    Console.WriteLine($"'{str}' is not a correct command. See '--help'");
                    break;
            }
        }
    }
}