using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Solid_Principles
{
    public static class OpenClosePrinciple
    {
        public static void Run()
        {

            var apple = new Product("Apple", Color.green, Size.small);
            var tree = new Product("Tree", Color.green, Size.large);
            var house = new Product("House", Color.blue, Size.large);

            Product[] products = new Product[] { apple, tree, house };

            var filter = new ProductFilter();
            var filtered=filter.filterBySize(products, Size.large);
            foreach (var item in filter.filterByColor(products, Color.green))
            {
                Console.WriteLine($"{item.name} is green");
            }

            var betterFilter = new Betterfilter();
            foreach (var item in betterFilter.Filter(products, new ColorSpecification(Color.green)))
            {
                Console.WriteLine($"{item.name} is {item.color}");
            }

            foreach (var item in betterFilter.Filter(products, new SizeSpecification(Size.large)))
            {
                Console.WriteLine($"{item.name} is {item.size}");
            }

            foreach (var item in betterFilter.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.blue),new SizeSpecification(Size.large))))
            {
                Console.WriteLine($"{item.name} is {item.color} and {item.size}");
            }
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> filterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var item in products)
            {
                if (item.size == size)
                    yield return item;
            }
        }
        public IEnumerable<Product> filterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var item in products)
            {
                if (item.color == color)
                    yield return item;
            }
        }
        public IEnumerable<Product> filterBySizeAndColor(IEnumerable<Product> products, Color color,Size size)
        {
            foreach (var item in products)
            {
                if (item.color == color && item.size==size)
                    yield return item;
            }
        }
    }

    public class Product
    {
        public Product(string Name, Color Color, Size Size)
        {
            name = Name;
            color = Color;
            size = Size;
        }

        public string name;
        public Color color;
        public Size size;

    }

    public enum Color
    {
        red,
        green,
        blue
    }

    public enum Size
    {
        small,
        medium,
        large,
        Xlarge
    }


    public interface Ispecification<T> 
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, Ispecification<T> specification);
    }

    public class ColorSpecification : Ispecification<Product>
    {
        public ColorSpecification(Color color)
        {
            Color = color;
        }

        public Color Color;

        public bool IsSatisfied(Product t)
        {
            return t.color == Color;
        }
    }

    public class SizeSpecification : Ispecification<Product>
    {

        public SizeSpecification(Size size)
        {
            Size = size;
        }

        public Size Size;

        public bool IsSatisfied(Product t)
        {
            return t.size == Size;
        }
    }

    public class AndSpecification<T> : Ispecification<T>
    {
        public AndSpecification(Ispecification<T> first, Ispecification<T> second)
        {
            this.first = first;
            this.second = second;
        }

        Ispecification<T> first;
        Ispecification<T> second;

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class Betterfilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, Ispecification<Product> specification)
        {
            foreach (var item in items)
            {
                if (specification.IsSatisfied(item))
                {
                    yield return item;
                }
            }
        }
    }
}
