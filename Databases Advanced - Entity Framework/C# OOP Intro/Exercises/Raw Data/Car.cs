using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Car
{
    public string Model { get; set; }
    public Engine Engine { get; set; }
    public Cargo Cargo { get; set; }
    public Tire Tire { get; set; }

    public Car(string model,Engine engine,Cargo cargo,Tire tire)
    {
        Model = model;
        Engine = engine;
        Cargo = cargo;
        Tire = tire;
    }
    
}

