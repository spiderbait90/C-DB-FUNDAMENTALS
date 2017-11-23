using System;
using System.Collections.Generic;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public decimal Limit { get;  set; }
        public decimal MoneyOwed { get;  set; }
        public decimal LimitLeft { get { return Limit - MoneyOwed; } }
        public DateTime ExpirationDate { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }


        public void Withdraw(decimal money)
        {            
            if (money>Limit)
            {
                throw new ArgumentException("Insufficient funds!");         
            }
            MoneyOwed += money;
        }

        public void Deposit(decimal money)
        {
            if (money < 0)
            {
                throw new ArgumentException("Value cannot be negative!");
            }
            MoneyOwed -= money;
        }
    }
}
