using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var employees = new Dictionary<string, List<Employee>>();

        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split(' ');
            var name = input[0];
            var salary = decimal.Parse(input[1]);
            var position = input[2];
            var department = input[3];

            var emp = new Employee
                    (name, salary, position, department);
            if (input.Length == 6)
            {
                emp.Email = input[4];
                emp.Age = int.Parse(input[5]);
            }
            else if (input.Length == 5)
            {
                if (input[4].Contains('@'))
                {
                    emp.Email = input[4];
                }
                else
                {
                    emp.Age = int.Parse(input[4]);
                }
            }

            if (!employees.ContainsKey(department))
                employees[department] = new List<Employee>();
            employees[department].Add(emp);            
        }

        string highestDep = null;
        decimal max = 0;
        foreach (var dep in employees)
        {
            decimal sum = 0;
            foreach (var em in dep.Value)
            {
                sum += em.Salary;
            }
            if (sum / dep.Value.Count() > max)
            {
                max = sum / dep.Value.Count();
                highestDep = dep.Key;
            }
        }

        foreach (var dep in employees.Where(x => x.Key == highestDep))
        {
            Console.WriteLine($"Highest Average Salary: {dep.Key}");
            foreach (var em in dep.Value.OrderByDescending(x=>x.Salary))
            {
                Console.WriteLine($"{em.Name} {em.Salary:f2} {em.Email} {em.Age}");
            }
        }
    }
}

