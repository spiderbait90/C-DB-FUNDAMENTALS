using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main(string[] args)
    {
        var customers = new Dictionary<string, Person>();
        var products = new Dictionary<string, Product>();

        var persons = Console.ReadLine()
            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var p in persons)
        {
            var splited = p.Split('=');
            try
            {
                var person = new Person(splited[0], decimal.Parse(splited[1]));
                customers.Add(splited[0], person);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

        }

        var productPrice = Console.ReadLine()
            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var p in productPrice)
        {
            var splited = p.Split('=');
            try
            {
                var product = new Product(splited[0], decimal.Parse(splited[1]));

                products.Add(splited[0], product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        while (true)
        {
            var input = Console.ReadLine().Split();
            if (input[0] == "END")
                break;

            var name = input[0];
            var product = input[1];

            customers[name].Buy(products[product]);
        }

        foreach (var person in customers.Values)
        {
            Console.Write($"{person.Name} - ");

            if (person.Products.Count() == 0)
                Console.WriteLine("Nothing bought");
            else
            {
                var bag = new List<string>();
                foreach (var item in person.Products)
                {
                    bag.Add(item.Name);
                }
                Console.WriteLine(string.Join(", ",bag));
            }
        }        
    }
}

