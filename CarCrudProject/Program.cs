using CarCrudProject.Models;
using System;
using System.IO;
using System.Linq;
using CarCrudProject.Services;

public class Program
{
    public static readonly string path = @"..\..\..\..\CarCrudProject\Data/cars_dataset_modified_final.csv";
    public static void Main(string[] args)
    {
        App app = new App();

        string asciiArt = "                              _.-=\"_-         _\n                         _.-=\"   _-          | ||\"\"\"\"\"\"\"---._______     __..\n             ___.===\"\"\"\"-.______-,,,,,,,,,,,,`-''----\" \"\"\"\"\"       \"\"\"\"\"  __'\n      __.--\"\"     __        ,'                   o \\           __        [__|\n __-\"\"=======.--\"\"  \"\"--.=================================.--\"\"  \"\"--.=======:\n]       [w] : /        \\ : |========================|    : /        \\ :  [w] :\nV___________:|          |: |========================|    :|          |:   _-\"\n V__________: \\        / :_|=======================/_____: \\        / :__-\"\n -----------'  \"-____-\"  `-------------------------------'  \"-____-\"";
        Console.Clear();
        Console.WriteLine(asciiArt);
        Console.WriteLine();
        Console.WriteLine();
        
        //initialization
        Logger.Write("START","Application started");
        app.Run(path);
    }
}
