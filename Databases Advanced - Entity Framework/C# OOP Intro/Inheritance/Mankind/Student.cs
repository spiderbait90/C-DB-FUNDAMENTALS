using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


public class Student : Human
{
    private string number;

    public Student(string firstName, string lastName, string number)
        : base(firstName, lastName)
    {
        Number = number;
    }

    public string Number
    {
        get
        {
            return this.number;
        }
        set
        {
            if (value.Length < 5 || value.Length > 10 ||
                value.Any(x => !char.IsLetterOrDigit(x)))
                throw new ArgumentException("Invalid faculty number!");

            number = value;
        }

    }

    public override string ToString()
    {
        return $"First Name: {FirstName}" + Environment.NewLine +
            $"Last Name: {LastName}" + Environment.NewLine +
            $"Faculty number: {Number}";
    }
}

