using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<BankAccount> Accounts { get; set; }
    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
        this.Accounts = new List<BankAccount>();
    }
    public Person(string name, int age, List<BankAccount> accounts)
    {
        this.Name = name;
        this.Age = age;
        this.Accounts = accounts;
    }
}
