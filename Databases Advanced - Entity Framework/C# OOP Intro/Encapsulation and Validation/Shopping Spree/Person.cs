using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Person
{
    private string name;
    private decimal money;
    private List<Product> products;

    public Person(string name,decimal money)
    {
        Name = name;
        Money = money;
        products = new List<Product>();
    }

    public string Name
    {
        get { return name; }
        set
        {
            if (value == string.Empty)
                throw new ArgumentException("Name cannot be empty");

            name = value;
        }
    }

    public decimal Money
    {
        get { return money; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Money cannot be negative");
            money = value;
        }
    }
    public List<Product> Products
    {
        get { return products; }
        set { products = value; }
    }

    public void Buy(Product product)
    {
        if (product.Price > money)
            Console.WriteLine($"{name} can't afford {product.Name}");
        else
        {
            products.Add(product);
            money -= product.Price;
            Console.WriteLine($"{name} bought {product.Name}");
        }
    }

}

