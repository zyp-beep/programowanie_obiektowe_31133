using System.Security.AccessControl;

Car car1 = new Car("VW", 2025);
Car car2 = new Car("Audi", 2000);
Car car3 = new Car("RollsRoyce", 2005);

car1.ShowMe();
car2.ShowMe();
car3.ShowMe();

Console.WriteLine("Your car: {0}", car1.model);
class Car
{
    string model;
    int year;

    public string Model
    {
        get {return model;}
    }

    public void ShowMe()
    {
        Console.WriteLine("Model: {0}, Year: {1}", model, year);
    }
}