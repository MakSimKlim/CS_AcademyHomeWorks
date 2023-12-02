using System;
using System.IO;

class Logger
{
    private readonly string logFilePath = Path.Combine("Debug", "journal.log");
    private readonly string notesFilePath = Path.Combine("Debug", "Notes.txt");

    public Logger()
    {
        // Создаем папку Debug, если её нет
        if (!Directory.Exists("Debug"))
        {
            Directory.CreateDirectory("Debug");
        }

        // Инициализируем журнал лога
        Log("Программа запущена");
    }

    public void Log(string action)
    {
        // Записываем действие пользователя в журнал лога
        string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logMessage = $"{currentDateTime}: {action}\n";
        File.AppendAllText(logFilePath, logMessage);
    }

    public void AddNote()
    {
        try
        {
            Console.Write("Введите текст новой заметки: ");
            string noteText = Console.ReadLine();

            // Добавляем новую заметку
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd");
            string noteMessage = $"Дата: {currentDateTime}\nТекст Заметки: {noteText}\n";
            File.AppendAllText(notesFilePath, noteMessage);

            // Записываем действие в журнал лога
            Log("Создана новая заметка");
        }
        catch (Exception ex)
        {
            // Записываем исключение в журнал лога
            Log($"Было вызвано исключение: {ex.Message}");
        }
    }

    public void ReadAllNotes()
    {
        try
        {
            // Читаем все заметки
            string allNotes = File.ReadAllText(notesFilePath);
            Console.WriteLine(allNotes);

            // Записываем действие в журнал лога
            Log("Заметки прочитаны");
        }
        catch (Exception ex)
        {
            // Записываем исключение в журнал лога
            Log($"Было вызвано исключение: {ex.Message}");
        }
    }
}

class Program
{
    static void Main()
    {
        Logger logger = new Logger();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Добавить новую заметку");
            Console.WriteLine("2. Прочитать все заметки");
            Console.WriteLine("3. Выйти");

            Console.Write("Выберите действие (1-3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    logger.AddNote();
                    break;
                case "2":
                    logger.ReadAllNotes();
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите от 1 до 3.");
                    break;
            }
        }
    }
}
