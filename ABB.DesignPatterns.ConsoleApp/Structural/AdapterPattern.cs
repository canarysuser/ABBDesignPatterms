using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ABB.DesignPatterns.ConsoleApp.Structural
{
    //Adaptee 
    public class CustomerDataStore
    {
        static string CR = "CUSTREPO";
        public DataSet GetAllCustomers()
        {
            WriteLine($"{CR}: Getting Customer List.");
            return new DataSet(); 
        }
        public DataTable GetCustomer(int id=0)
        {
            WriteLine($"{CR}: Getting Customer Details....");
            return new DataTable();
        }
        public void AddCustomer(DataRow row=null)
        {
            WriteLine($"{CR}: Adding new Customer....");
        }
        public void RemoveCustomer(int id=0)
        {
            WriteLine($"{CR}: Removing new Customer....");
        }
    }

    //NEW SYSTEM 
    public class Customer { public int ID; public string FirstName; public string LastName; }
    
    //TARGET INTERFACE
    public interface IRepository<TEntity, TIdentity>
    {
        List<TEntity> GetEntities();
        TEntity GetEntity(int id);
        void Create(TEntity item);
        void Remove(TIdentity id); 
    }

    //CLASS ADAPTER 
    public class CustomerRepository : CustomerDataStore, IRepository<Customer, int>
    {
        CustomerDataStore cds = new CustomerDataStore(); 
        public void Create(Customer item)
        {
            DataTable table = new DataTable();
            DataRow row = table.NewRow();
            //fill in the row data based on the item parameter
            base.AddCustomer(row);
        }

        public List<Customer> GetEntities()
        {
            var ds = base.GetAllCustomers();
            ds.Tables.CopyTo(new object[10], 0);
            //Write the conversion code from the dataset to the list 
            return new List<Customer>(); 
        }

        public Customer GetEntity(int id)
        {
            var table = base.GetCustomer(id);
            //Extract data from the table into an entity and send it
            return new Customer();

        }

        public void Remove(int id)
        {
            base.RemoveCustomer(id);
        }
    }


    //CLIENT
    class AdapterPattern
    {
        internal static void Test()
        {

            //LEGACY CODE 
            CustomerDataStore cds = new CustomerDataStore();
            var ds = cds.GetAllCustomers(); 
            //Convert the ds to the List<T> format 
            //Send this list to the D3 Chart

            IRepository<Customer, int> repo = new CustomerRepository();
            var list = repo.GetEntities();
            //send the list to the D3 Chart
            var item = repo.GetEntity(0);
        }
    }
}
