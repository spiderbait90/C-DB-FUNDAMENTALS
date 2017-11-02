using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Product
{
    private string name;
    private decimal price;

    public Product(string name,decimal price)
    {
        Name = name;
        Price = price;
    }
    public string Name
    {
        get { return name; }
        set
        {
            if (value==string.Empty)
            {
                throw new ArgumentException("Name cannot be empty");
            }
            name = value;
        }
    }

    public decimal Price
    {
        get { return price; }
        set
        {
            if(value<=0)
            {
                throw new ArgumentException("Price cannot be zero or negative");
            }
            price = value;
        }
    }
}
