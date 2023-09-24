using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyInjection
{
    interface IAccount
    {
        void PrintDetails();
    }

    class CurrentAccount : IAccount
    {
        public void PrintDetails() 
        {
            Console.WriteLine("Details of Current Account.");
        }
    }

    class SavingsAccount : IAccount
    {
        public void PrintDetails()
        {
            Console.WriteLine("Details of Savings Account.");
        }
    }

    class Account
    {
        public IAccount account { get; set; }

        public void PrintAccounts()
        {
            account.PrintDetails();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account();
            account.account = new CurrentAccount();
            account.PrintAccounts();

            account.account = new SavingsAccount();
            account.PrintAccounts();
        }
    }
}
