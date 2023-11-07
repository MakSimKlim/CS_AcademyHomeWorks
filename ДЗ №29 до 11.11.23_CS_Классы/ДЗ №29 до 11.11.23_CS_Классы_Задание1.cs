//ДЗ №29 до 11.11.23_CS_Классы_Задание1
/*
 Задание 1. Разработать класс Rectangle, который будет хранить высоту,
ширину и символ прямоугольника. Разработать у него конструктор по
умолчанию, который будет создавать прямоугольник размер 1 на 1 с
символом “*”, конструктор, который принимает высоту и ширину (символ
становится “*”), и который принимает все три параметра.
Создать метод Print, который выводит в консоль текстовым прямоугольник
заданного размера заданным символом. Он принимает один параметр типа
bool, если тот true, прямоугольник заполнен, если false - то нет.

 */


using System;

namespace ДЗ__29_до_11._11._23_CS_Классы_Задание1
{

    class Rectangle
{
    private int height;
    private int width;
    private char symbol;

    public Rectangle()
    {
        height = 1;
        width = 1;
        symbol = '*';
    }

    public Rectangle(int height, int width)
    {
        this.height = height;
        this.width = width;
        symbol = '*';
    }

    public Rectangle(int height, int width, char symbol)
    {
        this.height = height;
        this.width = width;
        this.symbol = symbol;
    }

    public void Draw()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write(symbol);
            }
            Console.WriteLine();
        }
    }

    public void Print(bool filled)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (filled || i == 0 || i == height - 1 || j == 0 || j == width - 1)
                {
                    Console.Write(symbol);
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        Rectangle defaultRectangle = new Rectangle();
        defaultRectangle.Draw();
        Console.WriteLine();

        Rectangle customRectangle = new Rectangle(3, 5);
        customRectangle.Draw();
        Console.WriteLine();

        Rectangle customSymbolRectangle = new Rectangle(4, 4, '#');
        customSymbolRectangle.Draw();
        Console.WriteLine();

        Rectangle filledRectangle = new Rectangle(5, 10, '*');
        filledRectangle.Print(true);
        Console.WriteLine();

        Rectangle hollowRectangle = new Rectangle(6, 8, '#');
        hollowRectangle.Print(false);
    }
}

}
