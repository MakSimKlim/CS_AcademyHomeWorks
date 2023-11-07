﻿//ДЗ__28_срок_до_09._11._23_CS_Массивы
/*
Задание 1. Объявить одномерный (5 элементов) массив с именем A и
двумерный массив (3 строки, 4 столбца) дробных чисел с именем B.
Заполнить одномерный массив А числами, введенными с клавиатуры
пользователем, а двумерный массив В случайными числами с помощью
циклов. Вывести на экран значения массивов: массива А в одну строку,
массива В — в виде матрицы. Найти в данных массивах общий
максимальный элемент, минимальный элемент, общую сумму всех
элементов, общее произведение всех элементов, сумму четных элементов
массива А, сумму нечетных столбцов массива В.
*/

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        //================== одномерный массив ========================================
        Console.WriteLine("********** Одномерный массив А **********");

        int[] A = new int[5];

        // Заполняем массив данными, введенными с клавиатуры
        for (int i=0; i < A.Length; i++)
        {
            Console.Write($"Введите элемент №{i + 1}: ");
            A[i] = int.Parse(Console.ReadLine());
        }

        // Выводим значения массива на экран
        Console.WriteLine("Массив A: ");
        foreach (int i in A)
        {
            Console.Write( i + " ");
        }
        Console.WriteLine();

        // Находим максимальный и минимальный элементы
        Console.WriteLine($"Максимальный элемент: {A.Max()}");
        Console.WriteLine($"Минимальный элемент: {A.Min()}");

        // Находим общую сумму и общее произведение элементов
        Console.WriteLine($"Общая сумма элементов: {A.Sum()}");

        // Находим общее произведение элементов
        Console.WriteLine($"Общее произведение элементов: {A.Aggregate(1, (acc, x) => acc * x)}");

        // Находим сумму четных элементов
        Console.WriteLine($"Сумма четных элементов: {A.Where(x => x % 2 == 0).Sum()}");

        //================== двумерный массив ========================================
        Console.WriteLine("********** Двумерный массив В **********");

        double[,] B = new double[3, 4];
        Random rand = new Random();

        // Заполняем двумерный массив случайными числами
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                B[row, col] = rand.NextDouble()*100; // Генерируем случайное дробное число
            }
        }

        // Выводим двумерный массив B в виде матрицы
        Console.WriteLine("Массив B:");

        int height = B.GetLength(0);
        int width = B.GetLength(1);
        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(B[y, x] + "\t");
            }
            Console.WriteLine();
        }

        // Находим максимальный и минимальный элементы
        // используем метод Cast<double>(), чтобы преобразовать многомерный массив в одномерную последовательность элементов
        Console.WriteLine($"Максимальный элемент: {B.Cast<double>().Max()}"); 
        Console.WriteLine($"Минимальный элемент: {B.Cast<double>().Min()}");

        // Находим общую сумму и общее произведение элементов
        Console.WriteLine($"Общая сумма элементов: {B.Cast<double>().Sum()}");

        // Находим общее произведение элементов
        Console.WriteLine($"Общее произведение элементов: {B.Cast<double>().Aggregate(1.0, (acc, x) => acc * x)}");

        // Находим сумму нечетных столбцов
        double sumOfOddElementsInEachRow = 0.0;

        for (int row = 0; row < B.GetLength(0); row++)
        {
            for (int col = 0; col < B.GetLength(1); col++)
            {
                if (col % 2 == 0 && B[row, col] % 2.0 != 0)
                {
                    sumOfOddElementsInEachRow += B[row, col];
                }
            }
        }

        Console.WriteLine($"Сумма нечетных столбцов: {sumOfOddElementsInEachRow}");
    }
}





