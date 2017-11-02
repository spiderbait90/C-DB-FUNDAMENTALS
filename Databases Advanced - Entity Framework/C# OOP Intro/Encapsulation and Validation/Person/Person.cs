using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Person
{
    private string firstName;
    private string lastName;
    private int age;
    private decimal salary;

    public Person(string firstName, string lastName, int age, decimal salary)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Age = age;
        this.Salary = salary;
    }
    public string FirstName
    {
        get { return firstName; }
        set
        {
            if (value.Length < 3)
                throw new ArgumentException("Names must be at least 3 symbols");
            firstName = value;
        }
    }

    public string LastName
    {
        get { return lastName; }
        set
        {
            if (value.Length < 3)
                throw new ArgumentException("Names must be at least 3 symbols");
            lastName = value;
        }
    }
    public int Age
    {
        get { return age; }
        set
        {
            if (value < 1)
                throw new ArgumentException("Age must not be zero or negative");
            age = value;
        }
    }
    public decimal Salary
    {
        get { return salary; }
        set
        {
            if (value < 460)
                throw new ArgumentException("Salary can't be less than 460.0");
            salary = value;
        }
    }
    public override string ToString()
    {
        return $"{FirstName} {LastName} get {Salary:f2} Leva";
    }

    public void IncreaseSalary(decimal bonus)
    {
        if (Age < 30)
            Salary += (Salary * (bonus / 2) / 100);
        else
            Salary += (Salary * bonus / 100);
    }
}


