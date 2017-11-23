using P01_BillsPaymentSystem.Data;
using System;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using System.Linq;
using System.Globalization;
using P01_BillsPaymentSystem.App;
using System.Collections.Generic;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new BillsPaymentSystemContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
                Database.Seed(db);

                Console.WriteLine("Enter UserId 1 :");
                var userId = int.Parse(Console.ReadLine());

                Database.UserDetails(userId, db);

                Console.WriteLine("Do You Want To Withdraw Sum ? Y/N");
                var input = Console.ReadLine().ToLower();

                if (input == "y")
                {
                    Console.WriteLine("Enter sum to withdraw");
                    PayBills(userId, decimal.Parse(Console.ReadLine()), db);
                }
                   
            }            
        }

        private static void PayBills(int userId, decimal amount, BillsPaymentSystemContext db)
        {
            using (db = new BillsPaymentSystemContext())
            {
                var user = db.Users
                .Include(x => x.PaymentMethods)
                .Where(x => x.UserId == userId)                
                .FirstOrDefault();

                var bankAccounts = new List<BankAccount>();
                var creditCards = new List<CreditCard>();

                foreach (var a in db.BankAccounts)
                {
                    foreach (var b in user.PaymentMethods)
                    {
                        if (a.BankAccountId == b.BankAccountId)
                            bankAccounts.Add(a);
                    }                    
                }

                foreach (var a in db.CreditCards)
                {
                    foreach (var b in user.PaymentMethods)
                    {
                        if (a.CreditCardId == b.CreditCardId)
                            creditCards.Add(a);
                    }
                }

                try
                {
                    foreach (var ba in bankAccounts)
                    {
                        ba.Withdraw(amount);
                    }
                    foreach (var cc in creditCards)
                    {
                        cc.Withdraw(amount);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
        }        
        
    }
}
