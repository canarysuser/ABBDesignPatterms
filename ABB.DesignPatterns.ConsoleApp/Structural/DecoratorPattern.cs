using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.DesignPatterns.ConsoleApp.Structural
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple =true)]
    public class DeveloperAttribute: Attribute
    {
        public string Name; 
        public string ProjectName { get; set; }
        public DeveloperAttribute(string name) => Name = name;
        public override string ToString() => $"Name: {Name}, Project: {ProjectName}";
    }

    [Developer("Sudhanshu", ProjectName ="EComm")]
    class DecoratorPattern
    {
        [Developer("Sudhanshu", ProjectName = "EComm")]
        [Developer("Mohit", ProjectName = "EComm2")]
        public void MyMethod()
        {
            Console.WriteLine("MyMethod");
        }
        internal static void Test()
        {
            DecoratorPattern dp = new DecoratorPattern();
            Type t = typeof(DecoratorPattern);
            object[] attributes = t.GetCustomAttributes(true);
            foreach(DeveloperAttribute devAttr in attributes)
            {
                Console.WriteLine(devAttr);
                var method = t.GetMethod("MyMethod", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public );
                object[] attribs = method.GetCustomAttributes(true); 
                foreach(DeveloperAttribute a in attribs)
                    Console.WriteLine($"Method: {a}");
            }
        }
    }
}
