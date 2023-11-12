using System;

namespace ДЗ__31_до_16._11._23_CS_Наследование_Задание_1
{
    public abstract class Human
    {
        string firstName;
        string lastName;
        DateTime birthDate;

        public Human()
        { }

        public Human(string fName, string lName)
        {
            firstName = fName;
            lastName = lName;
        }

        public Human(string fName, string lName, DateTime date) : this(fName, lName)
        {
            birthDate = date;
        }

        public override string ToString()
        {
            return $"Фамилия: {lastName}\n" +
                $"Имя: {firstName}\n" +
                $"Дата рождения: {birthDate.ToShortDateString()}\n";
        }
    }

    public abstract class Employee : Human
    {
        double _salary;

        public Employee(string fName, string lName) : base(fName, lName)
        { }

        public Employee(string fName, string lName, double salary) : base(fName, lName)
        {
            _salary = salary;
        }

        public Employee(string fName, string lName, DateTime date, double salary)
            : base(fName, lName, date)
        {
            _salary = salary;
        }

        public abstract void Introduce(); // Добавленный абстрактный метод
        public override string ToString()
        {
            return base.ToString() + $"Зарплата: {_salary} руб.\n";
        }
    }

    class Builder : Employee
    {
        string _builderInformation;
        public Builder(string fName, string lName, DateTime date, double salary, string builderInformation)
            : base(fName, lName, date, salary)
        {
            _builderInformation  = builderInformation;
        }

        public override void Introduce()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString() + $"Строитель. Информация: {_builderInformation}.\n";
        }
    }

    class Sailor : Employee
    {
        string _sailorInformation;
        public Sailor(string fName, string lName, DateTime date, double salary, string sailorInformation)
            : base(fName, lName, date, salary)
        {
            _sailorInformation = sailorInformation;
        }

        public override void Introduce()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString() + $"Моряк. Информация: {_sailorInformation}.\n";
        }
    }

    class Pilot : Employee
    {
        string _pilotInformation;
        public Pilot(string fName, string lName, DateTime date, double salary, string pilotInformation)
            : base(fName, lName, date, salary)
        {
            _pilotInformation = pilotInformation;
        }

        public override void Introduce()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString() + $"Пилот. Информация: {_pilotInformation}.\n";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Builder builder = new Builder("Алексей", "Кузнецов", new DateTime(1980, 01, 12), 125000.0, "Работает в составе основной бригады");
            Console.WriteLine(builder);

            Sailor sailor = new Sailor("Михаил", "Севостьянов", new DateTime(1973, 10, 18), 250000.0, "Работает на Ледоколе 'Ленин'");
            Console.WriteLine(sailor);

            Pilot pilot = new Pilot ("Андрей", "Лукин", new DateTime(1994, 05, 06), 200000.0,"Работает в Уральских Авиалиниях" );
            Console.WriteLine(pilot);

        }
    }
}
