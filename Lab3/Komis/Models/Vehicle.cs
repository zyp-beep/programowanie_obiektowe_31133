namespace ConsoleApp1.Models;

public abstract class Vehicle : Object
{
    string model;
    int year;
    string color;

    public string Model
    {
        get { return model; }
    }
    public int Year => year;

    public string Color
    {
        get { return color; }
    }
    
    public Vehicle(string model, int year, string color)
        {
        this.model = model;
        this.year = year;
        this.color = color;
        }

    public virtual void ShowInfo()
    {
        Console.WriteLine("Model: {0}", this.model);
        Console.WriteLine("Year: {0}", year);
        Console.WriteLine("Color: {0}", color);
    }

    public virtual void ShowInfo(string header)
    {
        Console.WriteLine(header);
        ShowInfo();
    }
    
}