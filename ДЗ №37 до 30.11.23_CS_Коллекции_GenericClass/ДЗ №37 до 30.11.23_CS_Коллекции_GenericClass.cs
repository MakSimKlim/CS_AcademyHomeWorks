using System;
using System.Collections.Generic;

namespace GenericClass
{
    public class PriorityQueue<T, TPriority> where TPriority : IComparable<TPriority>
    {
        private readonly List<Tuple<T, TPriority>> _elements = new List<Tuple<T, TPriority>>();

        public int Count => _elements.Count;

        public void Enqueue(T item, TPriority priority)
        {
            _elements.Add(Tuple.Create(item, priority));
            _elements.Sort((x, y) => y.Item2.CompareTo(x.Item2));// Сортировка по убыванию приоритета
        }

        public T MaxPriority()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty.");

            T item = _elements[0].Item1;
            _elements.RemoveAt(0); // с удалением из списка
            return item;
        }

        public T MaxPriorityAfterDeleting()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty.");

            return _elements[0].Item1;// без удаления из списка
        }
    }

    class Program
    {
        static void Main()
        {
            // Пример использования очереди с приоритетами для строк и их длин
            PriorityQueue<string, int> priorityQueue = new PriorityQueue<string, int>();

            priorityQueue.Enqueue("Apple", 5);
            priorityQueue.Enqueue("Banana", 3);
            priorityQueue.Enqueue("Orange", 7);

            Console.WriteLine($"Dequeue: {priorityQueue.MaxPriority()}");
            Console.WriteLine($"Peek: {priorityQueue.MaxPriorityAfterDeleting()}");
            Console.ReadKey();
        }
        
    }
}
