using System;

namespace ConstructorInjection
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
        private IAccount account;

        public Account(IAccount account)
        {
            this.account = account;
        }

        public void PrintAccounts()
        {
            account.PrintDetails();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            IAccount currentAccount = new CurrentAccount();
            Account account = new Account(currentAccount);
            account.PrintAccounts();

            IAccount savingsAccount = new SavingsAccount();
            Account account1 = new Account(savingsAccount);
            account1.PrintAccounts();
        }
    }
}
