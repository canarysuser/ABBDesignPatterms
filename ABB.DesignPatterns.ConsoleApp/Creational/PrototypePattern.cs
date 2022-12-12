using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ABB.DesignPatterns.ConsoleApp.Creational
{
    class PrototypePattern
    {
        internal static void Test()
        {
            var sam = new Person("Sam Rhodes", new Address(101, "My Street"));
            Console.WriteLine($"Original: {sam}");
            var sam2 = sam;
            Console.WriteLine($"After assignment: {sam2}");
            sam2.Name = "Jhonty Ferguson";
            sam2.Address.StreetName = "Cape Town Main Road";
            Console.WriteLine($"After changes: \n\t[1.{sam}], \n\t[2.{sam2}]");
            var sam3 = sam.Clone() as Person;
            Console.WriteLine($"After assignment: {sam3}");
            sam3.Name = "Lionel Messi";
            sam3.Address.StreetName = "Parkway Street";
            Console.WriteLine($"After changes: \n\t[1.{sam}], \n\t[2.{sam2}], \n\t[3.{sam3}]");
            var sam4 = sam3.DeepCopy();
            Console.WriteLine($"After assignment: {sam4}");
            sam4.Name = "Rohit Sharma";
            sam4.Address.StreetName = "2nd Main, Delhi";
            Console.WriteLine($"After changes: \n\t[1.{sam}], \n\t[2.{sam2}], \n\t[3.{sam3}], \n\t[4.{sam4}]");
        }
    }
    interface ICloneable<T>
    {
        T DeepCopy();
    }

    [Serializable]
    class Person : ICloneable, ICloneable<Person>
    {
        public string Name;
        public Address Address;
        public Person(string name, Address addr)
        {
            Name = name; Address = addr;
        }
        public object Clone() //ShallowCopy
        {
            return this.MemberwiseClone();
        }
        public Person DeepCopy()
        {
            var name = this.Name;
            var addr = this.Address.DeepCopy();
            var _this = new Person(name, addr);
            return _this;
        }
        public override string ToString()
        {
            return $"Name: {Name}, Address:{Address.HouseNumber},{Address.StreetName}";
        }
    }
    [Serializable]
    class Address : ICloneable<Address>
    {
        public string StreetName;
        public int HouseNumber; 
        public Address(int houseNumber, string streetName)
        {
            StreetName = streetName; HouseNumber = houseNumber;
        }

        public Address DeepCopy()
        {
            BinaryFormatter formatter = new BinaryFormatter(); 
            using(MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);
                stream.Position = 0;
                var _this = formatter.Deserialize(stream) as Address;
                return _this;
            }
        }
    }

    
}
