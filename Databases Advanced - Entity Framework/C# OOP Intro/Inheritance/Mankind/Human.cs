using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Human
{
    private string firstName;
    private string lastName;

    public Human(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName
    {
        get
        {
            return firstName;
        }
        set
        {
            if (char.IsLower(value[0]))
            {
                throw new ArgumentException($"Expected upper case letter! Argument: firstName");
            }
            else if(value.Length < 4)
            {
                throw new ArgumentException($"Expected length at least 4 symbols! Argument: firstName");
            }
            firstName = value;
        }
    }
    public string LastName
    {
        get
        {
            return lastName;
        }
        set
        {
            if (char.IsLower(value[0]))
            {
                throw new ArgumentException($"Expected upper case letter! Argument: lastName");
            }
            else if (value.Length < 3)
            {
                throw new ArgumentException($"Expected length at least 3 symbols! Argument: lastName ");
            }
            lastName = value;
        }
    }

}

