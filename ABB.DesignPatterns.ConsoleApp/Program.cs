using ABB.DesignPatterns.ConsoleApp.Creational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using static System.Console;
using System.Data;
using ABB.DesignPatterns.ConsoleApp.Structural;
using ABB.DesignPatterns.ConsoleApp.Behavioral;

namespace ABB.DesignPatterns.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StrategyPattern.Test();
            //System.Linq.Expressions.
            //CommandPattern.Test();
            //ProxyPattern.Test();
           // DecoratorPattern.Test();
           // CompositePattern.Test();
            //BridgePattern.Test();

            //BuilderPattern.Test();
            //AbstractFactoryInDotNet(); 
            // AbstractFactoryClient.Test();
           // SingletonPattern.Test();
            //MyConfigurationReader.Current.PrintAll();
            //var r1 = MyConfigurationReader.Current;
            //r1.Display();
            //PrototypePattern.Test();
        }
        static void AbstractFactoryInDotNet()
        {
            string invariantName = "System.Data.SqlClient";
            DbProviderFactory factory = DbProviderFactories.GetFactory(invariantName);

            DbConnection connection = factory.CreateConnection();
            DbCommand command = factory.CreateCommand();
            DbDataAdapter adapter = factory.CreateDataAdapter();
            //IDataReader reader = command.ExecuteReader(); 
            WriteLine($"Type of Connection: {connection.GetType().FullName}");
            WriteLine($"Type of Command: {command.GetType().FullName}");
            WriteLine($"Type of Adapter: {adapter.GetType().FullName}");



        }
    }
}
