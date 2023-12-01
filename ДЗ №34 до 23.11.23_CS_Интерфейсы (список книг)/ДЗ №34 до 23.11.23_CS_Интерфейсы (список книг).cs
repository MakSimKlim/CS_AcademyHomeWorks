using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Класс, представляющий книгу
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public override string ToString()
    {
        return $"{Title} by {Author}";
    }
}

// Класс списка книг
public class BookList : IEnumerable<Book>, ICloneable
{
    private List<Book> books = new List<Book>();

    public void AddBook(string title, string author)
    {
        books.Add(new Book(title, author));
    }

    public void RemoveBook(string title)
    {
        Book bookToRemove = books.Find(book => book.Title == title);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"Book '{title}' removed successfully.");
        }
        else
        {
            Console.WriteLine($"Book '{title}' not found in the list.");
        }
    }

    public bool ContainsBook(string title)
    {
        return books.Any(book => book.Title == title);
    }

    public void DisplayBooks()
    {
        Console.WriteLine("Book List:");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public IEnumerator<Book> GetEnumerator()
    {
        return books.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main()
    {
    BookList bookList = new BookList();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Check if Book is in the List");
            Console.WriteLine("4. Display Books");
            Console.WriteLine("5. Clone Book List");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Book Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter Author: ");
                    string author = Console.ReadLine();
                    bookList.AddBook(title, author);
                    break;
                case 2:
                    Console.Write("Enter Book Title to Remove: ");
                    string titleToRemove = Console.ReadLine();
                    bookList.RemoveBook(titleToRemove);
                    break;
                case 3:
                    Console.Write("Enter Book Title to Check: ");
                    string titleToCheck = Console.ReadLine();
                    bool containsBook = bookList.ContainsBook(titleToCheck);
                    Console.WriteLine($"Book '{titleToCheck}' is{(containsBook ? "" : " not")} in the list.");
                    break;
                case 4:
                    bookList.DisplayBooks();
                    break;
                case 5:
                    BookList clonedList = (BookList)bookList.Clone();
                    Console.WriteLine("Book List Cloned successfully.");
                    clonedList.DisplayBooks();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
