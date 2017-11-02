using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Car
{
    public string Model { get; set; }
    public decimal FuelAmount { get; set; }
    public decimal FuelPerKm { get; set; }
    public decimal DistanceTraveled { get; set; }

    public Car(string model, decimal fuelAmount, decimal fuelPerKm)
    {        
        Model = model;
        FuelAmount = fuelAmount;
        FuelPerKm = fuelPerKm;
        DistanceTraveled = 0;
    }

    public void Move(decimal amountOfKm)
    {
        var neededFuel = amountOfKm * this.FuelPerKm;
        if(neededFuel>FuelAmount)
        {
            Console.WriteLine("Insufficient fuel for the drive");
        }
        else
        {
            FuelAmount -= neededFuel;
            DistanceTraveled += amountOfKm;
        }
    }
}
