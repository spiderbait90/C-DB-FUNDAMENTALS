using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var persons = new List<Person>();
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split();
            var firstname = input[0];
            var lastName = input[1];
            var age = int.Parse(input[2]);
            var salary = decimal.Parse(input[3]);

            var person = new Person(firstname, lastName, age, salary);
            persons.Add(person);
        }

        var team = new Team("Maders");

        foreach (var person in persons)
        {
            team.AddPlayer(person);
        }

        Console.WriteLine(team.FirstTeam.Count());
        Console.WriteLine(team.SecondTeam.Count);
    }
}

