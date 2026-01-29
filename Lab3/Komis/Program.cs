using ConsoleApp1.Models;
using Newtonsoft.Json;

var path = Path.Combine(Directory.GetCurrentDirectory(), "data.json");
var txt = File.ReadAllText(path);
var data = JsonConvert.DeserializeObject<List<Car>>(txt);


var continueApp = true
do
{
    Console.WriteLine("Wybierz opcje z Menu");
    Console.WriteLine("1. Lista pojazdów");
    Console.WriteLine("2. Dodaj pojazd");
    Console.WriteLine("3. Usuń pojazd");
    Console.WriteLine("0. Wyjdź");

    switch (Console.ReadKey().KeyChar)
    {
        case "1" ;
            for (int i = 0, data)
            {
                car.Showinfo();
            }
            break;
        case "2" ;
            break;
        case "3" ;
            break;
        case "0" ;
            break;
        default:
            Console.WriteLine("Zła opcja");
            break;
    }
    
}

while (true);


return;



void AddCar()
{
    throw new NotImplementedException();
}



/*
Car car1 = new Car(model: "BMW",  year: 2020, color: "Black");
Car car2 = new Car(model: "Fiat", year: 2022, color: "Orange");

Bike bike1 = new Bike(model: "Yamaha", year: 2015, color: "Silver");
Bike bike2 = new Bike(model: "Toshiba", year: 2019, color: "Gold");
var vehiclelist = new List<Vehicle>()
{
    car1, new Car(model: "Audi", year: 2020, color: "White"),
    bike2, bike1
};

vehiclelist.Add(new Bike(model: "Honda", year: 2022, color: "White"));
Bike bike4 = new Bike(model: "Kawasaki", year: 2024, color: "Yellow");
vehiclelist.Add(bike4);

var orderedByModel = vehiclelist.OrderBy(by => by.Model);

foreach (var v in orderedByModel)
{
    v.ShowInfo();
}
*/
