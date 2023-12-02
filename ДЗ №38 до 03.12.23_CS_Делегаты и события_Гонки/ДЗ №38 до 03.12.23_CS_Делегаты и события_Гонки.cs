using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Racing
{
    // Абстрактный класс Car
    abstract class Car
    {
        // Делегат для события начала гонки
        public delegate void Start();
        // Делегат для события сообщения
        public delegate void Message();
        // Событие для отображения сообщения
        public event Message Display;

        // Метод для генерации случайной скорости в заданном диапазоне
        public int My_Speed(Random rnd, int r1, int r2)
        {
            int speed;
            return speed = rnd.Next(r1, r2 + 1);
        }

        // Метод для вывода сообщения о прибытии на финиш
        public void Win(string name)
        {
            Console.WriteLine($"{name} финишировал");
        }

        // Метод для выполнения гонки
        public void Go(string name, int speed)
        {
            int distance = 0;
            // Добавление обработчика события для отображения сообщения о прибытии на финиш
            Display += () => Win(name);
            for (int i = 0; i < 110; i += 10, distance += 10)
            {
                Thread.Sleep(speed);
                if (distance == 100)
                    // Вызов события для отображения сообщения о прибытии на финиш
                    Display();
            }
        }
    }

    // Класс Sport_Car, наследующийся от Car
    class Sport_Car : Car
    {
        public string Name = "Sport car";

        // Метод для запуска гонки на спортивном автомобиле
        public void Ride()
        {
            Go(Name, My_Speed(new Random(), 500, 800));
        }
    }

    // Класс Automobile, наследующийся от Car
    class Automobile : Car
    {
        public string Name = "Automobile";

        // Метод для запуска гонки на легковом автомобиле
        public void Ride()
        {
            Go(Name, My_Speed(new Random(), 600, 900));
        }
    }

    // Класс Cargo_Car, наследующийся от Car
    class Cargo_Car : Car
    {
        public string Name = "Cargo car";

        // Метод для запуска гонки на грузовом автомобиле
        public void Ride()
        {
            Go(Name, My_Speed(new Random(), 700, 1000));
        }
    }

    // Класс Bus, наследующийся от Car
    class Bus : Car
    {
        public string Name = "Bus";

        // Метод для запуска гонки на автобусе
        public void Ride()
        {
            Go(Name, My_Speed(new Random(), 800, 1100));
        }
    }

    // Основной класс Program
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Гонка началась!");
            // Создание экземпляров различных типов автомобилей
            Sport_Car car = new Sport_Car();
            Bus bus = new Bus();
            Cargo_Car cargo_Car = new Cargo_Car();
            Automobile automobile = new Automobile();

            
            // Создание списка потоков для выполнения гонок
            List<Thread> threads = new List<Thread>()
            {
                new Thread(cargo_Car.Ride),
                new Thread(car.Ride),
                new Thread(bus.Ride),
                new Thread(automobile.Ride)
            };

           

            // Запуск каждого потока
            foreach (Thread list in threads)
            {
                list.Start();
            }

            // Ожидание нажатия клавиши перед завершением программы
            Console.ReadKey();
            
        }
    }
}
