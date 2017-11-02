using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {
        var family = new Family();
        var n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split();
            var member = new Person(input[0], int.Parse(input[1]));
            family.AddMember(member);
        }
        Console.WriteLine(family.GetOldestMember());
    }
}

