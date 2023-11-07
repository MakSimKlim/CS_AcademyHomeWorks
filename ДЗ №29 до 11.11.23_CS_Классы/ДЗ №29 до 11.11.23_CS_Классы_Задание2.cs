//ДЗ №29 до 11.11.23_CS_Классы_Задание2
/*
 Задание 2. Создайте класс Корзина, который хранит массив классов Продукт.
Продукт имеет название и цену, может их выводить по методу Print, имеет
соответствующие конструкторы. Корзина может вывести список продуктов, а
также посчитать их суммарную стоимость, имеет конструктор по умолчанию,
а также методы для добавления и удаления продуктов из корзины.
 
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace ДЗ__29_до_11._11._23_CS_Классы_Задание2
{
    class Product
    {
        public string Name { get; }
        public double Price { get; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public void Print()
        {
            Console.WriteLine($"{Name}: ${Price}");
        }
    }

    class Cart
    {
        private List<Product> products;

        public Cart()
        {
            products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

        public void PrintProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }

            Console.WriteLine("Продукты в корзине:");
            foreach (var product in products)
            {
                product.Print();
            }
        }

        public double CalculateTotalPrice()
        {
            return products.Sum(product => product.Price);
        }
    }

    class Program
    {
        static void Main()
        {
            Product product1 = new Product("Яблоко", 1.0);
            Product product2 = new Product("Банан", 0.5);
            Product product3 = new Product("Молоко", 2.0);

            Cart cart = new Cart();
            cart.AddProduct(product1);
            cart.AddProduct(product2);
            cart.AddProduct(product3);

            cart.PrintProducts();

            double totalPrice = cart.CalculateTotalPrice();
            Console.WriteLine($"Общая стоимость: ${totalPrice}");

            cart.RemoveProduct(product2);

            Console.WriteLine("После удаления продукта 'Банан':");
            cart.PrintProducts();

            totalPrice = cart.CalculateTotalPrice();
            Console.WriteLine($"Общая стоимость: ${totalPrice}");
        }
    }

}
