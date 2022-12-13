using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ABB.DesignPatterns.ConsoleApp.Structural
{
    public interface ICustomerService
    {
        void AddCustomer();
    }
    public class CustomerServiceProxy : ICustomerService
    {
        public void AddCustomer()
        {
            //instantiate the real subject and forward the call to the real subject 
            Console.WriteLine("PROXY: Customer is added.");
        }
    }
    //Subject
    public interface IManager
    {
        void AddNew(string name, string address); 
    }
    //Real Subject 
    public class CustomerManager : IManager
    {
        public void AddNew(string name, string address)
        {
            Console.WriteLine($"REALSUBJECT: Name: {name}, Address: {address}");
        }
    }
    public interface IValidator {  bool IsValid(string value);}
    public class StringValidator : IValidator
    {
        public bool IsValid(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if(!Regex.Match(value, @"^\d\w+\d$").Success)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class CustomerManagerProxy : IManager
    {
        IValidator validator;
        CustomerManager realSubject;
        public CustomerManagerProxy(IValidator validator)
        {
            this.validator = validator;
            realSubject = new CustomerManager(); 
        }

        public void AddNew(string name, string address)
        {
            if (validator.IsValid(name) && validator.IsValid(address))
                realSubject.AddNew(name, address);
            else
                Console.WriteLine($"Either {name??"<<EMPTY>>"} or {address??"<<EMPTY>>"} is/are invalid.");
        }
    }

    class ProxyPattern
    {
        internal static void Test()
        {
            ICustomerService proxy = new CustomerServiceProxy();
            proxy.AddCustomer();
            IValidator validator = new StringValidator();
            CustomerManagerProxy p2 = new CustomerManagerProxy(validator);
            p2.AddNew("Valid Name", "Valid Address");
            p2.AddNew("12 Invalid name", null);
            p2.AddNew(null, "189, First Main, 999");
            Thread thread = new Thread(() => { });
            thread.IsBackground = true;
            thread.Name = "Managed Thread";
            thread.Start(); //actual OS thread is create with all these parameters. 
        }
    }
}
