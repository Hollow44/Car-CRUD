using CarCrudProject.Repositories;
using CarCrudProject.Services;
using CarCrudProject.Data;

public class App
{
    private Controller controller = new Controller();
    
    public void Run(string path)
    {
        FileLoader.Load(path);
        
        while (true)
        {
            string userInput = Console.ReadLine() ?? "";
            controller.ProcessInput(userInput);
        }
    }
}