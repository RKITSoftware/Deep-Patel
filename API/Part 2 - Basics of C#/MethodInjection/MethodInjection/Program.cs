using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodInjection
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
        public void PrintAccounts(IAccount account)
        {
            account.PrintDetails();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account();
            account.PrintAccounts(new CurrentAccount());
            account.PrintAccounts(new SavingsAccount());
        }
    }
}
