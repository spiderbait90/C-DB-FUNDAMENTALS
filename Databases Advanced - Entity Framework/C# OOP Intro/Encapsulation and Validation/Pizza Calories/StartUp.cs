using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    static void Main(string[] args)
    {
        try
        {
            var inputP = Console.ReadLine().Split();

            var pizza = new Pizza(inputP[1]);

            var inputD = Console.ReadLine().Split();

            var dough = new Dough
                (inputD[1].ToLower(), inputD[2].ToLower(), double.Parse(inputD[3]));

            pizza.Dough = dough;
            var countToppings = 0;
            while (true)
            {
                countToppings++;
                if (countToppings == 11)
                    throw new ArgumentException("Number of toppings should be in range [0..10].");
                var inputT = Console.ReadLine().Split();

                if (inputT[0] == "END")
                    break;

                var topping = new Topping(inputT[1].ToLower(), double.Parse(inputT[2]));
                pizza.Toppings.Add(topping);
                
            }
            Console.WriteLine($"{pizza.Name} - {pizza.Calories():f2} Calories.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);            
        }
        
    }
}

