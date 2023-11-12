using System;

namespace ДЗ__32_до_18._11._23_CS_Перегрузка_операторов
{
    public class Fraction
{
    private int numerator;// Числитель
        private int denominator;// Знаменатель

        public int Numerator
    {
        get { return numerator; }
    }

    public int Denominator
    {
        get { return denominator; }
    }
    
        // Конструктор класса Fraction
    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Знаменатель не может быть равен 0.");
        }

        this.numerator = numerator;
        this.denominator = denominator;

            // Проверяем, что числитель не становится равным нулю после создания объекта
            if (numerator == 0)
        {
            throw new ArgumentException("Числитель равен 0.");
        }
    }
        // Перегрузка оператора инкремента
        public static Fraction operator ++(Fraction fraction)
    {
        fraction.numerator += fraction.denominator;
        return fraction;
    }
        // Перегрузка оператора декремента
        public static Fraction operator --(Fraction fraction)
    {
        fraction.numerator -= fraction.denominator;
        return fraction;
    }
        // Перегрузка оператора отрицания
        public static Fraction operator !(Fraction fraction)
    {
        fraction.numerator = -fraction.numerator;
        return fraction;
    }
        // Перегрузка оператора true
        public static bool operator true(Fraction fraction)
    {
        return fraction.numerator != 0;
    }
        // Перегрузка оператора false
        public static bool operator false(Fraction fraction)
    {
        return fraction.numerator == 0;
    }
        // Перегрузка метода ToString для удобного вывода
        public override string ToString()
    {
        return $"{numerator}/{denominator}";
    }
}

class Program
{
        static void Main(string[] args)

        {
            try
        {
                // Создание объекта Fraction
                Fraction fraction = new Fraction(3,4);
            Console.WriteLine($"Оригинальная дробь: {fraction}");

                // Использование оператора инкремента
                fraction++; 
            Console.WriteLine($"После перегрузки инкремента: {fraction}");

                // Использование оператора декремента
                fraction--; 
            Console.WriteLine($"После перегрузки декремента: {fraction}");

                // Использование оператора отрицания
                fraction = !fraction;
            Console.WriteLine($"После перегрузки оператора отрицания: {fraction}");

                // Проверка с использованием операторов true и false
                if (fraction)
            {
                Console.WriteLine("Дробь не нулевая.");
            }
            else
            {
                Console.WriteLine("Дробь нулевая.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
    }
