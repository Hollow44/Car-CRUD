namespace CarCrudProject.Models;

public class Transaction
{
    private int Id;
    private int CarId;
    private DateTime Date;
    public Transaction(int id, int carId, DateTime date)
    {
        this.Id = id;
        this.CarId = carId;
        this.Date = date;
    }
    
    public int GetId()
    {
        return Id;
    }

    public void SetId(int id)
    {
        this.Id = id;
    }
    
    public int GetCarId()
    {
        return CarId;
    }

    public void SetCarId(int carId)
    {
        this.CarId = carId;
    }

    public DateTime GetDate()
    {
        return this.Date;
    }

    public void SetDate(DateTime date)
    {
        this.Date = date;
    }
}