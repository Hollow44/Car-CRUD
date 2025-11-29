using CarCrudProject.Services;

public class App
{
    private bool IsRunning = true;
    private Controller controller = new Controller();
    
    public void Run()
    {
        while (IsRunning)
        {
            string userInput = Console.ReadLine() ?? "";
            controller.ProcessInput(userInput);
        }
    }
}