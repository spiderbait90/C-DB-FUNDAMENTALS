using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main(string[] args)
    {
        var accounts = new Dictionary<int, BankAccount>();

        while (true)
        {
            var input = Console.ReadLine();
            var splited = input.Split(' ');
            switch (splited[0])
            {
                case "End": return;
                case "Create": Create(input, accounts); break;
                case "Deposit": Deposit(input, accounts); break;
                case "Withdraw": Withdraw(input, accounts); break;
                case "Print": Print(input, accounts); break;
            }
        }
    }

    public static void Print(string input, Dictionary<int, BankAccount> accounts)
    {
        var splited = input.Split(' ');
        var id = int.Parse(splited[1]);        
        if (!accounts.ContainsKey(id))
        {
            Console.WriteLine("Account does not exist");
        }
        else
        {
            Console.WriteLine($"Account ID {accounts[id].ID}, balance = {accounts[id].Balance:f2}");
            
        }
    }

    public static void Withdraw(string input, Dictionary<int, BankAccount> accounts)
    {
        var splited = input.Split(' ');
        var id = int.Parse(splited[1]);
        var balance = decimal.Parse(splited[2]);
        if(!accounts.ContainsKey(id))
        {
            Console.WriteLine("Account does not exist");
        }
        else if (balance>accounts[id].Balance)
        {
            Console.WriteLine("Insufficient balance");
        }
        else
        {
            accounts[id].Withdraw(balance);
        }
    }

    public static void Deposit(string input, Dictionary<int, BankAccount> accounts)
    {
        var splited = input.Split(' ');
        var id = int.Parse(splited[1]);
        var balance = decimal.Parse(splited[2]);
        if (!accounts.ContainsKey(id))
        {
            Console.WriteLine("Account does not exist");
        }
        else
        {
            accounts[id].Deposit(balance);
        }
    }

    public static void Create(string input, Dictionary<int, BankAccount> accounts)
    {
        var splited = input.Split();
        var id = int.Parse(splited[1]);        

        if (accounts.ContainsKey(id))
        {
            Console.WriteLine("Account already exists");
        }
        else
        {
            var acc = new BankAccount();
            acc.ID = id;
            accounts.Add(id, acc);
        }
    }
}

