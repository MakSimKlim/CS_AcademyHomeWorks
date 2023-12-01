using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Класс исключения для книг
public class MyException : ApplicationException
{
    private string _message;

    public DateTime TimeException { get; private set; }

    // Конструктор, принимающий один аргумент (сообщение об ошибке)
    public MyException(string errorMessage)
    {
        _message = errorMessage;
        TimeException = DateTime.Now;
    }
    // Конструктор по умолчанию (без аргументов)
    public MyException()
    {
        _message = "My Exception";
        TimeException = DateTime.Now;
    }
    // Переопределение свойства Message
    public override string Message
    {
        get
        {
            return _message;
        }
    }
}

public class Book
{
    private string title;
    private string author;

    public string Title
    {
        get { return title; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new MyException();
            }

            title = value;
        }
    }

    public string Author
    {
        get { return author; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new MyException();
            }

            author = value;
        }
    }

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

public class BookList : IEnumerable<Book>, ICloneable
{
    private List<Book> books = new List<Book>();

    public void AddBook(string title, string author)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                throw new MyException();
            }

            books.Add(new Book(title, author));
            Console.WriteLine("Book added successfully.");
        }
        catch (MyException ex)
        {
            Console.WriteLine($"Error adding book: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format. Please enter valid data.");
        }
        finally
        {
            Console.WriteLine("Finally block executed.");
        }
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
            int choice = GetChoice();
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
        int GetChoice()
        {
            while (true)
            {
                Console.Write("Enter your choice: ");
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice < 1 || choice > 3)
                    {
                        throw new MyException("Invalid menu choice");
                    }
                    return choice;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
                catch (MyException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
