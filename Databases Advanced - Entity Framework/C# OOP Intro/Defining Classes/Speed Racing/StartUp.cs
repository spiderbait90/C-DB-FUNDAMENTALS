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
        string[] input;
        var cars = new Dictionary<string, Car>();
        for (int i = 0; i < n; i++)
        {
            input = Console.ReadLine()
                .Split(new[] { ' ', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);

            var model = input[0];
            var fuelAmount = decimal.Parse(input[1]);
            var FuelPerKm = decimal.Parse(input[2]);
            var car = new Car(model, fuelAmount, FuelPerKm);

            if (!cars.ContainsKey(model))
                cars.Add(model, car);
        }

        while (true)
        {
            input = Console.ReadLine()
                .Split(new[] { ' ', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            if (input[0] == "End")
                break;

            var amountOfKm = decimal.Parse(input[2]);
            var model = input[1];
            cars[model].Move(amountOfKm);
        }

        foreach (var car in cars.Values)
        {
            Console.WriteLine($"{car.Model} {car.FuelAmount:f2} {car.DistanceTraveled}");
        }
    }
}

