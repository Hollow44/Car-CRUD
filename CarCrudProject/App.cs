using CarCrudProject.Services;

public class App
{
    private bool IsRunning = true;
    private Controller controller = new Controller();
    
    public void Run()
    {
        CarDataSetImporter carDataSetImporter = new CarDataSetImporter();
        carDataSetImporter.Import();
        while (IsRunning)
        {
            string userInput = Console.ReadLine() ?? "";
            controller.ProcessInput(userInput);
        }
    }
}