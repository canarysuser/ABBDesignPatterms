using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.DesignPatterns.ConsoleApp.Behavioral
{
    public class BankAccount
    {
        private int balance;
        private int odLimit = -500; 
        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount}, balance: {balance}");
        }
        public bool Withdraw(int amount)
        {
            if(balance-amount >= odLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdraw {amount}, balance: {balance}");
                return true;
            }
            return false;
        }
        public override string ToString() => $"{nameof(balance)}: {balance}";
        
    }
    public interface ICommand
    {
        void Call();
        void Undo();
    }
    public enum BankAction {  Deposit, Withdraw} 
    public class BankAccountCommand : ICommand
    {
        private BankAccount account;
        private BankAction action;
        private int amount;
        private bool succeeded;

        public BankAccountCommand(BankAccount acc, BankAction action, int amount)
        {
            account = acc; this.action = action; this.amount = amount;
        }
        
        public void Call()
        {
            switch (action)
            {
                case BankAction.Deposit:
                    account.Deposit(amount);
                    succeeded = true;
                    break;
                case BankAction.Withdraw:
                    succeeded = account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public void Undo()
        {
            switch (action)
            {
                case BankAction.Deposit:
                    succeeded = account.Withdraw(amount); 
                    break;
                case BankAction.Withdraw:
                    account.Deposit(amount);
                    succeeded = true;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }

    class CommandPattern
    {

        internal static void Test()
        {
            BankAccount ba = new BankAccount();
            BankAccountCommand depCom = new BankAccountCommand(ba, BankAction.Deposit, 500);
            BankAccountCommand wdrCom = new BankAccountCommand(ba, BankAction.Withdraw, 1000);
            Console.WriteLine($"1. {ba}");
            depCom.Call();
            Console.WriteLine($"2. After Deposit {ba}");
            depCom.Undo();
            Console.WriteLine($"3. After Undo Deposit {ba}");

        }
    }
}
