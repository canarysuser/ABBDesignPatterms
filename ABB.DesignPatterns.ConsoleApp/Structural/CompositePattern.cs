using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.DesignPatterns.ConsoleApp.Structural
{
    public interface IComponent<T>
    {
        void Add(IComponent<T> item);
        IComponent<T> Remove(T item);
        IComponent<T> Find(T item);
        string Display(int depth);
        T Name { get; set; }
    }
    public class Component<T> : IComponent<T>
    {
        public T Name { get; set; }
        public Component(T name)
         => Name = name;

        public void Add(IComponent<T> item)
        => Console.WriteLine("Cannot add to an item.");

        public string Display(int depth)
        => $"{new string('-', depth)}{Name}\n";

        public IComponent<T> Find(T item)
        => item.Equals(Name) ? this : null;

        public IComponent<T> Remove(T item)
        {
            Console.WriteLine("Cannot remove directly.");
            return this;
        }
    }

    public class Composite<T> : IComponent<T>
    {
        public T Name { get; set; }
        List<IComponent<T>> list = new List<IComponent<T>>();
        public Composite(T name) => Name = name;
        public void Add(IComponent<T> item)
        {
            list.Add(item);
        }

        public string Display(int depth)
        {
            StringBuilder sb = new StringBuilder(new string('-', depth));
            sb.Append($"Set {Name} length: {list.Count}\n");
            list.ForEach(c => sb.Append(c.Display(depth + 2)));
            return sb.ToString();
        }

        IComponent<T> startLocation = null;
        public IComponent<T> Find(T item)
        {
            if (Name.Equals(item)) return this;
            IComponent<T> found = null; 
            foreach(IComponent<T> obj in list)
            {
                found = obj.Find(item);
                if (found != null) break;
            }
            return found;
        }


        public IComponent<T> Remove(T item)
        {
            throw new NotImplementedException();
        }
    }
    class CompositePattern
    {
        internal static void Test()
        {
            IComponent<string> album = new Composite<string>("Album");
            IComponent<string> photo = new Composite<string>("Photo");
            album.Add(photo);
            photo.Add(new Composite<string>("Birthday Photos"));
            photo.Add(new Component<string>("EndNode"));

            Console.WriteLine($"Album list: \n {album.Display(1)}");

        }
    }
}
