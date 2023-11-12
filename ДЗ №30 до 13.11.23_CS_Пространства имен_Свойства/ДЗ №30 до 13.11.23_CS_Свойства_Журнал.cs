using System;

namespace Properties
{
    class Magazine
    {
        public string Name { get; set; }
        public int YearRelease { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string EMail { get; set; }

        public Magazine(string name, int yearRelease, string description, string telephone, string email)
        {
            Name = name;
            YearRelease = yearRelease;
            Description = description;
            Telephone = telephone;
            EMail = email;
        
        }
        // Метод для ввода данных из консоли
        public void InputData()
        {
            Console.Write("Enter the name of the magazine: ");
            Name = Console.ReadLine();

            Console.Write("Enter the year of release: ");
            int.TryParse(Console.ReadLine(), out int year);
            YearRelease = year;

            Console.Write("Enter the description: ");
            Description = Console.ReadLine();

            Console.Write("Enter the telephone: ");
            Telephone = Console.ReadLine();

            Console.Write("Enter the email: ");
            EMail = Console.ReadLine();
        }

        // Метод для вывода данных в консоль
        public void DisplayData()
        {
            Console.WriteLine($"Magazine: {Name}");
            Console.WriteLine($"Year of Release: {YearRelease}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Telephone: {Telephone}");
            Console.WriteLine($"Email: {EMail}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем экземпляр класса Magazine
            Magazine magazine = new Magazine("", 0, "", "", "");

            // Вводим данные
            magazine.InputData();

            // Выводим данные
            magazine.DisplayData();

        }
    }
}
