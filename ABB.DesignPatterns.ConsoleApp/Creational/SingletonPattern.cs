using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ABB.DesignPatterns.ConsoleApp.Creational
{
   public sealed class MyConfigurationReader
    {
        private static int counter = 0;
        private static MyConfigurationReader _instance;
        private static object syncRoot = new object(); 

        private MyConfigurationReader()
        {
            counter++;
            WriteLine($"MyConfiguration.ctor() ==> Counter:{counter}"); 
        }
        public static MyConfigurationReader Current
        {
            get
            {
                if(_instance==null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new MyConfigurationReader();
                        }
                    }
                }
                return _instance;
            }
        }
        public ChildReader Reader = new ChildReader();
        public class ChildReader 
        {
            public void Display()
            {
                MyConfigurationReader rdr = new MyConfigurationReader();
                WriteLine($"{nameof(ChildReader)}.{nameof(Display)}");
            }
        }
        public  void Display()
        {
            WriteLine($"{nameof(MyConfigurationReader)}.{nameof(Display)}");
        }
    }
    class MyDatabase
    {
        public static int instanceCount = 1; 
        private MyDatabase()
        {
            WriteLine($"Initializing the DB class.. count: {instanceCount}");
            instanceCount++;
        }
        private static Lazy<MyDatabase> _instance = new Lazy<MyDatabase>(isThreadSafe:true);
        public static MyDatabase Instance => _instance.Value;
    }
    public class SingletonPattern
    { 
        internal static void Test()
        {
            MyDatabase m1 = MyDatabase.Instance;
            MyDatabase m2 = MyDatabase.Instance;

            MyConfigurationReader reader = MyConfigurationReader.Current;
            reader.Display();
            //reader.Reader.Display();
            //var child = new MyConfigurationReader.ChildReader();
            //child.Display();
            //var rdr2 = MyConfigurationReader.Current;
            //rdr2.Display();

            //Thread[] myThreads = new Thread[50];
            //ThreadStart ts1 = () =>
            //{
            //    MyConfigurationReader rdr = MyConfigurationReader.Current;
            //    rdr.Display();
            //};
            //for (int i = 0; i < myThreads.Length; i++)
            //{
            //    myThreads[i] = new Thread(ts1);
            //    myThreads[i].Start();
            //}
            //WriteLine("All threads started. Waiting for completion");
        }
    }

}
