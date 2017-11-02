using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var persons = new List<Person>();
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split(' ');
            var name = input[0];
            var age = int.Parse(input[1]);

            var person = new Person(name,age);
            persons.Add(person);
        }

        foreach (var person in persons.Where(x=>x.Age>30)
            .OrderBy(x=>x.Name))
        {
            Console.WriteLine(person.Name+" - "+person.Age);
        }
    }
}

