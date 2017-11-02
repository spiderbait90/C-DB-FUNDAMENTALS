using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var cars = new List<Car>();

        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split();

            var engine = new Engine(int.Parse(input[1]), int.Parse(input[2]));
            var cargo = new Cargo(int.Parse(input[3]), input[4]);

            var tire = new Tire(double.Parse(input[5]), int.Parse(input[6]),
                                double.Parse(input[7]), int.Parse(input[8]),
                                double.Parse(input[9]), int.Parse(input[10]),
                                double.Parse(input[11]), int.Parse(input[12]));
            var car = new Car(input[0], engine, cargo, tire);

            cars.Add(car);
        }
        var type = Console.ReadLine();

        if (type == "fragile")
        {
            foreach (var car in cars.Where(x => x.Cargo.CargoType == "fragile"
            && (x.Tire.Tire1Pressure < 1 || x.Tire.Tire2Pressure < 1 ||
            x.Tire.Tire3Pressure < 1 || x.Tire.Tire4Pressure < 1)))
            {
                Console.WriteLine(car.Model);
            }
        }
        else if (type == "flammable")
        {
            foreach (var car in cars.Where(x=>x.Engine.EnginePower>250))
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}

