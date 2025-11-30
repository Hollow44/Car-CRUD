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
                    Commands.Exit();
                    break;

                default:
                    Commands.Help();
                    break;
            }
        }
        else
        {
            string argument = userInput[1];

            switch (command)
            {
                case "add":
                    Commands.Add(argument);
                    break;
                
                case "show":
                    Commands.Show(argument);
                    break;
                
                case "edit":
                    Commands.Edit(argument);
                    break;

                case "delete":
                    Commands.Delete(argument);
                    break;
                
                case "saveas":
                    FileSaver.SaveAs(argument);
                    break;
                
                case "--help":
                    Commands.Help(argument);
                    break;
                
                default:
                    Commands.Help();
                    break;
            }
        }
    }
}