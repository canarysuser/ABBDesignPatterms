using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console; 

namespace ABB.DesignPatterns.ConsoleApp.Creational
{
    class BuilderPattern
    {
        internal static void Test()
        {
            Console.Clear();
            PatternLessImplementation();
            WriteLine();
            WithHtmlElement();
            HtmlBuilder();
        }
        static void PatternLessImplementation()
        {
            var words = new[] { "hello", "world" };
            StringBuilder builder = new StringBuilder();
            builder.Clear();
            builder.Append("<ul>"); 
            foreach(var word in words)
            {
                builder.AppendFormat($"<li>{word}</li>"); 
            }
            builder.Append("</ul>");
            WriteLine(builder.ToString());
        }
        static void WithHtmlElement()  //Builder
        {
            var words = new[] { "hello", "world" };
            HtmlElement root = new HtmlElement("ul", string.Empty); //Complex object or Product 
            foreach (var word in words)
                root.AddElement("li", word); //Part of the Object
            WriteLine(root);
        }
        static void HtmlBuilder()
        {
            var bldr = new HtmlBuilder("ul");
            bldr.AddElement("li", "hello");
            bldr.AddElement("li", "world");
            WriteLine(bldr);
            var b2 = new HtmlBuilder("ul")
                .AddElementFluent("li", "hello")
                .AddElementFluent("li", "world")
                .AddElementFluent("li", "!!!");
            WriteLine(b2);
        }
    }
    class HtmlBuilder
    {
        protected readonly string rootName;
        protected HtmlElement rootElement = new HtmlElement(); 
        public HtmlBuilder(string name)
        {
            rootName = name;
            rootElement.Name = name; 
        }
        public void AddElement(string name, string text)
        {
            rootElement.AddElement(name, text);
        }
        public HtmlBuilder AddElementFluent(string name, string text)
        {
            rootElement.AddElement(name, text);
            return this;
        }
        public override string ToString() => rootElement.ToString();
        
    }
    class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>(); 
        public HtmlElement() { }
        public HtmlElement(string name, string text)
        {
            Name = name; Text = text;
        }
        public void AddElement(string name, string text)
        {
            Elements.Add(new HtmlElement(name, text));
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.AppendFormat($"<{Name}>");
            if (!string.IsNullOrEmpty(Text)) sb.Append(Text); 
            if(Elements.Count>1)
            {
                foreach(var element in Elements)
                {
                    sb.AppendFormat($"<{element.Name}>{element.Text}</{element.Name}>");
                }
            }
            sb.AppendFormat($"</{Name}>");
            return sb.ToString();
        }
    }
}
