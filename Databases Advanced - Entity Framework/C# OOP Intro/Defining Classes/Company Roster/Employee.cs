using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }

    public Employee()
    {
        Email = "n/a";
        Age = -1;
    }
    public Employee(string name, decimal salary, string position,
        string department) : this()
    {
        Name = name;
        Salary = salary;
        Position = position;
        Department = department;        
    }

    public Employee(string name, decimal salary, string position,
        string department,string email,int age)
        :this(name,salary,position,department)
    {
        Email = email;
        Age = Age;
    }

    public Employee(string name, decimal salary, string position,
        string department, string email)
        : this(name, salary, position, department)
    {
        Email = email;
    }

    public Employee(string name, decimal salary, string position,
        string department,int age)
        : this(name, salary, position, department)
    {
        Age = age;
    }

}

