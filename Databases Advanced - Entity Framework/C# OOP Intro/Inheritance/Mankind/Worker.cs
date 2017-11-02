using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Worker : Human
{
    private decimal weekSalary;
    private decimal hPerD;

    public Worker(string firstName, string lastName, decimal weekSalary, decimal hPerD)
        : base(firstName, lastName)
    {
        HPerD = hPerD;
        WeekSalary = weekSalary;
    }


    public decimal WeekSalary
    {
        get
        {
            return weekSalary;
        }
        set
        {
            if (value <= 10)
                throw new ArgumentException($"Expected value mismatch! Argument: weekSalary");

            weekSalary = value;
        }
    }
    public decimal HPerD
    {
        get
        {
            return hPerD;
        }
        set
        {
            if (value < 1 || value > 12)
                throw new ArgumentException($"Expected value mismatch! Argument: workHoursPerDay");

            hPerD = value;
        }
    }

    public decimal Calculate()
    {
        var perDay = WeekSalary / 5;
        var perHour = perDay / HPerD;

        return perHour;
    }

    public override string ToString()
    {
        return $"First Name: {FirstName}" + Environment.NewLine +
            $"Last Name: {LastName}" + Environment.NewLine +
            $"Week Salary: {WeekSalary:f2}" + Environment.NewLine +
            $"Hours per day: {HPerD:f2}" + Environment.NewLine +
            $"Salary per hour: {Calculate():f2}";
    }
}

