using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.DesignPatterns.ConsoleApp.Behavioral
{
    public class Product
    {
        public int Id, Price; public string Name; public ProductType Type;
    }
    public enum ProductType {  HomeFurnishing, Kitchen, Electronics, Groceries, Others }
    public class DiscountCalculator
    {
        public static double GetDiscount(Product product)
        {
            double discount = 0.0;
            switch(product.Type)
            {
                case ProductType.HomeFurnishing:
                case ProductType.Kitchen:
                    discount = 0.20; break;
                case ProductType.Groceries:
                    discount = 0.05; break;
                default:
                    discount = 0.10; break;
            }
            return discount;
        }
    }
    public interface IDiscountStrategy
    {
        double GetDiscount(Product item);
    }
    public class HomeFurnishingDiscount : IDiscountStrategy
    {
        public double GetDiscount(Product item)
        {
            return 0.20;
        }
    }
    public class ElectronicsDiscount : IDiscountStrategy
    {
        public double GetDiscount(Product item)
        {
            return 0.10;
        }
    }
    public class GroceriesDiscount : IDiscountStrategy
    {
        public double GetDiscount(Product item)
        {
            return 0.05;
        }
    }
    public class KitchenDiscount : IDiscountStrategy
    {
        public double GetDiscount(Product item)
        {
            return 0.20;
        }
    }
    public class OthersDiscount : IDiscountStrategy
    {
        public double GetDiscount(Product item)
        {
            return 0.10;
        }
    }
    public class DiscountManager
    {
        Dictionary<string, IDiscountStrategy> discountList = new Dictionary<string, IDiscountStrategy>();
        public DiscountManager()
        {
            discountList.Add(ProductType.HomeFurnishing.ToString(), new HomeFurnishingDiscount());
            discountList.Add(ProductType.Electronics.ToString(), new ElectronicsDiscount());
            discountList.Add(ProductType.Others.ToString(), new OthersDiscount());
            discountList.Add(ProductType.Groceries.ToString(), new GroceriesDiscount());
            discountList.Add(ProductType.Kitchen.ToString(), new KitchenDiscount());
        }
        public double GetDiscount(Product product) => discountList[product.Type.ToString()].GetDiscount(product);
    }
    class StrategyPattern
    {
        internal static void Test()
        {
            Product p1 = new Product { Id = 1, Name = "product 1", Type = ProductType.HomeFurnishing, Price = 20 };
            Product p2 = new Product { Id = 2, Name = "product 2", Type = ProductType.Electronics, Price = 234};
            Product p3 = new Product { Id = 3, Name = "product 3", Type = ProductType.Others, Price = 88};
            Product p4 = new Product { Id = 4, Name = "product 4", Type = ProductType.Groceries, Price = 55};
            Product p5 = new Product { Id = 4, Name = "product 4", Type = ProductType.Kitchen, Price = 60};

            Console.WriteLine(DiscountCalculator.GetDiscount(p1));
            Console.WriteLine(DiscountCalculator.GetDiscount(p3));

            DiscountManager call = new DiscountManager();
            Console.WriteLine(   call.GetDiscount(p5));
            Console.WriteLine(call.GetDiscount(p4));

        }
    }
}
