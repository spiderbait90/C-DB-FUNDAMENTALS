using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Pizza
{
    private string name;
    private List<Topping> toppings;
    private Dough dough;

    public Pizza(string name)
    {
        Name = name;
        toppings = new List<Topping>();
    }
    public string Name
    {
        get { return name; }
        set
        {
            if (value.Length < 1 || value.Length > 15)
            {
                throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
            }
            name = value;
        }
    }
    public Dough Dough
    {
        get { return dough; }
        set
        {
            dough = value;
        }
    }

    public List<Topping> Toppings
    {
        get { return toppings; }
        set
        {
            toppings = value;
        }
    }

    public double Calories()
    {
        var calories = Dough.Calories();

        foreach (var item in Toppings)
        {
            calories += item.Calories();
        }
        return calories;
    }
}

