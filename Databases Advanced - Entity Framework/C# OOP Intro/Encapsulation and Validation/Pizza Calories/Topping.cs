using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Topping
{
    private string type;
    private double weight;
    private double modifier;
    public Topping(string type, double weight)
    {
        Type = type;
        Weight = weight;
    }
    public string Type
    {
        get { return type; }
        set
        {
            var val = char.ToUpper(value[0]) + value.Substring(1);

            if (value != "meat" && value != "veggies" &&
                value != "cheese" && value != "sauce")
            {                
                throw new ArgumentException($"Cannot place {val} on top of your pizza.");
            }
            
            type = val;
        }
    }
    public double Weight
    {
        get { return weight; }
        set
        {
            if (value < 1 || value > 50)
            {
                throw new ArgumentException($"{Type} weight should be in the range [1..50].");
            }
            weight = value;
        }
    }

    public double Calories()
    {
        switch (Type)
        {
            case "Meat":modifier = 1.2;break;
            case "Veggies": modifier = 0.8; break;
            case "Cheese": modifier = 1.1; break;
            case "Sauce": modifier = 0.9; break;
        }
        return weight * 2 * modifier;
    }
}

