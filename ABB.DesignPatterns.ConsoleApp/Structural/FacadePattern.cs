using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ABB.DesignPatterns.ConsoleApp.Structural
{
    namespace Subsystem1
    {
        internal class OrderManager
        {
            public void PlaceOrder() => Console.WriteLine("OM: Order is Placed.");
            public void UpdateOrder() => Console.WriteLine("OM: Order is Updated.");
            public void CancelOrder() => Console.WriteLine("OM: Order is cancelled.");
            public void ProcessOrder() => Console.WriteLine("OM: Order is Processed.");
        }
    }
    namespace Subsystem2
    {
        internal class CustomerManager
        {
            public void AddCustomer() => Console.WriteLine("CM: Customer is Added.");
            public void UpdateCustomer() => Console.WriteLine("CM: Customer is Updated.");
            public void RemoveCustomer() => Console.WriteLine("CM: Customer is cancelled.");
        }
    }
    //Facade 
    public class AdministratorFacade {
        private Subsystem1.OrderManager om = new Subsystem1.OrderManager();
        private Subsystem2.CustomerManager cm = new Subsystem2.CustomerManager();
        public void AddCustomer() => cm.AddCustomer();
        public void RemoveCustomer() => cm.RemoveCustomer();
        public void ProcessOrder() => om.ProcessOrder();

    }
    public class CustomerFacade
    {
        private Subsystem1.OrderManager om = new Subsystem1.OrderManager();
        private Subsystem2.CustomerManager cm = new Subsystem2.CustomerManager();
        public void UpdateCustomer() => cm.UpdateCustomer();
        public void PlaceOrder() => om.PlaceOrder();
        public void UpdateOrder() => om.UpdateOrder();
        public void CancelOrder() => om.CancelOrder();
    }
    class FacadePattern
    {
        internal static void Test()
        {
            AdministratorFacade admin = new AdministratorFacade();
            admin.AddCustomer(); admin.RemoveCustomer(); admin.ProcessOrder();
            CustomerFacade cust = new CustomerFacade();
            cust.UpdateCustomer(); cust.UpdateOrder(); cust.PlaceOrder();cust.CancelOrder();
        }
    }
}
