using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Book
{
    private string author;
    private string title;
    private decimal price;

    public Book(string author, string title, decimal price)
    {
        Author = author;
        Title = title;
        Price = price;
    }
    public string Author
    {
        get
        {
            return this.author;
        }
        set
        {
            var splited = value.Split();
            string secondName = " ";
            if (splited.Length == 2)
                secondName = splited[1];

            if (char.IsDigit(secondName[0]))
                throw new ArgumentException("Author not valid!");

            this.author = value;
        }
    }

    public string Title
    {
        get
        {
            return this.title;
        }
        set
        {
            if (value.Length < 3)
                throw new ArgumentException("Title not valid!");

            this.title = value;
        }
    }


    public virtual decimal Price
    {
        get
        {
            return this.price;
        }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Price not valid!");

            this.price = decimal.Round(value, 2);
        }
    }

    public override string ToString()
    {
        return $"Type: {this.GetType().Name}" + Environment.NewLine +
        $"Title: {this.Title}" + Environment.NewLine +
        $"Author: {this.Author}" + Environment.NewLine +
        $"Price: {this.Price:f2}";
    }
}

